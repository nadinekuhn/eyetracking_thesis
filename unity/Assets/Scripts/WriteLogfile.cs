using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WriteLogfile : MonoBehaviour
{

    public static void writeToFile()
    {
        
        print("WRITE FILE\n");

        string path = "LOG/eye_tracking_study_log_test_1.txt";
        if(!Directory.Exists("LOG/"))
        {
            Directory.CreateDirectory("LOG/");
        }

        if (!File.Exists(path))
        {
            // Create file to write to
            print("CREATE FILE");
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("person|scenario|selection-method|eyetracking|object|selectiontime|errortime|errorcounter");
            }
        }
        
        using(StreamWriter sw = File.AppendText(path))
        {
            sw.WriteLine($"1|{SceneManager.GetActiveScene().name}|{SelectionManager.selectionMethod}|off|{SpawnSelectableObject.getNumberOfSelection()}|{SelectionManager.getSelectionTimer()%60}s|{SelectionManager.getErrorTimer()%60}s|{SelectionManager.getErrorCount()}");
        }
    }
}
