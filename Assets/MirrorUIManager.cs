using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class MirrorUIManager : NetworkBehaviour
{
    // For whatever reason having a general "OnButtonPress" doesn't work :(

    [Header("Experiment Menu objects")]
    public Text pseudoConsole;
    public Button StartExperimentButton;

    [Header("Main Menu objects")]
    public Button ControllerExperimentButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //--START EXPERIMENT BUTTON--
    public void OnStartExperiment()
    {
        CmdPressStartExperimentButton();
    }

    [Command(requiresAuthority = false)]
    void CmdPressStartExperimentButton()
    {
        StartExperimentButton.onClick.Invoke();
        //pseudoConsole.text = "Start Experiment Button pressed"; // do stuff on server
    }

    //--CONTROLLER BUTTON--
    public void OnControllerExperiment()
    {
        CmdPressControllerExperimentButton();
    }

    [Command(requiresAuthority = false)]
    void CmdPressControllerExperimentButton()
    {
        ControllerExperimentButton.onClick.Invoke();
        //pseudoConsole.text = "Start Experiment Button pressed"; // do stuff on server
    }

    [Command(requiresAuthority = false)]
    void CmdDoServerStuff(bool newValue)
    {
        pseudoConsole.text = newValue.ToString(); // do stuff on server
    }
}
