using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private bool button1IsPressed = false;

    public void startExperiment()
    {
        Debug.Log("Start");
    }

    public void BlueToothConnect()
    {
        Debug.Log("Connect");
    }

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
    }
}
