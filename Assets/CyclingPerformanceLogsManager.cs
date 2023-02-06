using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CyclingPerformanceLogsManager : MonoBehaviour
{
    [Header("TEMPORARY: Console Manager")]
    public Text PseudoConsole;

    public LogsManager logsManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //OPTION 1
        /*        if (!isUpdating)
                {
                    StartCoroutine(GetLocation());
                    isUpdating = !isUpdating;
                }*/

        //OPTION 2
        //GetUserLocation();

        //OPTION 3
        //StartCoroutine(LocationCoroutine());

        //OPTION 4
        logsManager.LogTilt(GetAccelerations());

        //Vector3 dir = GetAccelerations();
        //PseudoConsole.text = String.Format("x: {0},\n y: {1},\n z: {2}", dir.x.ToString("F2"), dir.y.ToString("F2"), dir.z.ToString("F2"));
    }

    // we assume that device is held parallel to the ground
    // and Home button is in the right hand.
    // a_z should be upward acceletarion (aka at most times just gravity)
    public Vector3 GetAccelerations()
    {
        Vector3 dir = Input.acceleration;

        // remap device acceleration axis to game coordinates:
        //  1) XY plane of the device is mapped onto XZ plane
        //  2) rotated 90 degrees around Y axis
        dir.x = Input.acceleration.y;
        dir.y = Input.acceleration.z;
        dir.z = Input.acceleration.x;

        /*// clamp acceleration vector to unit sphere
        if (dir.sqrMagnitude > 1)
            dir.Normalize();*/

        return dir;
    }

    // OPTION 1
    /*IEnumerator GetLocation()
    {
        Debug.Log("Hello");

        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            PseudoConsole.text = "Location not enabled";
            yield return new WaitForSeconds(10);
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1 )
        {
            PseudoConsole.text = "Timed out";
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            PseudoConsole.text = "Unable to determine device location";
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            PseudoConsole.text = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + 100f + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stop service if there is no need to query location updates continuously
        *//*        isUpdating = !isUpdating;
                Input.location.Stop();*//*

        //Stop retrieving location
        Input.location.Stop();
        StopCoroutine("Start");
    }*/

    // OPTION 2
    /*public void GetUserLocation()
    {
        if (!Input.location.isEnabledByUser) //FIRST IM CHACKING FOR PERMISSION IF "true" IT MEANS USER GAVED PERMISSION FOR USING LOCATION INFORMATION
        {
            PseudoConsole.text = "No Permission";
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        else
        {
            PseudoConsole.text = "Permission Granted";
            StartCoroutine(GetLatLonUsingGPS());
        }
    }

    IEnumerator GetLatLonUsingGPS()
    {
        Input.location.Start();
        int maxWait = 5;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            PseudoConsole.text = Input.location.status.ToString();
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        PseudoConsole.text = "waiting before getting lat and lon";

        // Access granted and location value could be retrieve
        double longitude = Input.location.lastData.longitude;
        double latitude = Input.location.lastData.latitude;

        //AddLocation(latitude, longitude);
        PseudoConsole.text = "" + Input.location.status + "  lat:" + latitude + "  long:" + longitude;

        //Stop retrieving location
        Input.location.Stop();
        StopCoroutine("Start");
    }*/

    // OPTION 3
    IEnumerator LocationCoroutine()
    {
        // Uncomment if you want to test with Unity Remote
#if UNITY_EDITOR
        yield return new WaitWhile(() => !UnityEditor.EditorApplication.isRemoteConnected);
        yield return new WaitForSecondsRealtime(5f);
#endif
#if UNITY_EDITOR
        // No permission handling needed in Editor
#elif UNITY_ANDROID
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CoarseLocation)) {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CoarseLocation);
        }

        // First, check if user has location service enabled
        if (!UnityEngine.Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("Android and Location not enabled");
            yield break;
        }
#endif
        // Start service before querying location
        UnityEngine.Input.location.Start(500f, 500f);

        // Wait until service initializes
        int maxWait = 15;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            maxWait--;
        }

        // Editor has a bug which doesn't set the service status to Initializing. So extra wait in Editor.
#if UNITY_EDITOR
        int editorMaxWait = 15;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Stopped && editorMaxWait > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            editorMaxWait--;
        }
#endif

        // Service didn't initialize in 15 seconds
        if (maxWait < 1)
        {
            // TODO Failure
            PseudoConsole.text = "Timed out";
            yield break;
        }

        // Connection has failed
        if (UnityEngine.Input.location.status != LocationServiceStatus.Running)
        {
            // TODO Failure
            Debug.Log(String.Format("Unable to determine device location. Failed with status {0}", UnityEngine.Input.location.status));
            yield break;
        }
        else
        {
            PseudoConsole.text = String.Format("Location service live. status {0}", UnityEngine.Input.location.status);
            // Access granted and location value could be retrieved
            PseudoConsole.text = "Latitude: "
                + UnityEngine.Input.location.lastData.latitude + "\nLongitude: "
                + UnityEngine.Input.location.lastData.longitude + "\nAltitude:  "
                + UnityEngine.Input.location.lastData.altitude + "\nAcuuracy:  "
                + UnityEngine.Input.location.lastData.horizontalAccuracy + "\nTime: "
                + UnityEngine.Input.location.lastData.timestamp;

            var _latitude = UnityEngine.Input.location.lastData.latitude;
            var _longitude = UnityEngine.Input.location.lastData.longitude;
            // TODO success do something with location
        }

        // Stop service if there is no need to query location updates continuously
        UnityEngine.Input.location.Stop();
    }
}
