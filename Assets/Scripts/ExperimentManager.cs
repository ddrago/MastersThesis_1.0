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

    private static List<string> instructions_to_give = new List<string>(new string[] { "Music", "News", "Podcasts", "Sports" });
    private static List<string> instructions;
    private string next_instruction;

    private static System.Random rnd = new System.Random();

    private int currentInstructionItem = 0;

    private void Awake()
    {
        // Test 
        StartCondition();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCondition()
    {
        currentInstructionItem = 0;
        instructions = new List<string>(instructions_to_give);
        // TODO: will need to multiplicate the instructions (2, 3 or 4 times?)

        instructions = instructions.OrderBy(a => rnd.Next()).ToList();
        //Debug.Log(string.Join(",", instructions.ToArray()));

        next_instruction = instructions[currentInstructionItem];
        instructionGiver.text = next_instruction;

        // TODO: get the current condition
        logsManager.LogOnCSV("[START <TODO:CONDITION>]", "N/A", "N/A", true);
        logsManager.LogInstructions(instructions);
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
}
