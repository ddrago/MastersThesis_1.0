using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        mid.performed += context => ControlsButtonPress("Mid");
        up.performed += context => ControlsButtonPress("North");
        down.performed += context => ControlsButtonPress("South");
        right.performed += context => ControlsButtonPress("East");
        left.performed += context => ControlsButtonPress("West");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // NB: the 'buttonName' parameter is only temporary, it will be the proper item name 
    // Which I haven't chosen yet
    void ControlsButtonPress(string buttonName)
    {
        Debug.Log(buttonName + " Pressed!");
        logsManager.LogOnCSV("[REMOTE]", buttonName, "-", true);
    }

}
