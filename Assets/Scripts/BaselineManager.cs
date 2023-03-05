using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaselineManager : MonoBehaviour
{
    [Header("Scene objects")]
    public GameObject pleaseSelectItem;
    public GameObject instructionGiver;
    public GameObject pseudoConsole;
    public Text mainText;

    [Header("Other scripts")]
    public ExperimentManager experimentManager;

    [Header("Condition variables")]
    public float baselineDuration = 45f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartCondition()
    {
        pleaseSelectItem.SetActive(false);
        instructionGiver.SetActive(false);
        pseudoConsole.SetActive(false);


        Invoke("ExitCondition", baselineDuration);
        Invoke("PleaseStopCycling", baselineDuration-3f);
    }

    public void ExitCondition()
    {
        experimentManager.EndCondition();

        pleaseSelectItem.SetActive(true);
        instructionGiver.SetActive(true);
        pseudoConsole.SetActive(true);
    }

    private void PleaseStopCycling()
    {
        mainText.text = "Ok, feel free to stop.";
    }
}
