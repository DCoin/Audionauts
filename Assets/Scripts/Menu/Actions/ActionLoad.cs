using UnityEngine;

public class ActionLoad : MenuAction {

    public string Level;

    private bool loading = false;

    public override void Execute() {

        if (loading)
            return;

        loading = true;

        Application.LoadLevelAsync(Level);
        

        //Application.LoadLevel(Level);

    }
}
