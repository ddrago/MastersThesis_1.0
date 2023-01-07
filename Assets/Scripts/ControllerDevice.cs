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

/*
 This struct can be described as such: 
    {
        byte reportId;        // #0
        byte Button1 : 1;     // #1 bit #0
        byte Button2 : 1;     // #1 bit #1
        byte Button3 : 1;     // #1 bit #2
        byte Button4 : 1;     // #1 bit #3
        byte Button5 : 1;     // #1 bit #4
    }
 */
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
    [InputControl(name = "WestButton", layout = "Button", bit = 3)]
    [InputControl(name = "EastButton", layout = "Button", bit = 4)]
    [InputControl(name = "button6", layout = "Button", bit = 5)]
    [InputControl(name = "MidButton", layout = "Button", bit = 6)]
    [InputControl(name = "NorthButton", layout = "Button", bit = 7)]
    [InputControl(name = "SouthButton", layout = "Button", bit = 8)]
    [FieldOffset(2)] public int buttons;
}

[InputControlLayout(stateType = typeof(ControllerDeviceState))]
[InitializeOnLoad]
public class ControllerDevice : InputDevice
{
    public ButtonControl button1 { get; private set; }
    public ButtonControl button2 { get; private set; }
    public ButtonControl button3 { get; private set; }
    public ButtonControl WestButton { get; private set; }  //PREVIOUS TRACK
    public ButtonControl EastButton { get; private set; }  //NEXT TRACK
    public ButtonControl button6 { get; private set; }  
    public ButtonControl MidButton { get; private set; }  //PLAY/PAUSE
    public ButtonControl NorthButton { get; private set; }  //VOLUME UP
    public ButtonControl SouthButton9 { get; private set; }  //VOLUME DOWN

    protected override void FinishSetup()
    {
        base.FinishSetup();

        button1 = GetChildControl<ButtonControl>("button1");
        button2 = GetChildControl<ButtonControl>("button2");
        button3 = GetChildControl<ButtonControl>("button3");
        button4 = GetChildControl<ButtonControl>("button4");
    }

    static ControllerDevice()
    {
        InputSystem.RegisterLayout<ControllerDevice>(
                matches: new InputDeviceMatcher()
                .WithInterface("HID")
                .WithCapability("vendorId", 0x2652) // Iton Corp.
                .WithCapability("productId", 0x17666) // Broadcom Bluetooth Wireless Keyboard.
        );

        if (!InputSystem.devices.Any(x => x is ControllerDevice))
            InputSystem.AddDevice<ControllerDevice>();
    }

    [RuntimeInitializeOnLoadMethod]
    public static void Initialize()
    {


    }
}
