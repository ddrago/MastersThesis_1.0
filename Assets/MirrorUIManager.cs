using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System.Linq;
using System;

public class MirrorUIManager : NetworkBehaviour
{
    // For whatever reason having a general "OnButtonPress" doesn't work :(

    [Header("Experiment Menu objects")]
    public Text pseudoConsole;
    public Button StartExperimentButton;

    [Header("Main Menu objects")]
    public Button VoiceExperimentButton;
    public Button TouchscreenExperimentButton;
    public Button ControllerExperimentButton;
    public Button GesturesExperimentButton;

    [Header("Back Buttons")]
    public Button VoiceBackButton;
    public Button TouchscreenBackButton;
    public Button ControllerBackButton;
    public Button GesturesBackButton;

    [Header("Touchscreen Menu objects")]
    public Button TouchscreenButton0;
    public Button TouchscreenButton1;
    public Button TouchscreenButton2;
    public Button TouchscreenButton3;
    public Button TouchscreenButton4;
    public Button TouchscreenButton5;

    private static System.Random rnd = new System.Random();
    public LogsManager logsManager;
    public ExperimentManager experimentManager;

    // Useful variables
    public readonly SyncList<string> instructions = new SyncList<string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region buttonSyncing

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

    //--VOICE BUTTON--
    public void OnVoiceExperiment()
    {
        CmdPressVoiceExperimentButton();
    }

    [Command(requiresAuthority = false)]
    void CmdPressVoiceExperimentButton()
    {
        VoiceExperimentButton.onClick.Invoke();
        //pseudoConsole.text = "Start Experiment Button pressed"; // do stuff on server
    }

    //--TOUCHSCREEN BUTTON--
    public void OnTouchscreenExperiment()
    {
        CmdPressTouchscreenExperimentButton();
    }

    [Command(requiresAuthority = false)]
    void CmdPressTouchscreenExperimentButton()
    {
        TouchscreenExperimentButton.onClick.Invoke();
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

    //--GESTURES BUTTON--
    public void OnGesturesExperiment()
    {
        CmdPressGesturesExperimentButton();
    }

    [Command(requiresAuthority = false)]
    void CmdPressGesturesExperimentButton()
    {
        GesturesExperimentButton.onClick.Invoke();
        //pseudoConsole.text = "Start Experiment Button pressed"; // do stuff on server
    }

    //--VOICE BACK BUTTON--
    public void OnVoiceBack()
    {
        CmdPressVoiceBackButton();
    }

    [Command(requiresAuthority = false)]
    void CmdPressVoiceBackButton()
    {
        VoiceBackButton.onClick.Invoke();
        //pseudoConsole.text = "Start Experiment Button pressed"; // do stuff on server
    }

    //--TOUCHSCREEN BACK BUTTON--
    public void OnTouchscreenBack()
    {
        CmdPressTouchscreenBackButton();
    }

    [Command(requiresAuthority = false)]
    void CmdPressTouchscreenBackButton()
    {
        TouchscreenBackButton.onClick.Invoke();
        //pseudoConsole.text = "Start Experiment Button pressed"; // do stuff on server
    }

    //--CONTROLLER BACK BUTTON--
    public void OnControllerBack()
    {
        CmdPressControllerBackButton();
    }

    [Command(requiresAuthority = false)]
    void CmdPressControllerBackButton()
    {
        ControllerBackButton.onClick.Invoke();
        //pseudoConsole.text = "Start Experiment Button pressed"; // do stuff on server
    }

    //--GESTURES BACK BUTTON--
    public void OnGesturesBack()
    {
        CmdPressGesturesBackButton();
    }

    [Command(requiresAuthority = false)]
    void CmdPressGesturesBackButton()
    {
        GesturesBackButton.onClick.Invoke();
        //pseudoConsole.text = "Start Experiment Button pressed"; // do stuff on server
    }

    //--TOUCHSCREEN BUTTON 0--
    public void OnTouchscreenButton0()
    {
        CmdPressTouchscreenButton0();
    }

    [Command(requiresAuthority = false)]
    void CmdPressTouchscreenButton0()
    {
        TouchscreenButton0.onClick.Invoke();
        //pseudoConsole.text = "Start Experiment Button pressed"; // do stuff on server
    }

    //--TOUCHSCREEN BUTTON 1--
    public void OnTouchscreenButton1()
    {
        CmdPressTouchscreenButton1();
    }

    [Command(requiresAuthority = false)]
    void CmdPressTouchscreenButton1()
    {
        TouchscreenButton1.onClick.Invoke();
        //pseudoConsole.text = "Start Experiment Button pressed"; // do stuff on server
    }

    //--TOUCHSCREEN BUTTON 2--
    public void OnTouchscreenButton2()
    {
        CmdPressTouchscreenButton2();
    }

    [Command(requiresAuthority = false)]
    void CmdPressTouchscreenButton2()
    {
        TouchscreenButton2.onClick.Invoke();
        //pseudoConsole.text = "Start Experiment Button pressed"; // do stuff on server
    }

    //--TOUCHSCREEN BUTTON 3--
    public void OnTouchscreenButton3()
    {
        CmdPressTouchscreenButton3();
    }

    [Command(requiresAuthority = false)]
    void CmdPressTouchscreenButton3()
    {
        TouchscreenButton3.onClick.Invoke();
        //pseudoConsole.text = "Start Experiment Button pressed"; // do stuff on server
    }

    //--TOUCHSCREEN BUTTON 4--
    public void OnTouchscreenButton4()
    {
        CmdPressTouchscreenButton4();
    }

    [Command(requiresAuthority = false)]
    void CmdPressTouchscreenButton4()
    {
        TouchscreenButton4.onClick.Invoke();
        //pseudoConsole.text = "Start Experiment Button pressed"; // do stuff on server
    }

    //--TOUCHSCREEN BUTTON 5--
    public void OnTouchscreenButton5()
    {
        CmdPressTouchscreenButton5();
    }

    [Command(requiresAuthority = false)]
    void CmdPressTouchscreenButton5()
    {
        TouchscreenButton5.onClick.Invoke();
        //pseudoConsole.text = "Start Experiment Button pressed"; // do stuff on server
    }

    #endregion

    [Command(requiresAuthority = false)]
    void CmdDoServerStuff(bool newValue)
    {
        pseudoConsole.text = newValue.ToString(); // do stuff on server
    }

    #region Instructions

    [ServerCallback]
    public void ServerSetInstructions(List<string> longer_instruction_list, Text instructionGiver, int currentInstructionItem, string condition)
    {
        instructions.Clear();
        instructions.AddRange(longer_instruction_list.OrderBy(a => rnd.Next()).ToList());

        instructionGiver.text = instructions[currentInstructionItem];
        Debug.Log("SERVER: " + instructions[currentInstructionItem]);
        RpcUpdateInstructions(instructions[currentInstructionItem]);
        //TargetUpdateInstructionGiver(instructions[currentInstructionItem], instructionGiver);
        //NextInstruction();

        logsManager.LogOnCSV(string.Format("[START {0} CONDITION]", condition.ToUpper()), "N/A", "N/A", true);
        logsManager.LogInstructions(instructions.ToList<string>());
    }

    [ClientRpc]
    public void RpcUpdateInstructions(string instruction)
    {
        Debug.Log("Checkpoint");
        experimentManager.updateInstructionGiver(instruction);
        Debug.Log("CLIENT: " + instruction);
    }

    public List<string> GetInstructions()
    {
        return instructions.ToList<string>();
    }

    /*
        [TargetRpc]
        public void TargetUpdateInstructionGiver(string instruction, Component instructionGiver)
        {
            Debug.Log("Will it work?");
            instructionGiver.GetComponent<Text>().text = instruction;
            Debug.Log("CLIENT: " + instruction);
        }*/

    #endregion

    #region tiltLogging

    [ClientCallback]
    public void logTilt(Vector3 acceleration)
    {
        logsManager.LogTilt(acceleration);
    }

    #endregion
}
