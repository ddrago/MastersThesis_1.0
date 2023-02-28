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
    public ControllerManager controllerManager;
    public VoiceCommands voiceCommands;

    // Useful variables
    //public readonly SyncList<string> instructions = new SyncList<string>();
    public readonly SyncList<int> instructions = new SyncList<int>();
    public readonly SyncList<string> voice_instructions = new SyncList<string>();

    [SyncVar(hook = nameof(UpdateCurrentControllerItemIndex))]
    public int currentControllerItemIndex = 0;

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
    }

    #endregion

    [Command(requiresAuthority = false)]
    void CmdDoServerStuff(bool newValue)
    {
        pseudoConsole.text = newValue.ToString(); // do stuff on server
    }

    #region Instructions

    [ServerCallback]
    //public void ServerSetInstructions(List<string> longer_instruction_list, Text instructionGiver, int currentInstructionItem, string condition)
    public void ServerSetInstructions(List<int> index_instructions_to_give, List<string> instructions_to_give, int instructionMultiplicationNumber)
    {
        instructions.Clear();

        List<int> longer_index_instruction_list = new List<int>();
        List<string> longer_instruction_list = new List<string>();

        for (int i = 0; i < instructionMultiplicationNumber; i++)
        {
            longer_index_instruction_list.AddRange(index_instructions_to_give);
            longer_instruction_list.AddRange(instructions_to_give);
        }

        instructions.AddRange(longer_index_instruction_list.OrderBy(a => rnd.Next()).ToList());
        voice_instructions.AddRange(longer_instruction_list.OrderBy(a => rnd.Next()).ToList());
    }

    public List<int> GetInstructions()
    {
        return instructions.ToList<int>();
    }

    public List<string> GetVoiceInstructions()
    {
        return voice_instructions.ToList();
    }

    #endregion

    #region ControllerManager

    [Client]
    public void UpdateCurrentControllerItemIndex(int oldIndex, int newIndex)
    {

        //pseudoConsole.text = newIndex.ToString();
        // Here I can update the selected item maybe

        controllerManager.HighlightSelectedItem(newIndex);
        controllerManager.UpdateScrollBar(newIndex);
    }

    public int GetCurrentControllerItemIndex()
    {
        return currentControllerItemIndex;
    }

    public void DecreaseCurrentControllerItemIndex()
    {
        //Deal with the index
        if (currentControllerItemIndex - 1 < 0)
            currentControllerItemIndex = 0;
        else
            currentControllerItemIndex--;
    }

    public void IncreaseCurrentControllerItemIndex(int itemListLength)
    {
        //Deal with the index
        if (currentControllerItemIndex + 1 > itemListLength - 1)
            currentControllerItemIndex = itemListLength - 1;
        else
            currentControllerItemIndex++;
    }

    [ClientRpc]
    public void RpcControlsButtonPress()
    {
        controllerManager.ControlsButtonPress();
    }

    #endregion

    [ClientRpc]
    public void RpcVoiceCommand(string phrase)
    {
        voiceCommands.MirrorPhraseRecognizer(phrase);
    }

    #region tiltLogging

    [ClientCallback]
    public void logTilt(Vector3 acceleration)
    {
        logsManager.LogTilt(acceleration);
    }

    #endregion
}
