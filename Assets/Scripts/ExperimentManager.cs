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

    [Header("Menu Manager")]
    public GameObject VoiceConditionButton;
    public GameObject TouchscreenConditionButton;
    public GameObject ControllerConditionButton;
    public GameObject GesturesConditionButton;
    public GameObject MainMenu;
    public GameObject VoiceMenu;
    public GameObject TouchscreenMenu;
    public GameObject ControllerMenu;
    public GameObject GesturesMenu;

    // TODO: update with better instructions if necessary
    private static List<string> instructions_to_give = new List<string>(new string[] { "music", "calls", "maps", "news", "weather", "terrain" });
    private string next_instruction;

    private static System.Random rnd = new System.Random();

    // Mid-study variables
    private int currentInstructionItem = 0;
    private string currentCondition;
    private bool studyCurrentlyOngoing = false;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartExperiment()
    {
        instructionGiver.text = "Loading...";
        logsManager.LogOnCSV("[START EXPERIMENT]", "N/A", "N/A", true);
    }

    public void StartCondition(string condition)
    {
        currentInstructionItem = 0;
        currentCondition = condition;
        studyCurrentlyOngoing = true;

        // TODO: will need to multiplicate the instructions (2, 3 or 4 times?)
        List<string> longer_instruction_list = new List<string>();
        for(int i = 0; i < instructionMultiplicationNumber; i++)
        {
            longer_instruction_list.AddRange(instructions_to_give);
        }

        mirrorUIManager.ServerSetInstructions(longer_instruction_list,
                                              instructionGiver,
                                              currentInstructionItem,
                                              condition);
        /*
        instructions.Clear();
        instructions.AddRange(longer_instruction_list.OrderBy(a => rnd.Next()).ToList());

        instructionGiver.text = instructions[currentInstructionItem];
        Debug.Log("SERVER: " + instructions[currentInstructionItem]);
        TargetUpdateInstructionGiver(instructions[currentInstructionItem]);
        //NextInstruction();

        logsManager.LogOnCSV(string.Format("[START {0} CONDITION]", condition.ToUpper()), "N/A", "N/A", true);
        logsManager.LogInstructions(instructions.ToList<string>());
        */
    }

    public void updateInstructionGiver(string instruction)
    {
        instructionGiver.text = instruction;
    }

    void NextInstruction()
    {
        currentInstructionItem += 1;

        //Debug.Log("i: " + currentInstructionItem + "count: " + instructions.Count);

        if (currentInstructionItem < mirrorUIManager.GetInstructions().Count)
        {
            instructionGiver.text = mirrorUIManager.GetInstructions()[currentInstructionItem];
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
        GesturesConditionButton.GetComponent<Button>().interactable = status;
    }

    public void GoBackToMainMenu()
    {
        // We want to be able to see the main menu
        MainMenu.SetActive(true);

        // And we want any other condition menu to disappear
        VoiceMenu.SetActive(false);
        TouchscreenMenu.SetActive(false);
        ControllerMenu.SetActive(false);
        GesturesMenu.SetActive(false);
    }

    public void SelectItem(string item)
    {
        if (studyCurrentlyOngoing)
        {
            // User selected the correct item
            bool targetItemWasSelected = item.ToLower().Equals(mirrorUIManager.GetInstructions()[currentInstructionItem].ToLower());
            Debug.Log(string.Format("item: {0}, target: {1}, isCorrect: {2}", item, mirrorUIManager.GetInstructions()[currentInstructionItem], targetItemWasSelected));
            logsManager.LogOnCSV(string.Format("[{0}]", currentCondition.ToUpper()), item, mirrorUIManager.GetInstructions()[currentInstructionItem], targetItemWasSelected);
            NextInstruction();
        }
        else Debug.Log("WARNING: Before providing input, please select a condition.");
    }
}
