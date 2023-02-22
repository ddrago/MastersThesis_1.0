using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Android;

public class CyclingPerformanceLogsManager : MonoBehaviour
{
    [Header("TEMPORARY: Console Manager")]
    public Text PseudoConsole;

    public LogsManager logsManager;
    public MirrorUIManager mirrorUIManager;

    void OnEnable()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.ACTIVITY_RECOGNITION"))
        {
            Permission.RequestUserPermission("android.permission.ACTIVITY_RECOGNITION");
        }

        if (Accelerometer.current != null)
            InputSystem.EnableDevice(Accelerometer.current);
    }

    protected void OnDisable()
    {
        if (Accelerometer.current != null)
            InputSystem.DisableDevice(Accelerometer.current);
    }

    void Start()
    {

    }

    void Update()
    {
        mirrorUIManager.logTilt(GetAccelerations()); 
    }

    // we assume that device is held parallel to the ground
    // and Home button is in the right hand.
    // a_z should be upward acceletarion (aka at most times just gravity)
    public Vector3 GetAccelerations()
    {

        #if UNITY_EDITOR
            Vector3 dir = Input.acceleration;

            /* // clamp acceleration vector to unit sphere
            if (dir.sqrMagnitude > 1)
                dir.Normalize();*/

            return dir;
        #elif UNITY_ANDROID
            if (!Accelerometer.current.enabled)
            {
                PseudoConsole.text = "The accelerometer is not enabled";
                InputSystem.EnableDevice(Accelerometer.current);
            }
            else
            {
                return Accelerometer.current.acceleration.ReadValue();
            }

            return Vector3.zero;
        #endif
        //It's neither unity editor nor android
        return Vector3.zero;

    }
}
