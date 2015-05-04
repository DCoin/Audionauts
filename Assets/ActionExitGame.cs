using Assets.Scripts.Managers;
using UnityEngine;

public class ActionExitGame : MenuAction {

    private void ExitGame() {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif

        
    }

    public override void Execute() {

        FadeManager.Instance.EndScene(ExitGame);

    }

}
