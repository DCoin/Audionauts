using UnityEngine;

[RequireComponent(typeof(Animation))]
public class AnimationController : MonoBehaviour {

	private Animation anim;

	public float animationLength = 1f;

	void Start() {

		anim = GetComponent<Animation>();

	}

	void Update() {

		foreach (AnimationState state in anim) {

			state.speed = state.length / animationLength;

		}

	}
}
