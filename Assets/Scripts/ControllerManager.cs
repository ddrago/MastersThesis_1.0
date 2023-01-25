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
    public LogsManager logsManager;
    public ExperimentManager experimentManager;
    Text PseudoConsole;


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
        mid.performed += context => ControlsButtonPress("maps");
        up.performed += context => ControlsButtonPress("calls");
        down.performed += context => ControlsButtonPress("music");
        right.performed += context => ControlsButtonPress("East");
        left.performed += context => ControlsButtonPress("West");
    }


    // Start is called before the first frame update
    void Start()
    {
        PseudoConsole = GameObject.Find("PseudoConsole").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void ControlsButtonPress(string item)
    {
        Debug.Log(item + " Pressed!");
        PseudoConsole.text = item;
        experimentManager.SelectItem(item);
        //logsManager.LogOnCSV("[REMOTE]", buttonName, "-", true);
    }

}
