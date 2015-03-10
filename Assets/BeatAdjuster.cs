using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AnimationController))]
public class BeatAdjuster : MonoBehaviour {

	private MusicManager mm;
	private AnimationController ac;

	// Use this for initialization
	void Start () {

		mm = MusicManager.Instance;
		ac = GetComponent<AnimationController> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		ac.animationLength = 1f / mm.BPS;
	
	}
}
