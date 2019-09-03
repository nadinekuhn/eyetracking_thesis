using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    public Text textField;
    protected static int levelToLoad;

    public GameObject selectionManager;
    public GameObject tobiiXRInitializer;

    public static bool successfulCalibration = false;

    void Start()
    {
        SpawnSelectableObject.isScenarioDone = false;
        
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        if (activeScene != 0) { 
            textField.text = $"Szenario: {activeScene}\nSelektion mit Eye-Tracking";
        } else
        {
            textField.text = "Kalibrierung\nPositioniere das Headset und folge mit den Augen den roten Punkten";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnSelectableObject.isScenarioDone)
        {
            FadeToNextLevel();
        }

        if(successfulCalibration)
        {
            textField.text = "Kalibrierung erfolgreich!";
            textField.enabled = true;
            successfulCalibration = false;
            FadeToNextLevel();
        }
    }

    public void FadeToNextLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (activeSceneIndex <= 3)
        {
            FadeToLevel(activeSceneIndex);
        }
        else {
           //fadeToLevel(0);
        }
        
    }

    public void FadeToLevel(int levelindex)
    {
        levelToLoad = levelindex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void OnStart()
    {
        selectionManager.SetActive(true);
        tobiiXRInitializer.SetActive(true);
    }

    public static void setSuccessfulCalibration(bool successful)
    {
        successfulCalibration = successful;
    }


}
