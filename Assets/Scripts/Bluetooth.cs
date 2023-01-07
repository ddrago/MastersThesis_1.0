using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bluetooth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ready to receive input from the bluetooth controller");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            if(Keyboard.current.spaceKey.wasPressedThisFrame){
                Debug.Log(Gamepad.all.Count);
                //Debug.Log("---");
                for (int i = 0; i < InputSystem.devices.Count; i++)
                {
                    Debug.Log(InputSystem.devices[i]);
                }
                Debug.Log("END\n\n");
            }

            if (Mouse.current.backButton.wasPressedThisFrame)
            {
                Debug.Log("Mouse back");
            }

            if (Mouse.current.forwardButton.wasPressedThisFrame)
            {
                Debug.Log("Mouse forward");
            }
        }

    }
}


