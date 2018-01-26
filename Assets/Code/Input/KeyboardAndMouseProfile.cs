using System;
using System.Collections;
using UnityEngine;
using InControl;


// This custom profile is enabled by adding it to the Custom Profiles list
// on the InControlManager component, or you can attach it yourself like so:
// InputManager.AttachDevice( new UnityInputDevice( "KeyboardAndMouseProfile" ) );
// 
public class KeyboardAndMouseProfile : UnityInputDeviceProfile {
    public KeyboardAndMouseProfile() {
        Name = "Keyboard/Mouse";
        Meta = "A keyboard and mouse combination profile appropriate for FPS.";

        // This profile only works on desktops.
        SupportedPlatforms = new[]
        {
                "Windows",
                "Mac",
                "Linux"
            };

        Sensitivity = 1.0f;
        LowerDeadZone = 0.0f;
        UpperDeadZone = 1.0f;

        ButtonMappings = new[]
        {
                new InputControlMapping
                {
                    Handle = "Action1",
                    Target = InputControlType.Action1,
                    Source = KeyCodeButton(KeyCode.Alpha1)
                },

                new InputControlMapping
                {
                    Handle = "Action2",
                    Target = InputControlType.Action2,
                Source = KeyCodeButton(KeyCode.Alpha2)
                },

                new InputControlMapping
                {
                    Handle = "Action3",
                    Target = InputControlType.Action3,
                    Source = KeyCodeButton(KeyCode.Alpha3)
                },

                new InputControlMapping
                {
                    Handle = "Action4",
                    Target = InputControlType.Action4,
                    Source = KeyCodeButton(KeyCode.Alpha4)
                }
            };

        AnalogMappings = new[]
        {
                new InputControlMapping
                {
                    Handle = "Steer",
                    Target = InputControlType.LeftStickX,
					// KeyCodeAxis splits the two KeyCodes over an axis. The first is negative, the second positive.
					Source = KeyCodeAxis( KeyCode.A, KeyCode.D )
                },

                new InputControlMapping
                {
                    Handle = "Throttle/Brake",
                    Target = InputControlType.RightStickY,
                    Source = KeyCodeAxis(KeyCode.S, KeyCode.W )
                }
            };
    }
}

