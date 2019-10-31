using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowQuestions : MonoBehaviour
{

    public Text textField;
    // Start is called before the first frame update
    void Start()
    {
        showTextfield("Wie schnell kam Dir die Selektion der Objekte vor?\n1-sehr langsam - 10-sehr schnell");
    }

    // Update is called once per frame
    void Update()
    {
        switch (Input.inputString)
        {
            case "y":
                showTextfield("Wie einfach/schwer fiel Dir die Selektion der Objekte?\n1-sehr einfach - 10-sehr schwer");
                break;
            case "z":
                showTextfield("Fühltest Du Dich zu irgendeiner Zeit frustriert?\n1-gar nicht - 10-sehr frustriert");
                break;
        }
    }

    public void showTextfield(string text)
    {
        textField.text = text;
    }
}
