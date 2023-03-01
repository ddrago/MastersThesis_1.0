using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentManager : MonoBehaviour
{
    [Header("Experiment variables")]
    public int instructionMultiplicationNumber = 3;

    [Header("GameObject Elements")]
    public LogsManager logsManager;
    public Text instructionGiver;
    public MirrorUIManager mirrorUIManager;
    public TouchscreenMenu touchscreenMenu;
    public ControllerManager controllerManager;
    public VoiceCommands voiceManager;

    [Header("Menu Manager")]
    public GameObject VoiceConditionButton;
    public GameObject TouchscreenConditionButton;
    public GameObject ControllerConditionButton;
    //public GameObject GesturesConditionButton;
    public GameObject MainMenu;
    public GameObject VoiceMenu;
    public GameObject TouchscreenMenu;
    public GameObject ControllerMenu;
    //public GameObject GesturesMenu;

    private static List<string> instructions_to_give = new List<string>(new string[] { "Music", "Calls", "Maps", "News", "Weather", "Terrain" });
    private static List<int> index_instructions_to_give = new List<int>(new int[] { 0, 1, 2, 3, 4, 5 });
    private string next_instruction;

    // Mid-study variables
    private int turnNumber = 0;
    private string currentCondition;
    public bool studyCurrentlyOngoing = false;

    private static System.Random rnd = new System.Random();

    public void StartExperiment()
    {
        instructionGiver.text = "Loading...";
        logsManager.LogOnCSV("[START EXPERIMENT]", "N/A", "N/A", 404, 404, true);

        // Set the list of instructions for the participants (for all conditions)
        mirrorUIManager.ServerSetInstructions(index_instructions_to_give, instructions_to_give, instructionMultiplicationNumber);
    }

    public void StartCondition(string condition)
    {
        turnNumber = 0;
        currentCondition = condition;
        studyCurrentlyOngoing = true;

        // If the condition is either controller or touchscreen, take the string instructions list and
        // randomize it. Then update the name of the buttons on the screen to follow this list in the
        // same order. 

        switch (condition)
        {
            case "Voice":
                next_instruction = mirrorUIManager.GetVoiceInstructions()[turnNumber];
                break;
            case "Touchscreen":
                touchscreenMenu.OnStartCondition();
                next_instruction = touchscreenMenu.GetInstructionCorrespondingToIndex(getCurrentInstruction());
                break;
            case "Controller":
                controllerManager.OnStartCondition();
                next_instruction = controllerManager.GetInstructionCorrespondingToIndex(getCurrentInstruction());
                break;
        }

        //Show the instruction 
        UpdateInstructionGiver(next_instruction);
        
        // Log everything
        logsManager.LogOnCSV(string.Format("[START {0} CONDITION]", condition.ToUpper()), "N/A", "N/A", 404, 404, true);
        logsManager.LogInstructions(mirrorUIManager.GetInstructions());
    }

    public void UpdateInstructionGiver(string instruction)
    {
        instructionGiver.text = instruction;
    }

    public void NextInstruction()
    {
        turnNumber += 1;

        if (mirrorUIManager.GetInstructions().Count != mirrorUIManager.GetVoiceInstructions().Count)
        {
            Debug.LogError("ERROR: index-based instructions and name-based ones should have the same amount of elements");
            return;
        }

        if (turnNumber < mirrorUIManager.GetInstructions().Count)
        {
            switch (currentCondition)
            {
                case "Voice":
                    next_instruction = mirrorUIManager.GetVoiceInstructions()[turnNumber];
                    break;
                case "Touchscreen":
                    next_instruction = touchscreenMenu.GetInstructionCorrespondingToIndex(getCurrentInstruction());
                    break;
                case "Controller":
                    next_instruction = controllerManager.GetInstructionCorrespondingToIndex(getCurrentInstruction());
                    break;
            }

            UpdateInstructionGiver(next_instruction);
        }
        else EndCondition();
    }

    private void EndCondition()
    {
        studyCurrentlyOngoing = false;
        if (currentCondition == "Voice")
            voiceManager.StopKeywordRecognizer();

        logsManager.LogOnCSV(string.Format("[END {0} CONDITION]", currentCondition.ToUpper()), "N/A", "N/A", 404, 404, true);

        instructionGiver.text = "Loading...";

        GoBackToMainMenu();
    }

    public void SetConditionButtonsInteractiveStatus(bool status)
    {
        VoiceConditionButton.GetComponent<Button>().interactable = status;
        TouchscreenConditionButton.GetComponent<Button>().interactable = status;
        ControllerConditionButton.GetComponent<Button>().interactable = status;
        //GesturesConditionButton.GetComponent<Button>().interactable = status;
    }

    public void GoBackToMainMenu()
    {
        // We want to be able to see the main menu
        MainMenu.SetActive(true);

        // And we want any other condition menu to disappear
        VoiceMenu.SetActive(false);
        TouchscreenMenu.SetActive(false);
        ControllerMenu.SetActive(false);
        //GesturesMenu.SetActive(false);
    }

    public void SelectItem(string item, string targetItem, int i)
    {
        if (studyCurrentlyOngoing)
        {
            FindObjectOfType<AudioManager>().Play("BeepPositive");

            bool targetItemWasSelected = i == getCurrentInstruction();

            Debug.Log(string.Format("item: {0}, target: {1}, index: {2}, targetIndex: {3}, isCorrect: {4}", 
                item,
                targetItem, 
                i, 
                getCurrentInstruction(), 
                targetItemWasSelected));
            
            logsManager.LogOnCSV(
                string.Format("[{0}]", currentCondition.ToUpper()), 
                item,
                targetItem, 
                i, 
                getCurrentInstruction(), 
                targetItemWasSelected);
        }
        else Debug.Log("WARNING: Before providing input, please select a condition.");
    }

    public int getCurrentInstruction()
    {
        return mirrorUIManager.GetInstructions()[turnNumber];
    }

    public void SelectItemVoiceCondition(string item)
    {
        if (studyCurrentlyOngoing)
        {
            string targetItem = mirrorUIManager.GetVoiceInstructions()[turnNumber];

            bool targetItemWasSelected = (item == targetItem.ToLower());

            Debug.Log(string.Format("item: {0}, target: {1}, index: {2}, targetIndex: {3}, isCorrect: {4}",
                item,
                targetItem,
                404,
                404,
                targetItemWasSelected));

            logsManager.LogOnCSV(
                string.Format("[{0}]", currentCondition.ToUpper()),
                item,
                targetItem,
                404,
                404,
                targetItemWasSelected);
        }
        else Debug.Log("WARNING: Before providing input, please select a condition.");
    }

    public List<string> GetCopyOfInstructionNames()
    {
        return new List<string>(instructions_to_give);
    }
}
