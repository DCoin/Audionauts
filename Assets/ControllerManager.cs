using InControl;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public InputControlType Modifier = InputControlType.Start;
    public InputControlType InputControlType = InputControlType.Action4;

    public float DeadZone;

    public static ControllerManager Controllers { get; private set; }

    private readonly InputDevice[] _controllers = new InputDevice[2];

    public InputDevice this[int i]
    {
        get
        {
            return _controllers[i] ?? InputDevice.Null;
        }
    }

    private void Start()
    {
        if (Controllers == null)
        {
            Controllers = this;
        }
        else
        {
            Debug.LogError("A scene should only have one ControllerManager");
        }
    }

    private void RegisterDevice(InputDevice device) {

        var oldest = 0;

        for (var i = 0; i < _controllers.Length; i++) {

            if (_controllers[i] == null) {
                oldest = i;
                break;
            }

            if (_controllers[i] == device) {
                return;
            }

            if (_controllers[oldest].LastChangedAfter(_controllers[i])) { 
                oldest = i; 
            }

        }

        _controllers[oldest] = device;
    }

    

    private bool stickIsLive(InputDevice device)
    {
        var foo = new TwoAxisInputControl[] {device.LeftStick, device.RightStick};

        foreach (var baz in foo)
        {
            var v = baz.Vector;

            if (v.sqrMagnitude > DeadZone * DeadZone)
                return true;



        }

        return false;

    }

    private void Update() {

        foreach (var device in InputManager.Devices)
        {

            if (device.AnyButton.WasPressed || stickIsLive(device))
                RegisterDevice(device);

        }

        foreach (var device in _controllers)
        {
            if(device == null)
                break;

            if (!device.GetControl(Modifier).IsPressed) 
                continue;

            if (device.GetControl(InputControlType).WasPressed)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }

    }
}
