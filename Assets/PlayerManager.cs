using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    MainMenu ExperimentMenuScriptComponent;

    // Start is called before the first frame update
    void Start()
    {
        ExperimentMenuScriptComponent = GameObject.Find("ExperimentMenu").GetComponent<MainMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ExperimentMenuScriptComponent.getButton1IsPressed())
        {
            print("We pressed a button");
            CmdYoyo();
        }
    }

    [Command(requiresAuthority = false)]
    void CmdYoyo()
    {
        Debug.Log("A client Pressed a button!");
    }
}
