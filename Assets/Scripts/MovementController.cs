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

		public bool UseAcceleration = true;
		public float acc = 12;

		public bool UseTarget = false;
		public AnimationCurve ClosenessDeceleration;

		public bool UseFidelity = false;
		public AnimationCurve Fidelity;

		public bool UseAutoAim = false;
		public AnimationCurve ErrorCorrection;

		private Vector2 velocity = Vector2.zero;

		private readonly float ringDistanceRad = Mathf.PI / 4f;
		private readonly float halfRingDistanceRad = Mathf.PI / 8f;

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

		private static Vector2 AngleToVector (float angle) {
			return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
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

			if (UseFidelity) v = v.normalized * Fidelity.Evaluate(v.magnitude);

			if (UseTarget) {
				var target = v.normalized;
				if (UseAutoAim) {
					var rad = Mathf.Atan2(target.y, target.x); // TODO only uses 180 deg?
					var fractionized = rad % ringDistanceRad;
					var ringDistanceOffset = Mathf.Floor(rad / ringDistanceRad);
					float error;
					float dir;
					if (fractionized < halfRingDistanceRad) {
						error = fractionized;
						dir = -1;
					} else {
						error = ringDistanceRad - fractionized;
						dir = 1;
					}
					float percentOff = error / halfRingDistanceRad;
					target = AngleToVector(fractionized + dir * error * ErrorCorrection.Evaluate(percentOff) + ringDistanceRad * ringDistanceOffset);
				}
				target = target * Radius * 1.1f;
				var direction = target - (Vector2)transform.localPosition;
                if(Controller == 0)
				v = direction.normalized * v.magnitude * ClosenessDeceleration.Evaluate(direction.magnitude / 4f);
			}

			if (UseAcceleration) velocity = Vector2.Lerp(velocity, v*Speed, acc * Time.deltaTime);
			else velocity = v * Speed;
            
            transform.Translate(velocity 
                * Time.deltaTime
                , Space.World);

            transform.localPosition = Vector2.ClampMagnitude(transform.localPosition, Radius);

        }
    }
}
