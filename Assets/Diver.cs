using UnityEngine;
using System.Collections;

public class Diver : MonoBehaviour
{

	public float speed;
	public bool FollowMusic = false;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 pos = transform.position;

		if (!FollowMusic) {
			pos.z += speed * Time.deltaTime;
		} else {
			pos.z = 10 + MusicManager.Instance.BeatsPlayed / 4 * 20;
		}
		
		transform.position = pos;
	}
}
