using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class MirrorUIManager : NetworkBehaviour
{
    [Header("UI Elements")]
    public Text pseudoConsole;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartExperiment()
    {
        CmdDoServerStuff(true);
    }

    [Command(requiresAuthority = false)]
    void CmdDoServerStuff(bool newValue)
    {
        pseudoConsole.text = newValue.ToString(); // do stuff on server
    }
}
