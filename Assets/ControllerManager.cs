using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class ControllerManager : MonoBehaviour
{
	public static ControllerManager Controllers { get; private set; }

	private InputDevice[] _controllers = new InputDevice[2];

	public InputDevice this[int i] {
		get {
			if (_controllers[i] == null) return InputDevice.Null;
			return _controllers[i];
		}
	}

	private void Start ()
	{
		if (Controllers == null) {
			Controllers = this;
		} else {
			Debug.LogError ("A scene should only have one ControllerManager");
		}
	}

	private void Update ()
	{
		// Watch out for break and goto abuse...
		for (int i = 0; i < InputManager.Devices.Count; i++) {
			if (InputManager.Devices[i].AnyButton.WasPressed) {
				int oldest = 0;
				for (int j = 0; j < _controllers.Length; j++) {
					if (_controllers[j] == null) {
						oldest = j;
						break;
					}
					if (_controllers[j] == InputManager.Devices[i]) goto End; // C# WHY U NO LABELED CONTINUE???++?+??
					if (_controllers[oldest].LastChangedAfter(_controllers[j])) oldest = j;
				}
				_controllers[oldest] = InputManager.Devices[i];
			}
		End:;
		}
	}
}
