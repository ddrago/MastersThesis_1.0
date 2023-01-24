using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentManager : MonoBehaviour
{
    public LogsManager logsManager;
    public Text instructionGiver;

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

        // TODO: need to initialise it to something at some point
        next_instruction = instructions[currentInstructionItem];
        instructionGiver.text = next_instruction;

        logsManager.LogInstructions(instructions);
    }

}
