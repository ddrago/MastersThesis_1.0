using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HandGesturePrint : MonoBehaviour
{
    Text PseudoConsole;


    // Start is called before the first frame update
    void Start()
    {
        PseudoConsole = GameObject.Find("PseudoConsole").GetComponent<Text>();
    }

    public void PrintActivateMessage(string text)
    {
        PseudoConsole.text = text;
    }
}
