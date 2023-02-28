using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TouchscreenMenu : MonoBehaviour
{
    public ExperimentManager experimentManager;
    public Text pseudoConsole;
    string consolePreMessage = "Last input: ";

    public Scrollbar scrollbar;

    private static System.Random rnd = new System.Random();

    private void Start()
    {
        scrollbar.value = 1; // At the start, we should see the first element of the list of buttons
        //ShuffleTouchscreenMenuItems();
    }

    public void OnStartCondition()
    {
        ShuffleTouchscreenMenuItems();
    }

    public string GetInstructionCorrespondingToIndex(int currentInstructionIndex)
    {
        //return currentInstructionName
        GameObject[] items = GameObject.FindGameObjectsWithTag("TouchscreenMenuSelectableItems")
            .OrderBy(obj => obj.name, new AlphanumComparatorFast())
            .ToArray();

        return items[currentInstructionIndex].GetComponentInChildren<Text>().text;
    }

    public void ShuffleTouchscreenMenuItems()
    {
        if (experimentManager.studyCurrentlyOngoing)
        {
            GameObject[] items = GameObject.FindGameObjectsWithTag("TouchscreenMenuSelectableItems")
                .OrderBy(obj => obj.name, new AlphanumComparatorFast())
                .ToArray();

            //TODO we want to randomly assign one of the six possible button names from a copied list of the names
            List<string> names = experimentManager.GetCopyOfInstructionNames().OrderBy(a => rnd.Next()).ToList();

            // assign one of its string elements to each items text
            if (names.Count != items.Length)
                Debug.LogError(string.Format("In ShuffleTouchscreenMenuItems: different number of buttons ({0}) and respective names! ({1})", items.Length, names.Count));
            else
                for (int i = 0; i < names.Count; i++)
                    items[i].GetComponentInChildren<Text>().text = names[i];
        }
    }

    public void ButtonPress(GameObject go)
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("TouchscreenMenuSelectableItems")
            .OrderBy(obj => obj.name, new AlphanumComparatorFast())
            .ToArray<GameObject>();

        int i = -1;
        if (items.Contains<GameObject>(go))
            i = System.Array.IndexOf(items, go);
        else
        {
            Debug.LogError("ERROR: either button items were not retrieved or button name not correct");
            return;
        }

        pseudoConsole.text = consolePreMessage + go.gameObject.GetComponentInChildren<Text>().text;
        experimentManager.SelectItem(
            go.gameObject.GetComponentInChildren<Text>().text,
            items[experimentManager.getCurrentInstruction()].GetComponentInChildren<Text>().text,
            i);

        ShuffleTouchscreenMenuItems();
        experimentManager.NextInstruction();
    }
}
