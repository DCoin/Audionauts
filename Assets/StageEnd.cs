using Assets.Scripts.Managers;
using UnityEngine;

    public class StageEnd : MonoBehaviour {

        private AsyncOperation aop;

        private void LateUpdate() {
            var traveller = StageManager.Instance.Traveller;

            var za = traveller.LastPosition.z;

            var z = transform.position.z;

            var zb = traveller.CurrentPosition.z;

            if ((!(za < z)) || (!(z < zb))) return;

            aop = Application.LoadLevelAsync("Menu");

            aop.allowSceneActivation = false;

            FadeManager.Instance.EndScene(LoadMenu);
        }


        private void LoadMenu() {

            aop.allowSceneActivation = true;

        }

    }
