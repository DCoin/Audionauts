﻿using InControl;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ControllerManager : MonoBehaviour
    {
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

        private void Update() {

            foreach(var device in InputManager.Devices) 
                if (device.AnyButton.WasPressed) 
                    RegisterDevice(device);
        
        }
    }
}
