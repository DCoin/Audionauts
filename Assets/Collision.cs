using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Collision : MonoBehaviour
{
	public float power;
	private Radius[] players;

	void Start ()
	{
		players = GetComponentsInChildren<Radius>();
	}

	void Update ()
	{
		// For some reason i decided to allow any number of players
		for (int i = 0; i < players.Length; i++) {
			var curr = players[i];
			for (int j = i+1; j < players.Length; j++) {
				checkCollision(curr, players[j]);
			}
		}
	}

	void checkCollision (Radius r1, Radius r2)
	{
		var dist = (r1.transform.position - r2.transform.position).magnitude;
		//Debug.Log ("r1: " + r1 + " r2: " + r2 + " dist: " + dist);
		if (dist < r1.value + r2.value) {
			resolveCollision(r1, r2);
		}
	}

	void resolveCollision (Radius r1, Radius r2)
	{
//		Debug.Log("Collision");
		var r1Normal = (r2.transform.position - r1.transform.position).normalized;
		var r2Normal = (r1.transform.position - r2.transform.position).normalized;
		
		r1.GetComponent<ControllerMoverB>().disturbance = r1Normal*power * -1;
		r2.GetComponent<ControllerMoverB>().disturbance = r2Normal*power * -1;
	}
}
