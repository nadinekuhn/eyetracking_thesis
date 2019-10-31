using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    public Text textField;
    public Image background;
    protected static int levelToLoad;
    public bool eyetrackingEnabled = false;
    public bool bubbleCursorEnabled = false;

    public GameObject selectionManager;
    public GameObject tobiiXRInitializer;

    public static bool successfulCalibration = false;

    void Start()
    {
        SpawnSelectableObject.isScenarioDone = false;
        
        int activeScene = SceneManager.GetActiveScene().buildIndex;
     /*   if (activeScene != 0) { 
            textField.text = $"Szenario {activeScene}"; 
        } else
        {
            textField.text = "Kalibrierung\nPositioniere das Headset und folge mit den Augen den roten Punkten";
        }*/
    }

    // Update is called once per frame
    void Update()
    {
       /* if (SpawnSelectableObject.isScenarioDone)
        {
            FadeToNextLevel();
        }*/

        if(successfulCalibration)
        {
            textField.text = "Kalibrierung erfolgreich!";
            textField.enabled = true;
            successfulCalibration = false;
            //FadeToNextLevel();
        }

        switch (Input.inputString)
        {
            case "0":
                FadeToLevel(0);
                break;
            case "1":
                FadeToLevel(1);
                break;
            case "2":
                FadeToLevel(2);
                break;
            case "3":
                FadeToLevel(3);
                break;
            case "4":
                FadeToLevel(4);
                break;
            case "5":
                FadeToLevel(5);
                break;
            case "6":
                FadeToLevel(6);
                break;
            case "e":
                SceneManager.LoadScene(1);
                break;
            case "x":
                showTextfield("Wie schnell kam Dir die Selektion der Objekte vor?\n1-sehr langsam - 10-sehr schnell");
                break;
            case "y":
                showTextfield("Wie einfach/schwer fiel Dir die Selektion der Objekte?\n1-sehr einfach - 10-sehr schwer");
                break;
            case "z":
                showTextfield("Fühltest Du Dich zu irgendeiner Zeit frustriert?\n1-gar nicht - 10-sehr frustriert");
                break;

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

    public void showTextfield(string text)
    {
        textField.text = text;
        textField.color = new Color(0.9f, 0.9f, 0.9f,1);
        background.color = new Color(0.0f, 0.203536f, 0.227451f, 1);
        animator.SetTrigger("ShowQuestion");
    }

}
