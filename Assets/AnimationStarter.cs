using UnityEngine;

[RequireComponent(typeof(Animation))]
public class AnimationStarter : MonoBehaviour {

	public KeyCode starterKey;

	private Animation theAnimation;

	// Use this for initialization
	void Start () {

		theAnimation = this.GetComponent<Animation>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(starterKey)) {
			theAnimation.Play();
		}
	
	}
}
