using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using UnityEngine;
using InControl;

namespace Assets.Scripts
{
    public class MovementController : MonoBehaviour {

        public AxisSource AxisSource;

        public float Radius;

        public float Speed;

        public int Controller;

        private static TwoAxisInputControl GetAxis(InputDevice device, AxisSource source)
        {

            switch (source)
            {
                case AxisSource.StickLeft:
                    return device.LeftStick;

                case AxisSource.StickRight:
                    return device.RightStick;

                case AxisSource.DPad:
                    return device.DPad;

                default:
                    return device.DPad;

            }
        }


        private void Update()
        {

            var device = ControllerManager.Controllers[Controller];
            var axis = GetAxis(device, AxisSource);

            if (ControllerManager.Controllers[1] == InputDevice.Null)
            {
                device = ControllerManager.Controllers[0];

                switch (Controller)
                {
                    case 0:
                        axis = GetAxis(device, AxisSource.StickLeft);
                        break;
                    case 1:
                        axis = GetAxis(device, AxisSource.StickRight);
                        break;
                }
            }

            var flip = ControllerManager.Controllers.GetAxisFlip(Controller);

            var v = axis.Vector;
            v.x *= flip.x;
            v.y *= flip.y;
            
            transform.Translate(v * Speed 
                * Time.deltaTime
                , Space.World);

            transform.localPosition = Vector2.ClampMagnitude(transform.localPosition, Radius);

        }
    }
}
