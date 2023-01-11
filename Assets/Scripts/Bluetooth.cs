using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bluetooth : MonoBehaviour
{
    ControllerDevice controller;
    //bool flag1 = false;

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
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                
                for (int i = 0; i < InputSystem.devices.Count; i++)
                {
                    //Debug.Log(InputSystem.devices[i].name);
                    if (InputSystem.devices[i].name == "ControllerDevice1")
                    {
                        controller = (ControllerDevice)InputSystem.devices[i];
                    }
                }
                Debug.Log(controller.enabled);

            }
        }
        else if (this.gameObject.activeSelf && controller != null)
        {
            Debug.Log("Ok it gets here");
            if (controller.midButton.isPressed)
            {
                Debug.Log("We did it bois");
            }
        }
    }
}


