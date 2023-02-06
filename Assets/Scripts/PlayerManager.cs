using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    GameObject touchscreenMenu;
    Text PseudoConsole;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("What? Hello!");
        /*
        touchscreenMenu = GameObject.Find("TouchscreenMenu");
        Debug.Log(touchscreenMenu is null);

        PseudoConsole = GameObject.Find("PseudoConsole").GetComponent<Text>();
        Debug.Log(PseudoConsole is null);*/
    }

    // Update is called once per frame
    void Update()
    {
        /*if (touchscreenMenu.GetComponent<TouchscreenMenu>() != null)
        {
            if (touchscreenMenu.GetComponent<TouchscreenMenu>().getButton1IsPressed())
            {
                print("We pressed a button");
                CmdYoyo();
            }
        }*/
    }

    [Command(requiresAuthority = false)]
    public void CmdYoyo()
    {
        Debug.Log("A client Pressed a button!");
        //PseudoConsole.text = "TADA!";
    }
}
