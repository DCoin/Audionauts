using Assets.Scripts.Managers;

public class ActionResetFlip : MenuAction {

    public override void Execute() {

        ControllerManager.Controllers.ResetAxisFlips();
    }
}