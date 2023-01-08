using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

[StructLayout(LayoutKind.Explicit)]
public struct ControllerDeviceState : IInputStateTypeInfo
{
    public FourCC format => new FourCC('H', 'I', 'D');

    // HID input reports can start with an 8-bit report ID. It depends on the device
    // whether this is present or not. We don't really need to add the field, but let's do so for the sake of
    // completeness. This can also help with debugging.
    [FieldOffset(0)] public byte reportId;
    [FieldOffset(1)] public byte something;

    /*[InputControl(name = "dpad", format = "BIT", layout = "Dpad", sizeInBits = 5, defaultState = 8)]*/
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

[InputControlLayout(stateType = typeof(ControllerDeviceState))]
[InitializeOnLoad]
public class ControllerDevice : InputDevice
{
    public ButtonControl button1 { get; private set; }
    public ButtonControl button2 { get; private set; }
    public ButtonControl button3 { get; private set; }
    public ButtonControl westButton { get; private set; }  //PREVIOUS TRACK
    public ButtonControl eastButton { get; private set; }  //NEXT TRACK
    public ButtonControl button6 { get; private set; }  
    public ButtonControl midButton { get; private set; }  //PLAY/PAUSE
    public ButtonControl northButton { get; private set; }  //VOLUME UP
    public ButtonControl southButton { get; private set; }  //VOLUME DOWN

    protected override void FinishSetup()
    {
        base.FinishSetup();

        westButton = GetChildControl<ButtonControl>("westButton");
        eastButton = GetChildControl<ButtonControl>("eastButton");
        midButton = GetChildControl<ButtonControl>("midButton");
        northButton = GetChildControl<ButtonControl>("northButton");
        southButton = GetChildControl<ButtonControl>("southButton");
    }

    static ControllerDevice()
    {
        InputSystem.RegisterLayout<ControllerDevice>(
                matches: new InputDeviceMatcher()
                .WithInterface("HID")
                .WithManufacturer("Iton Corp.      ")
                .WithCapability("inputReportSize", 4)
                //.WithCapability("vendorId", 0x2652) // Iton Corp.
                //.WithCapability("productId", 0x17666) // Broadcom Bluetooth Wireless Keyboard.
        );

        if (!InputSystem.devices.Any(x => x is ControllerDevice))
            InputSystem.AddDevice<ControllerDevice>();
    }

    [RuntimeInitializeOnLoadMethod]
    public static void Initialize()
    {


    }
}
