using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogsManager : MonoBehaviour
{
    public int participantNumber;

    //The name of the logging files - MODIFY FOR EACH PARTICIPANT
    private  readonly string baseFileName = "log.csv";
    private  string filename;
    private  readonly string baseTiltPathFilename = "gazepath.csv";
    private string tiltPathFilename;

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
            "[START]" + "," + DateTime.Now.ToString() + "," + DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond + "," + "N/A" + "," + "N/A" + "," + "N/A"
        });

        Debug.Log(Application.persistentDataPath);

        // Log the gaze path directional data
        tiltPathFilename = Application.persistentDataPath + "/" + participantNumber + "_" + baseTiltPathFilename;
        System.IO.File.WriteAllLines(tiltPathFilename, new string[] {
            "timestamp,tilt"
        });
    }

    public void LogOnCSV(string interactionType, string item, string itemToSelect, bool isCorrectItem)
    {
        System.IO.File.AppendAllLines(filename, new string[] {
            interactionType + "," + DateTime.Now.ToString() + "," + (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString() + "," + item + "," + itemToSelect + "," + isCorrectItem
        });
    }

    public void LogGazePath()
    {
        /*if (Physics.Raycast(_ray, out _hitInfo, 100))
        {
            Debug.Log(_hitInfo.point.ToString());
            System.IO.File.AppendAllLines(gazePathFilename, new string[] {
                _hitInfo.point.x.ToString() + "," +  _hitInfo.point.y.ToString() + "," +  _hitInfo.point.z.ToString() + "," + DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond + "," + _hitInfo.collider.gameObject.name
            });
        }*/

        
    }
}
