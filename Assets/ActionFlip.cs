using UnityEngine;
using System.Collections;
using Assets.Scripts.Managers;

public class ActionFlip : MenuAction {

    public AnimationCurve FlipAnimation;

    public int Axis;
    public int Player;

    private bool idle = true;
    private float time = 0f;

    public float Length = 1;

    public override void Execute() {

        if(!idle)
            return;

        ControllerManager.Controllers.DoAxisFlip(Player, Axis);
        
        StartAnimation();
    }

    
    private void StartAnimation() {

        idle = false;
        time = 0f;

    }

    void Update() {

        if (idle)
            return;

        time += Time.deltaTime / Length;

        if(time > Length) {
            idle = true;
            time = Length;
        }

        var value = FlipAnimation.Evaluate(time) * 360f;

        if (Axis == 0) {
            transform.localRotation = Quaternion.Euler(0f, value, 0f);
        } else {
            transform.localRotation = Quaternion.Euler(value, 0f, 0f);
        }

        

    }

}