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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Command(requiresAuthority = false)]
    public void CmdYoyo()
    {
        Debug.Log("A client Pressed a button!");
        //PseudoConsole.text = "TADA!";
    }
}
