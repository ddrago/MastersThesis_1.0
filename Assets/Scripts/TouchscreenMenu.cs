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

    public void ShuffleTouchscreenMenuItems()

    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("TouchscreenMenuSelectableItems");

        int[] indexList = new int[items.Length]; //.OrderBy(a => rnd.Next()).ToList();
        for (int i = 0; i < items.Length; i++) indexList[i] = i;
        indexList = indexList.OrderBy(a => rnd.Next()).ToArray();
        //GameObject[] countOrdered = count.OrderBy(go => go.name).ToArray();

        for (int i = 0; i < items.Length; i++)
        {
            items[i].transform.SetSiblingIndex(indexList[i]);
        }
    }

    public void buttonPress(string item)
    {
        pseudoConsole.text = item;
        experimentManager.SelectItem(item);
    }

    internal void updateButtonNames(List<string> button_names)
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("TouchscreenMenuSelectableItems");
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
