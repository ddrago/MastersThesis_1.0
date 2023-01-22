using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Scripting;

[Preserve]
[StructLayout(LayoutKind.Explicit)]
public struct ControllerDeviceState : IInputStateTypeInfo
{
    public FourCC format => new FourCC('H', 'I', 'D');

    // HID input reports can start with an 8-bit report ID. It depends on the device
    // whether this is present or not. We don't really need to add the field, but let's do so for the sake of
    // completeness. This can also help with debugging.
    [FieldOffset(0)] public byte reportId;
    [FieldOffset(1)] public byte something;

    [InputControl(name = "dpad", format = "BIT", layout = "Dpad", sizeInBits = 5, defaultState = 8)]
    [InputControl(name = "button1", layout = "Button", bit = 0)]
    [InputControl(name = "button2", layout = "Button", bit = 1)]
    [InputControl(name = "button3", layout = "Button", bit = 2)]
    [InputControl(name = "westButton", displayName = "prevTrack", layout = "Button", bit = 3)]
    [InputControl(name = "eastButton", displayName = "nextTrack", layout = "Button", bit = 4)]
    [InputControl(name = "button6", layout = "Button", bit = 5)]
    [InputControl(name = "midButton", displayName = "playPause", layout = "Button", bit = 6)]
    [InputControl(name = "northButton", displayName = "volumeUp", layout = "Button", bit = 7)]
    [InputControl(name = "southButton", displayName = "volumeDown", layout = "Button", bit = 8)]
    [FieldOffset(2)] public int buttons;
}

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
[Preserve]
[InputControlLayout(stateType = typeof(ControllerDeviceState))]
public class ControllerDevice : Gamepad
{
#if UNITY_EDITOR
    static ControllerDevice()
    {
        Initialize();
    }
#endif


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        InputSystem.RegisterLayout<ControllerDevice>
        (
            matches: new InputDeviceMatcher()
            .WithInterface("HID")
            .WithManufacturer("Iton Corp.      ")
        //.WithCapability("inputReportSize", 4)
        );
    }

    public ButtonControl button1 { get; private set; }
    public ButtonControl button2 { get; private set; }
    public ButtonControl button3 { get; private set; }
    public ButtonControl westButton { get; private set; }  //PREVIOUS TRACK
    public ButtonControl eastButton { get; private set; }  //NEXT TRACK
    public ButtonControl button6 { get; private set; }
    public ButtonControl midButton { get; private set; }  //PLAY/PAUSE
    public ButtonControl northButton { get; private set; }  //VOLUME UP
    public ButtonControl southButton { get; private set; }  //VOLUME DOWN
    public bool SelectButtonDown { get; set; }
    public bool RewindButtonDown { get; set; }
    public bool PlayPauseButtonDown { get; set; }
    public bool FastForwardButtonDown { get; set; }

    protected override void FinishSetup()
    {
        base.FinishSetup();

        westButton = GetChildControl<ButtonControl>("westButton");
        eastButton = GetChildControl<ButtonControl>("eastButton");
        midButton = GetChildControl<ButtonControl>("midButton");
        northButton = GetChildControl<ButtonControl>("northButton");
        southButton = GetChildControl<ButtonControl>("southButton");
    }

    public static ControllerDevice current { get; private set; }

    public override void MakeCurrent()
    {
        base.MakeCurrent();
        current = this;
    }

    protected override void OnRemoved()
    {
        base.OnRemoved();
        if (current == this)
            current = null;
    }

    #region Editor
#if UNITY_EDITOR
    [MenuItem("Tools/AftvRemote/Create Device")]
    private static void CreateDevice()
    {
        InputSystem.AddDevice<ControllerDevice>();
    }

    [MenuItem("Tools/AftvRemote/Remove Device")]
    private static void RemoveDevice()
    {
        var customDevice = InputSystem.devices.FirstOrDefault(x => x is ControllerDevice);
        if (customDevice != null)
            InputSystem.RemoveDevice(customDevice);
    }
#endif
    #endregion

    // This does not work somehow. 
    public void OnUpdate()
    {
        var state = new ControllerDeviceState();
        InputSystem.QueueStateEvent(this, state);
    }
}
