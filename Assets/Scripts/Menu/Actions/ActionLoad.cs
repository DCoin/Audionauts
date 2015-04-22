using Assets.Scripts.Managers;

namespace Assets.Scripts.Menu.Actions {
    public class ActionLoad : MenuAction {

        public string Level;

        public override void Execute() {


            FadeManager.Instance.EndScene(Level);

        }

    }
}
