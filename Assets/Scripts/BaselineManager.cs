using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaselineManager : MonoBehaviour
{
    public GameObject pleaseSelectItem;
    public GameObject instructionGiver;
    public GameObject pseudoConsole;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartCondition()
    {
        pleaseSelectItem.SetActive(false);
        instructionGiver.SetActive(false);
        pseudoConsole.SetActive(false);
    }

    public void OnExitCondition()
    {
        pleaseSelectItem.SetActive(true);
        instructionGiver.SetActive(true);
        pseudoConsole.SetActive(true);
    }
}
