using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    TouchscreenMenu TouchscreenScriptComponent;
    Text PseudoConsole;

    // Start is called before the first frame update
    void Start()
    {
        TouchscreenScriptComponent = GameObject.Find("TouchscreenMenu").GetComponent<TouchscreenMenu>();
        PseudoConsole = GameObject.Find("PseudoConsole").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TouchscreenScriptComponent.getButton1IsPressed())
        {
            print("We pressed a button");
            CmdYoyo();
        }
    }

    [Command(requiresAuthority = false)]
    void CmdYoyo()
    {
        Debug.Log("A client Pressed a button!");
        Debug.Log(PseudoConsole.ToString());
        PseudoConsole.text = "TADA!";
    }
}
