using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Menu.Actions {
    public class ActionLoad : MenuAction {

        private void LoadLevel() {

            Application.LoadLevel(Level);

        }

        public string Level;

        public override void Execute() {

            FadeManager.Instance.EndScene(LoadLevel);

        }

    }
}
