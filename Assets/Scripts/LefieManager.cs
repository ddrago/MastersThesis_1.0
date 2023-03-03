using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class LefieManager : MonoBehaviour
{
    [Header("Menus")]
    //One side
    public GameObject ExperimentMenu;
    private NetworkManagerHUD networkManagerHUD;

    // Other
    public GameObject MainMenu;
    public GameObject ControllerMenu;
    public GameObject TouchscreenMenu;
    public GameObject VoiceMenu;

    [Header("Spawnpoints")]
    public GameObject leftieExperimentMenuSpawnpoint;
    public GameObject rightyExperimentMenuSpawnpoint;
    public GameObject leftieMainMenuSpawnpoint;
    public GameObject rightyMainMenuSpawnpoint;

    [Header("Spawnpoints")]
    public Toggle toggle;


    // Start is called before the first frame update
    void Start()
    {
        networkManagerHUD = GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD>();
    }

    //Assumes the three condition menus have the same coordinates as the main menu
    public void SwapUIs()
    {

        if (toggle.isOn)  // Participant is left handed
        {
            // One side
            ExperimentMenu.transform.position = leftieExperimentMenuSpawnpoint.transform.position;
            if(networkManagerHUD!=null)
                networkManagerHUD.offsetX = 1460;

            // The other side
            MainMenu.transform.position = leftieMainMenuSpawnpoint.transform.position;
            ControllerMenu.transform.position = leftieMainMenuSpawnpoint.transform.position;
            TouchscreenMenu.transform.position = leftieMainMenuSpawnpoint.transform.position;
            VoiceMenu.transform.position = leftieMainMenuSpawnpoint.transform.position;
        }
        else // Participant is right handed
        {
            // One side
            ExperimentMenu.transform.position = rightyExperimentMenuSpawnpoint.transform.position;
            if (networkManagerHUD != null)
                networkManagerHUD.offsetX = 0;

            // The other side
            MainMenu.transform.position = rightyMainMenuSpawnpoint.transform.position;
            ControllerMenu.transform.position = rightyMainMenuSpawnpoint.transform.position;
            TouchscreenMenu.transform.position = rightyMainMenuSpawnpoint.transform.position;
            VoiceMenu.transform.position = rightyMainMenuSpawnpoint.transform.position;
        }
    }
}
