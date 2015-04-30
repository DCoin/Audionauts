using InControl;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ControllerManager : MonoBehaviour
    {
        public InputControlType Modifier;
        public InputControlType InputControlType;

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

                axisFlips = new int[2, 2];

                for (int i = 0; i < 2; i++) {
                    for (int j = 0; j < 2; j++) {
                        var key = PrefKey(i, j);
                        var val = PlayerPrefs.GetInt(key);
                        axisFlips[i, j] = val == 0 ? 1 : val;
                    }
                }

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

        private string PrefKey(int player, int axis) {

            return "Player" + player + "Axis" + axis;

        }

        private int[,] axisFlips;

        public void DoAxisFlip(int player, int axis) {
    
            var val = PlayerPrefs.GetInt(PrefKey(player,axis));

            if(val == 1) {
                val = -1;
            } else {
                val = 1;
            }

            axisFlips[player, axis] = val;

            PlayerPrefs.SetInt(PrefKey(player,axis), val);
            
        }

        public Vector2 GetAxisFlip(int player) {

            var flipX = axisFlips[player, 0];
            var flipY = axisFlips[player, 1];

            return new Vector2(flipX,flipY);

        }

        
    }
}
