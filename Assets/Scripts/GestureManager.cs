using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PrintActivatedText(string text)
    {
        Debug.Log("Activated: " + text);
    }

    public void PrintDeactivatedText(string text)
    {
        Debug.Log("Deactivated: " + text);
    }
}
