using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii;
using Tobii.XR;
using Tobii.G2OM;
using Tobii.Research;
using csDelaunay;

public class VoronoiSelection : MonoBehaviour
{
    // We are using a constant weight distributor for the voronoi diagram
    // This allows easy access to objects that are at the back
    // In a data viz scenario, this is useful
    [Tooltip("Constant weight distributor for each Voronoi site. [0 - 1.0] value range.")]
    public double WeightDistributor = 0.8;

    // We are using a constant for the sphere cast radius. 0.3 meters is enough for most scenarios.
    // Increase this value if your objects are very far away from each other. 
    public float SphereCastRadius = 0.3f;

    private List<Edge> edges;
    private Rectf bounds;
    private Voronoi voronoi;

    // Start is called before the first frame update
    void Start()
    {
        bounds = new Rectf(0, 0, Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        print("VORONOI");
        if(!HighlightAtGaze.isFocused)
        {
            // Only check objects in layer 9
            var layerMask = 1 << 9;
            float maxDistance = 80.0f;
            // Cast a sphere to see what nodes are in the radius of the head orientation
            var hitObjects = Physics.SphereCastAll(TobiiXR.EyeTrackingData.GazeRay.Origin, SphereCastRadius, TobiiXR.EyeTrackingData.GazeRay.Direction + new Vector3(0,0,1), maxDistance,  layerMask);
            var voronoiNodes = new List<Vector2>();

            if(hitObjects.Length > 0)
            {
                int objectindex = -1;
                if (hitObjects.Length == 1)
                {
                    // Only one object in range -> no voronoi needed
                    objectindex = 0;
                } else
                {
                    foreach (var hit in hitObjects)
                    {
                        var screenObjects = WorldTo2dHelper.GUICoordinatesWithObject(hit.collider.gameObject);
                        voronoiNodes.Add(screenObjects);
                    }

                    objectindex = this.CreateNewVoronoiSelection(voronoiNodes, new Vector2(Screen.width / 2, Screen.height / 2));
                }

                if (objectindex != -1 && hitObjects.Length > objectindex)
                {
                    var hitObject = hitObjects[objectindex].collider.gameObject;
                    if (hitObject != TobiiXR.FocusedObjects[0].GameObject)
                    {
                        hitObject.SendMessage("GazeFocusChanged", true);

                        if (TobiiXR.FocusedObjects[0].GameObject != null)
                        {
                            TobiiXR.FocusedObjects[0].GameObject.SendMessage("GazeFocusChanged", false);
                        }
                        
                    }
                }
            }
        }
    }

    public int CreateNewVoronoiSelection(List<Vector2> voronoiNodes, Vector2 centerPoint)
    {
        var points = new List<Vector2f>();
        foreach(var node in voronoiNodes)
        {
            points.Add(new Vector2f(node.x,node.y));
        }

        if (voronoi != null)
        {
            voronoi.Dispose();
        }

        voronoi = new Voronoi(points, bounds);
        edges = voronoi.Edges;
        if (edges.Count < 1)
            return -1;

        var closestSite = GetClosestSiteByPoint(new Vector2f(centerPoint.x, centerPoint.y));
        if (closestSite == null) { return -1; }
         
        // The site index does not match the inital points index , we need to check for the correct index
        for(int i = 0; i<points.Count; i++)
        {
            if(points[i] == closestSite.Coord)
            {
                return i;
            }
        }

        return -1;
    }

    private Site GetClosestSiteByPoint(Vector2f point)
    {
        var minDistance = float.MaxValue;
        var closestEdgeIndex = -1;
        for (int i = 0; i< edges.Count; i++)
        {
            // if clipped ends are null, it means the edge is outside the bound
            if (edges[i].ClippedEnds == null)
            {
                continue;
            }

            // Get the Minimum distance between point and edge
            var currentDistance = MinimumDistance(edges[i], new Vector2(point.x, point.y));
            if (currentDistance != -1 && currentDistance < minDistance)
            {
                minDistance = currentDistance;
                closestEdgeIndex = i;
            }
        }

        if(closestEdgeIndex == -1)
        {
            return null;
        }

        // Get the adhacent sites and return the nearest one
        var leftSite = edges[closestEdgeIndex].LeftSite;
        var rightSite = edges[closestEdgeIndex].RightSite;
        var distLeft = leftSite.Coord.DistanceSquare(point);
        var distRight = rightSite.Coord.DistanceSquare(point);

        return distLeft < distRight ? leftSite : rightSite;
    }

    private float MinimumDistance(Edge line, Vector2 p)
    {
        var v = new Vector2(line.ClippedEnds[LR.LEFT].x, line.ClippedEnds[LR.LEFT].y);
        var w = new Vector2(line.ClippedEnds[LR.RIGHT].x, line.ClippedEnds[LR.RIGHT].y);

        var vf = new Vector2f(v.x, v.y);
        var wf = new Vector2f(w.x, w.y);
        var pf = new Vector2f(p.x, p.y);

        // return minimum line segment between vw and p
        float twelve = vf.DistanceSquare(wf);
        if (twelve == 0.0) return pf.DistanceSquare(vf);
        float t = Vector2.Dot((p - v), (w - v)) / twelve;

        if (t < 0 || t > 1)
            return -1;

        // Projection falls on the segment
        Vector2 projection = v + t * (w - v);
        var projectionF = new Vector2f(projection.x, projection.y);
        return pf.DistanceSquare(projectionF);
    }

 
}
