using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentManager : MonoBehaviour
{
    public LogsManager logsManager;
    public Text instructionGiver;
    public GameObject StartConditionButton;
    public GameObject VoiceConditionButton;
    public GameObject TouchscreenConditionButton;
    public GameObject ControllerConditionButton;
    public GameObject GesturesConditionButton;

    // TODO: update with better instructions if necessary
    private static List<string> instructions_to_give = new List<string>(new string[] { "music", "calls", "maps" });
    private static List<string> instructions;
    private string next_instruction;

    private static System.Random rnd = new System.Random();

    private int currentInstructionItem = 0;
    private string currentCondition;

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

        // TODO: will need to multiplicate the instructions (2, 3 or 4 times?)
        List<string> longer_instruction_list = new List<string>();
        for(int i = 0; i < 3; i++)
        {
            longer_instruction_list.AddRange(instructions_to_give);
        }
        Debug.Log(longer_instruction_list.Count);

        instructions = new List<string>(longer_instruction_list);

        instructions = instructions.OrderBy(a => rnd.Next()).ToList();
        instructionGiver.text = instructions[currentInstructionItem];
        //NextInstruction();

        logsManager.LogOnCSV(string.Format("[START {0} CONDITION]", condition.ToUpper()), "N/A", "N/A", true);
        logsManager.LogInstructions(instructions);
    }

    void NextInstruction()
    {
        currentInstructionItem += 1;

        //Debug.Log("i: " + currentInstructionItem + "count: " + instructions.Count);
        
        if (currentInstructionItem < instructions.Count)
        {
            instructionGiver.text = instructions[currentInstructionItem];
        }
        else
        {
            // TODO: this is just temporary
            currentInstructionItem = 0;
            Debug.Log("TODO: EndCondition()");
        }
    }

    public void SetStartButtonInteractiveStatus(bool status) 
    {
        StartConditionButton.GetComponent<Button>().interactable = status;
    }

    public void SetConditionButtonsInteractiveStatus(bool status)
    {
        VoiceConditionButton.GetComponent<Button>().interactable = status;
        TouchscreenConditionButton.GetComponent<Button>().interactable = status;
        ControllerConditionButton.GetComponent<Button>().interactable = status;
        GesturesConditionButton.GetComponent<Button>().interactable = status;
    }

    public void SelectItem(string item)
    {
        // User selected the correct item
        bool targetItemWasSelected = item.ToLower().Equals(instructions[currentInstructionItem].ToLower());
        Debug.Log(string.Format("item: {0}, target: {1}, isCorrect: {2}", item, instructions[currentInstructionItem], targetItemWasSelected));
        logsManager.LogOnCSV(string.Format("[{0}]", currentCondition.ToUpper()), item, instructions[currentInstructionItem], targetItemWasSelected);
        NextInstruction();
    }
}
