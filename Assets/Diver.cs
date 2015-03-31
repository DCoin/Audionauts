using UnityEngine;
using System.Collections;

public class Diver : MonoBehaviour
{

	public float speed;
	public bool FollowMusic = false;
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 pos = transform.position;

		if (!FollowMusic) {
			pos.z += speed * Time.deltaTime;
		} else {
			pos.z = MusicManager.Instance.BeatsPlayed;
		}
		
		transform.localPosition = pos;
	}
}
