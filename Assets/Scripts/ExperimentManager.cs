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

    // TODO: update with better instructions if necessary
    private static List<string> instructions_to_give = new List<string>(new string[] { "Music", "Calls", "Maps", "News", "Weather", "Terrain" });
    private static List<int> index_instructions_to_give = new List<int>(new int[] { 0, 1, 2, 3, 4, 5 });
    private string next_instruction;

    // Mid-study variables
    private int currentInstructionItem = 0;
    private string currentCondition;
    private bool studyCurrentlyOngoing = false;

    private static System.Random rnd = new System.Random();

    public void StartExperiment()
    {
        instructionGiver.text = "Loading...";
        logsManager.LogOnCSV("[START EXPERIMENT]", "N/A", "N/A", true);

        // Set the list of instructions for the participants (for all conditions)
        mirrorUIManager.ServerSetInstructions(index_instructions_to_give, instructionMultiplicationNumber);
    }

    public void StartCondition(string condition)
    {
        currentInstructionItem = 0;
        currentCondition = condition;
        studyCurrentlyOngoing = true;

        // If the condition is either controller or touchscreen, take the string instructions list and
        // randomize it. Then update the name of the buttons on the screen to follow this list in the
        // same order. 
        mirrorUIManager.ServerSetButtonNames(instructions_to_give);
        mirrorUIManager.ShuffleButtonNames();

        if (condition.Equals("Touchscreen")) 
            touchscreenMenu.updateButtonNames(mirrorUIManager.GetButtonNames());
        else if (condition.Equals("Controller"))
            controllerManager.updateButtonNames(mirrorUIManager.GetButtonNames());

        //logsManager.LogOnCSV(string.Format("[START {0} CONDITION]", condition.ToUpper()), "N/A", "N/A", true);
        //TODO: logsManager.LogInstructions(instructions.ToList<string>());
    }

    // TODO
    public void updateInstructionGiver(string instruction)
    {
        instructionGiver.text = instruction;
    }

    void NextInstruction()
    {
        currentInstructionItem += 1;

        if (currentInstructionItem < mirrorUIManager.GetInstructions().Count)
        {
            instructionGiver.text = mirrorUIManager.GetInstructions()[currentInstructionItem].ToString();
        }
        else EndCondition();
    }

    private void EndCondition()
    {
        studyCurrentlyOngoing = false;

        logsManager.LogOnCSV(string.Format("[END {0} CONDITION]", currentCondition.ToUpper()), "N/A", "N/A", true);

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

    public void SelectItem(string item)
    {
        if (studyCurrentlyOngoing)
        {
            // User selected the correct item
            // TODO: REMOVE ToString()!!
            bool targetItemWasSelected = item.ToLower().Equals(mirrorUIManager.GetInstructions()[currentInstructionItem].ToString().ToLower());
            Debug.Log(string.Format("item: {0}, target: {1}, isCorrect: {2}", item, mirrorUIManager.GetInstructions()[currentInstructionItem], targetItemWasSelected));
            logsManager.LogOnCSV(string.Format("[{0}]", currentCondition.ToUpper()), item, mirrorUIManager.GetInstructions()[currentInstructionItem].ToString(), targetItemWasSelected);
            NextInstruction();
        }
        else Debug.Log("WARNING: Before providing input, please select a condition.");
    }
}
