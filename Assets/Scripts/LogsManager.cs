using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogsManager : MonoBehaviour
{
    [Header("Logging Utilities")]
    public int participantNumber;

    //The name of the logging files - MODIFY FOR EACH PARTICIPANT
    private readonly string baseFileName = "log.csv";
    private string filename;
    private readonly string baseTiltPathFilename = "gazepath.csv";
    private string tiltPathFilename;
    private static string instruction_log_filename = "instructions.txt";

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
        System.IO.File.WriteAllLines(filename, new string[] {
            "InteractionType,Time,TimeMS,Item,itemToSelect,isCorrectItem",
            "[INIT]" + "," + DateTime.Now.ToString() + "," + DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond + "," + "N/A" + "," + "N/A" + "," + "N/A"
        });

        // Log the gaze path directional data
        tiltPathFilename = Application.persistentDataPath + "/" + participantNumber + "_" + baseTiltPathFilename;
        System.IO.File.WriteAllLines(tiltPathFilename, new string[] {
            "timestamp,tilt"
        });
    }

    public void LogOnCSV(string interactionType, string item, string targetItem, bool isCorrectItem)
    {
        System.IO.File.AppendAllLines(filename, new string[] {
            interactionType + "," + DateTime.Now.ToString() + "," + (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString() + "," + item + "," + targetItem + "," + isCorrectItem
        });
    }

    // TODO: This method still needs to be figured out. Should the laptop measure tilt? 
    // Should the android phone log it? Should an additional phone strapped to the frame?
    public void LogTiltPath()
    {
        /*if (Physics.Raycast(_ray, out _hitInfo, 100))
        {
            Debug.Log(_hitInfo.point.ToString());
            System.IO.File.AppendAllLines(gazePathFilename, new string[] {
                _hitInfo.point.x.ToString() + "," +  _hitInfo.point.y.ToString() + "," +  _hitInfo.point.z.ToString() + "," + DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond + "," + _hitInfo.collider.gameObject.name
            });
        }*/

        
    }

    public void LogInstructions(List<string> instructions)
    {
        // TODO make a bit more indicative than just instructions.txt
        //instruction_log_filename = currentInputMethod + "_" + "instructions.txt";
        instruction_log_filename = "instructions.txt";

        instruction_log_filename = Application.persistentDataPath + "/" + instruction_log_filename;
        Debug.Log(instruction_log_filename);

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
