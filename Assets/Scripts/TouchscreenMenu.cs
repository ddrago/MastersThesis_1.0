using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TouchscreenMenu : MonoBehaviour
{
    public ExperimentManager experimentManager;
    public Text pseudoConsole;

    private static System.Random rnd = new System.Random();

    private void Start()
    {
        ShuffleTouchscreenMenuItems();
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

    // Mirror stuff
    /*
    private bool button1IsPressed = false;
    public void PressButton1()
    {
        button1IsPressed = true;
    }

    public bool getButton1IsPressed()
    {
        if (button1IsPressed)
        {
            button1IsPressed = false;
            return true;
        }

        return false;
    }*/


}
