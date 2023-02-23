using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControllerManager : MonoBehaviour
{
    [Header("Controls")]
    public InputAction mid;
    public InputAction up;
    public InputAction down;
    public InputAction right;
    public InputAction left;

    [Header("Utilities")]
    /*public LogsManager logsManager;
    public ExperimentManager experimentManager;*/
    public Text pseudoConsole;

    //List navigation handling
    private GameObject[] items;
    private int currentItemIndex = 0;

    void OnEnable()
    {
        mid.Enable();
        up.Enable();
        down.Enable();
        right.Enable();
        left.Enable();
    }

    void OnDisable()
    {
        mid.Disable();
        up.Disable();
        down.Disable();
        right.Disable();
        left.Disable();
    }

    private void Awake()
    {
        mid.performed += context => Click();
        up.performed += context => MoveUp();
        down.performed += context => MoveDown();
        /*mid.performed += context => ControlsButtonPress("maps");
        up.performed += context => ControlsButtonPress("calls");
        down.performed += context => ControlsButtonPress("music");*/
        right.performed += context => ControlsButtonPress("East");
        left.performed += context => ControlsButtonPress("West");
    }


    // Start is called before the first frame update
    void Start()
    {
        //FOR TESTING PURPOSES
        OnStartControllerExperiment();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnStartControllerExperiment()
    {
        items = GameObject.FindGameObjectsWithTag("ControllerMenuSelectableItems");
        if (items != null) //This may not work i seem to vaguely recall
        {
            Debug.Log(items[currentItemIndex].name);
            items[currentItemIndex].GetComponent<Button>().Select();
            //items[currentItemIndex].SetActive(true);
        }
    }

    private void Click()
    {
        // DEBUG
        //Debug.Log("Select");
        //pseudoConsole.text = currentItemIndex.ToString();
        pseudoConsole.text = "Select";
    }

    private void MoveUp()
    {
        if (currentItemIndex - 1 < 0)
            currentItemIndex = 0;
        else
            currentItemIndex--;

        // DEBUG
        //Debug.Log("Up");
        //pseudoConsole.text = currentItemIndex.ToString();
        //pseudoConsole.text = "Up";

        //visual output
    }

    private void MoveDown()
    {
        if(!(items is null))
        {
            if (currentItemIndex + 1 > items.Length-1)
                currentItemIndex = items.Length - 1;
            else
                currentItemIndex++;
        }

        // DEBUG
        /*Debug.Log("Down");
        pseudoConsole.text = currentItemIndex.ToString();*/
        //pseudoConsole.text = "Down";

        //visual output

    }

    void ControlsButtonPress(string item)
    {
        Debug.Log(item + " Pressed!");
        pseudoConsole.text = item;
        //experimentManager.SelectItem(item);
        //logsManager.LogOnCSV("[REMOTE]", buttonName, "-", true);
    }

}
