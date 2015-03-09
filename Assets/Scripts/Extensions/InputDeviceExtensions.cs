using InControl;

namespace Audionauts.Extensions {

	public static class InputDeviceExtensions {

		public static TwoAxisInputControl GetAxis(this InputDevice device, AxisSource source) {

			switch (source) {
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
	}

}
