using UnityEngine;
using Assets.Scripts.Managers;

public class ActionAnimate : MenuAction {

    public AnimationCurve ScaleAnimationIdle;

    public AnimationCurve ScaleAnimation;

    public override void Execute() {

        if(!idle)
            return;

        StartAnimation();
    }

    private bool idle = true;
    private float time = 0f;

    public float Length = 1;


    private void StartAnimation() {

        idle = false;
        time = 0f;

    }

    private Vector3 initialScale;

    void Start() {

        initialScale = transform.localScale;

    }

    void Update() {

        if (idle) {

            var t = MusicManager.Instance.SmoothBeatsPlayed;
            t -= (int) t;

            var v = ScaleAnimationIdle.Evaluate(t);

            transform.localScale = v*initialScale;

        } else {

            time += Time.deltaTime/Length;

            if (time > Length) {
                idle = true;
                Update();
            }

            var value = ScaleAnimation.Evaluate(time);

            transform.localScale = value*initialScale;

        }

    }


}
