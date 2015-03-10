using UnityEngine;
using System.Collections;

public class Diver : MonoBehaviour
{

	public float speed;
	public bool FollowMusic = false;
	private float BeatsSeperation; // The distance between beats

	public Transform stage;

	// Use this for initialization
	void Start ()
	{

		BeatsSeperation = stage.localScale.z;
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 pos = transform.position;

		if (!FollowMusic) {
			pos.z += speed * Time.deltaTime;
		} else {
			pos.z = MusicManager.Instance.BeatsPlayed * BeatsSeperation;
		}
		
		transform.position = pos;
	}
}
