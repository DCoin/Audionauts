using UnityEngine;
using System.Collections;

public class ActionOpenMenu : MenuAction {

    public Transform Menu;

    public MenuHandler MenuHandler;

    private bool opening = false;

    public int distance;
    public float speed;

    public override void Execute() {

        opening = true;

        Menu.gameObject.SetActive(true);

        Menu.localPosition = Vector3.forward * distance;

        MenuHandler.NextMenu = Menu;

    }

    void Update() {

        if(!opening)
            return;

        var pos = Menu.localPosition;

        pos.z -= Time.deltaTime * speed;

        if(pos.z <= 0f) {
            pos.z = 0f;

            opening = false;

            

        }

        Menu.localPosition = pos;

    }

}
