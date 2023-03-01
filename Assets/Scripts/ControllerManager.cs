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
    string consolePreMessage = "Last input: ";

    public Scrollbar scrollbar;
    public MirrorUIManager mirrorUIManager;
    public ExperimentManager experimentManager;

    //List navigation handling
    private GameObject[] items;
    //private int currentItemIndex = 0;
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
        mid.performed += context => ControlsButtonPress();
        up.performed += context => MoveUp();
        down.performed += context => MoveDown();
        /*mid.performed += context => ControlsButtonPress("maps");
        up.performed += context => ControlsButtonPress("calls");
        down.performed += context => ControlsButtonPress("music");*/
        right.performed += context => ControlsButtonPress();
        left.performed += context => ControlsButtonPress();
    }


    // Start is called before the first frame update
    void Start()
    {
        scrollbar.gameObject.SetActive(true);
        scrollbar.value = 1; // At the start, we should see the first element of the list of buttons
    }

    public void OnStartCondition()
    {
        ShuffleControllerMenuItems();

        items = GameObject.FindGameObjectsWithTag("ControllerMenuSelectableItems")
                    .OrderBy(obj => obj.name, new AlphanumComparatorFast())
                    .ToArray<GameObject>();

        if (items != null) items[mirrorUIManager.GetCurrentControllerItemIndex()].GetComponent<Button>().Select();
    }

    public void ShuffleControllerMenuItems()
    {
        if (experimentManager.studyCurrentlyOngoing)
        {
            if (items == null) return;

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

    public string GetInstructionCorrespondingToIndex(int currentInstructionIndex)
    {
        if (items == null) return "ERROR";

        return items[currentInstructionIndex].GetComponentInChildren<Text>().text;
    }

    public void ControlsButtonPress()
    {
        if (items is null) return;

        mirrorUIManager.RpcControlsButtonPress();

        int i = mirrorUIManager.GetCurrentControllerItemIndex();
        string itemSelected = items[i].GetComponentInChildren<Text>().text;

        pseudoConsole.text = consolePreMessage + itemSelected;

        experimentManager.SelectItem(
            itemSelected,
            items[experimentManager.getCurrentInstruction()].GetComponentInChildren<Text>().text,
            i);

        //ShuffleControllerMenuItems();
        experimentManager.NextInstruction();
    }

    private void MoveUp()
    {
        if (items is null) return;
    
        mirrorUIManager.DecreaseCurrentControllerItemIndex();

        //visual output
        items[mirrorUIManager.GetCurrentControllerItemIndex()].GetComponent<Button>().Select();
        UpdateScrollBar(mirrorUIManager.GetCurrentControllerItemIndex());
    }

    private void MoveDown()
    {
        if (items is null) return;
       
        mirrorUIManager.IncreaseCurrentControllerItemIndex(items.Length);

        //visual output
        items[mirrorUIManager.GetCurrentControllerItemIndex()].GetComponent<Button>().Select();
        UpdateScrollBar(mirrorUIManager.GetCurrentControllerItemIndex());
    }

    public void HighlightSelectedItem(int index)
    {
        items[index].GetComponent<Button>().Select();
    }

    public void UpdateScrollBar(int index)
    {
        if (scrollbar != null) scrollbar.value = 1f - index / 5f;
    }

    public class AlphanumComparatorFast : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            string s1 = x as string;
            if (s1 == null)
            {
                return 0;
            }
            string s2 = y as string;
            if (s2 == null)
            {
                return 0;
            }

            int len1 = s1.Length;
            int len2 = s2.Length;
            int marker1 = 0;
            int marker2 = 0;

            // Walk through two the strings with two markers.
            while (marker1 < len1 && marker2 < len2)
            {
                char ch1 = s1[marker1];
                char ch2 = s2[marker2];

                // Some buffers we can build up characters in for each chunk.
                char[] space1 = new char[len1];
                int loc1 = 0;
                char[] space2 = new char[len2];
                int loc2 = 0;

                // Walk through all following characters that are digits or
                // characters in BOTH strings starting at the appropriate marker.
                // Collect char arrays.
                do
                {
                    space1[loc1++] = ch1;
                    marker1++;

                    if (marker1 < len1)
                    {
                        ch1 = s1[marker1];
                    }
                    else
                    {
                        break;
                    }
                } while (char.IsDigit(ch1) == char.IsDigit(space1[0]));

                do
                {
                    space2[loc2++] = ch2;
                    marker2++;

                    if (marker2 < len2)
                    {
                        ch2 = s2[marker2];
                    }
                    else
                    {
                        break;
                    }
                } while (char.IsDigit(ch2) == char.IsDigit(space2[0]));

                // If we have collected numbers, compare them numerically.
                // Otherwise, if we have strings, compare them alphabetically.
                string str1 = new string(space1);
                string str2 = new string(space2);

                int result;

                if (char.IsDigit(space1[0]) && char.IsDigit(space2[0]))
                {
                    int thisNumericChunk = int.Parse(str1);
                    int thatNumericChunk = int.Parse(str2);
                    result = thisNumericChunk.CompareTo(thatNumericChunk);
                }
                else
                {
                    result = str1.CompareTo(str2);
                }

                if (result != 0)
                {
                    return result;
                }
            }
            return len1 - len2;
        }
    }

}
