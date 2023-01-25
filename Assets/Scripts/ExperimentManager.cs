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

    private static List<string> instructions_to_give = new List<string>(new string[] { "Music", "News", "Maps", "Sports" });
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
        
        instructions = new List<string>(instructions_to_give);
        // TODO: will need to multiplicate the instructions (2, 3 or 4 times?)

        instructions = instructions.OrderBy(a => rnd.Next()).ToList();
        instructionGiver.text = instructions[currentInstructionItem];
        //NextInstruction();

        logsManager.LogOnCSV(string.Format("[START {0} CONDITION]", condition.ToUpper()), "N/A", "N/A", true);
        logsManager.LogInstructions(instructions);
    }

    void NextInstruction()
    {
        if (currentInstructionItem < instructions.Count)
        {
            instructionGiver.text = instructions[currentInstructionItem];
            currentInstructionItem += 1;
        }
        else
        {
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

    public void loadNextInstruction()
    {
        next_instruction = instructions[currentInstructionItem];
        instructionGiver.text = next_instruction;
    }

    public void SelectItem(string item)
    {
        // User selected the correct item
        if ((item.ToLower()).Equals(instructions[currentInstructionItem].ToLower()))
        {
            Debug.Log("Whoohooo!");
        }
        else
        {
            Debug.Log("uh-oh, spaghetti-os");
        }
    }
}
