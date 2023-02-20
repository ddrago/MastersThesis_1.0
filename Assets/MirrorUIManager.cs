using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class MirrorUIManager : NetworkBehaviour
{
    [Header("UI Elements")]
    public Text pseudoConsole;
    public Button StartExperimentButton;

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
        CmdPressStartExperimentButton();
    }

    [Command(requiresAuthority = false)]
    void CmdPressStartExperimentButton()
    {
        StartExperimentButton.onClick.Invoke();
        pseudoConsole.text = "Start Experiment Button pressed"; // do stuff on server
    }

    [Command(requiresAuthority = false)]
    void CmdDoServerStuff(bool newValue)
    {
        pseudoConsole.text = newValue.ToString(); // do stuff on server
    }
}
