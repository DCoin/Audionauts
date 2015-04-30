using Assets.Scripts.Managers;
using UnityEngine;

    public class StageEnd : MonoBehaviour {

        private void LateUpdate() {
            var traveller = StageManager.Instance.Traveller;

            var za = traveller.LastPosition.z;

            var z = transform.position.z;

            var zb = traveller.CurrentPosition.z;

            var diff = zb - za;

            if((za < z) && (z < zb)) {

                FadeManager.Instance.EndScene("Menu");

            }
        }

    }
