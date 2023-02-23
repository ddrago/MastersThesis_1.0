using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControllerManager : MonoBehaviour
{
    [Header("Controls")]
    public InputAction mid;
    public InputAction up;
    public InputAction down;
    public InputAction right;
    public InputAction left;

    [Header("Utilities")]
    /*public LogsManager logsManager;
    public ExperimentManager experimentManager;*/
    public Text pseudoConsole;
    public Scrollbar scrollbar;

    //List navigation handling
    private GameObject[] items;
    private int currentItemIndex = 0;
    private static System.Random rnd = new System.Random();

    void OnEnable()
    {
        mid.Enable();
        up.Enable();
        down.Enable();
        right.Enable();
        left.Enable();
    }

    void OnDisable()
    {
        mid.Disable();
        up.Disable();
        down.Disable();
        right.Disable();
        left.Disable();
    }

    private void Awake()
    {
        mid.performed += context => Click();
        up.performed += context => MoveUp();
        down.performed += context => MoveDown();
        /*mid.performed += context => ControlsButtonPress("maps");
        up.performed += context => ControlsButtonPress("calls");
        down.performed += context => ControlsButtonPress("music");*/
        right.performed += context => ControlsButtonPress("East");
        left.performed += context => ControlsButtonPress("West");
    }


    // Start is called before the first frame update
    void Start()
    {
        scrollbar.gameObject.SetActive(true);
        scrollbar.value = 1; // At the start, we should see the first element of the list of buttons

        //FOR TESTING PURPOSES
        OnStartControllerExperiment();
        //ShuffleTouchscreenMenuItems();
    }

    public void ShuffleTouchscreenMenuItems()

    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("ControllerMenuSelectableItems");

        int[] indexList = new int[items.Length]; //.OrderBy(a => rnd.Next()).ToList();
        for (int i = 0; i < items.Length; i++) indexList[i] = i;
        indexList = indexList.OrderBy(a => rnd.Next()).ToArray();
        //GameObject[] countOrdered = count.OrderBy(go => go.name).ToArray();

        for (int i = 0; i < items.Length; i++)
        {
            items[i].transform.SetSiblingIndex(indexList[i]);
        }
    }

    public void OnStartControllerExperiment()
    {
        items = GameObject.FindGameObjectsWithTag("ControllerMenuSelectableItems");
        if (items != null) //This may not work i seem to vaguely recall
        {
            Debug.Log(items[currentItemIndex].name);
            items[currentItemIndex].GetComponent<Button>().Select();
            //items[currentItemIndex].SetActive(true);
        }
    }

    private void Click()
    {
        // DEBUG
        //Debug.Log("Select");
        //pseudoConsole.text = currentItemIndex.ToString();
        pseudoConsole.text = "Select";

        //ShuffleTouchscreenMenuItems();
    }

    private void MoveUp()
    {
        if (!(items is null))
        {
            //Deal with the index
            if (currentItemIndex - 1 < 0)
                currentItemIndex = 0;
            else
                currentItemIndex--;
            if (scrollbar != null)
            {
                //Deal with the scrollbar
                if (scrollbar.value >= 1)
                    scrollbar.value = 1;
                else
                    scrollbar.value = scrollbar.value + (1f / (float)items.Length);
            }
        }

        // DEBUG
        //Debug.Log("Up");
        //pseudoConsole.text = currentItemIndex.ToString();
        //pseudoConsole.text = "Up";

        //visual output
        items[currentItemIndex].GetComponent<Button>().Select();
    }

    private void MoveDown()
    {
        if(!(items is null))
        {
            //Deal with the index
            if (currentItemIndex + 1 > items.Length-1)
                currentItemIndex = items.Length - 1;
            else
                currentItemIndex++;

            if(scrollbar != null)
            {
                //Deal with the scrollbar
                if (scrollbar.value <= 0)
                    scrollbar.value = 0;
                else
                {
                    scrollbar.value = scrollbar.value - (1f / (float)items.Length);
                }
            }
        }

        // DEBUG
        /*Debug.Log("Down");
        pseudoConsole.text = currentItemIndex.ToString();*/
        //pseudoConsole.text = "Down";

        //visual output
        items[currentItemIndex].GetComponent<Button>().Select();

    }

    void ControlsButtonPress(string item)
    {
        Debug.Log(item + " Pressed!");
        pseudoConsole.text = item;
        //experimentManager.SelectItem(item);
        //logsManager.LogOnCSV("[REMOTE]", buttonName, "-", true);
    }

}
