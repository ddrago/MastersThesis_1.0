using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
    public LogsManager logsManager;

    private static List<string> instructions_to_give = new List<string>(new string[] { "Music", "News", "Podcasts", "Sports" });
    private static List<string> instructions;
    private string next_instruction;

    private static System.Random rnd = new System.Random();

    private void Awake()
    {
        // Test 
        startCondition();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startCondition()
    {
        instructions = new List<string>(instructions_to_give);
        instructions = instructions.OrderBy(a => rnd.Next()).ToList();
        //Debug.Log(string.Join(",", instructions.ToArray()));
        next_instruction = null;

        logsManager.LogInstructions(instructions);
    }
}
