using System;
using System.Collections;
using UnityEngine;
using InControl;

public class TestProfile : UnityInputDeviceProfile
{
	public TestProfile ()
	{
		Name = "Keyboard/Mouse";
		Meta = "A keyboard and mouse combination profile appropriate testing.";

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
					Handle = "A",
					Target = InputControlType.Action1,
					Source = KeyCodeButton( KeyCode.Q )
				},
				new InputControlMapping
				{
					Handle = "B",
					Target = InputControlType.Action2,
					Source = KeyCodeButton( KeyCode.E )
				},
				new InputControlMapping
				{
					Handle = "X",
					Target = InputControlType.Action3,
					Source = KeyCodeButton( KeyCode.U )
				},
				new InputControlMapping
				{
					Handle = "Y",
					Target = InputControlType.Action4,
					Source = KeyCodeButton( KeyCode.O )
				},
				new InputControlMapping
				{
					Handle = "DPad up",
					Target = InputControlType.DPadUp,
					Source = KeyCodeButton( KeyCode.UpArrow )
				},
				new InputControlMapping
				{
					Handle = "DPad down",
					Target = InputControlType.DPadDown,
					Source = KeyCodeButton( KeyCode.DownArrow )
				},
				new InputControlMapping
				{
					Handle = "DPad left",
					Target = InputControlType.DPadLeft,
					Source = KeyCodeButton( KeyCode.LeftArrow )
				},
				new InputControlMapping
				{
					Handle = "DPad right",
					Target = InputControlType.DPadRight,
					Source = KeyCodeButton( KeyCode.RightArrow )
				}
			};

		AnalogMappings = new[]
			{
				new InputControlMapping
				{
					Handle = "AD-X",
					Target = InputControlType.LeftStickX,
					// KeyCodeAxis splits the two KeyCodes over an axis. The first is negative, the second positive.
					Source = KeyCodeAxis( KeyCode.A, KeyCode.D )
				},
				new InputControlMapping
				{
					Handle = "WS-Y",
					Target = InputControlType.LeftStickY,
					// Notes that up is positive in Unity, therefore the order of KeyCodes is down, up.
					Source = KeyCodeAxis( KeyCode.S, KeyCode.W )
				},
				new InputControlMapping
				{
					Handle = "JL-X",
					Target = InputControlType.RightStickX,
					// KeyCodeAxis splits the two KeyCodes over an axis. The first is negative, the second positive.
					Source = KeyCodeAxis( KeyCode.J, KeyCode.L )
				},
				new InputControlMapping
				{
					Handle = "IK-Y",
					Target = InputControlType.RightStickY,
					// Notes that up is positive in Unity, therefore the order of KeyCodes is down, up.
					Source = KeyCodeAxis( KeyCode.K, KeyCode.I )
				}
			};
	}
}

