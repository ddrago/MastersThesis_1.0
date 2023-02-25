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

    public void ShuffleTouchscreenMenuItems()

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

    public void ButtonPress(GameObject go)
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("TouchscreenMenuSelectableItems")
            .OrderBy(obj => obj.name, new AlphanumComparatorFast())
            .ToArray<GameObject>();

        int i = 0;
        if (items.Contains<GameObject>(go))
            i = System.Array.IndexOf(items, go);
        else
            Debug.LogError("ERROR: either button items were not retrieved or index out of scope somehow");

        pseudoConsole.text = go.gameObject.GetComponentInChildren<Text>().text;
        experimentManager.SelectItem(go.gameObject.GetComponentInChildren<Text>().text, i);
    }

    internal void UpdateButtonNames(List<string> button_names)
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("TouchscreenMenuSelectableItems")
            .OrderBy(obj => obj.name, new AlphanumComparatorFast())
            .ToArray<GameObject>();
        if (items.Length != button_names.Count) {
            Debug.LogError("The two lists do not have the same amount of items!!" + 
                string.Format("\nitems is {0} items long, button_names is {1}.", items.Length, button_names.Count));
            return;
        }

        for (int i = 0; i < items.Length; i++)
        {
            items[i].GetComponentInChildren<Text>().text = button_names[i];
        }
    }
}
