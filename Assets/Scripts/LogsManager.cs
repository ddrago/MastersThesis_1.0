using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
//using Mirror;

public class LogsManager : MonoBehaviour
{
    [Header("Logging Utilities")]
    public int participantNumber;

    //The name of the logging files - MODIFY FOR EACH PARTICIPANT
    private readonly string baseFileName = "log.csv";
    private string filename;
    private readonly string baseTiltPathFilename = "tilt.csv";
    private string tiltPathFilename;
    private static string instruction_log_filename = "instructions.txt";

    // GPS stuff to get the speed of the user
    public bool isUpdating;

    // Called before Start
    private void Awake()
    {
        InitLogging();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitLogging()
    {
        // Log the browing and selection data
        filename = Application.persistentDataPath + "/" + participantNumber + "_" + baseFileName;
        
        Debug.Log(filename);

        System.IO.File.WriteAllLines(filename, new string[] {
            "InteractionType,Time,TimeMS,Item,itemToSelect,isCorrectItem",
            "[INIT]" + "," + DateTime.Now.ToString() + "," + DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond + "," + "N/A" + "," + "N/A" + "," + "N/A"
        });

        // Log the gaze path directional data
        tiltPathFilename = Application.persistentDataPath + "/" + participantNumber + "_" + baseTiltPathFilename;
        System.IO.File.WriteAllLines(tiltPathFilename, new string[] {
            "Time,TimeMS,TiltX,TiltY,TiltZ"
        });
    }

    public void LogOnCSV(string interactionType, string item, string targetItem, bool isCorrectItem)
    {
        System.IO.File.AppendAllLines(filename, new string[] {
            interactionType + "," + DateTime.Now.ToString() + "," + (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString() + "," + item + "," + targetItem + "," + isCorrectItem
        });
    }

    public void LogTilt(Vector3 tilt)
    {
        System.IO.File.AppendAllLines(tiltPathFilename, new string[] {
            DateTime.Now.ToString() + "," + (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString() + "," + tilt.x + "," + tilt.y + "," + tilt.z
        });
    }

    public void LogInstructions(List<string> instructions)
    {
        // TODO make a bit more indicative than just instructions.txt
        //instruction_log_filename = currentInputMethod + "_" + "instructions.txt";
        instruction_log_filename = "instructions.txt";

        instruction_log_filename = Application.persistentDataPath + "/" + instruction_log_filename;
        //Debug.Log(instruction_log_filename);

        System.IO.FileInfo theSourceFile = new System.IO.FileInfo(instruction_log_filename);
        System.IO.File.WriteAllText(instruction_log_filename, string.Join(",", instructions.ToArray()));
        
        //Just a debug functionality to make sure the file exists
        /*if (System.IO.File.Exists(instruction_log_filename))
        {
            System.IO.StreamReader reader = theSourceFile.OpenText();
            string text = reader.ReadLine();
            print(text);
        }*/
    }
}
