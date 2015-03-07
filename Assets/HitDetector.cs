using UnityEngine;
using System.Collections.Generic;

public class HitDetector : MonoBehaviour {

	public Transform players;

	public Transform player1;
	public Transform player2;

	private float distance;
	
	// Use this for initialization
	void Start () {
	
		distance = GetDistance();

	}

	private float GetDistance() {

		Vector3 a = this.transform.position;
		Vector3 b = players.transform.position;
		Vector3 d = a - b;

		return d.z;
	}
	
	// Update is called once per frame
	void Update () {
	
		float newDistance = GetDistance ();

		if (distance > 0f && newDistance < 0f) {
			RegisterHits();

		}

		distance = newDistance;

	}

	private IEnumerable<Transform> GetImmediateChildren() {

		foreach (Transform child in GetComponentInChildren<Transform>()) {

			if(child.parent == this.transform)
				yield return child;

		}

		yield break;

	}

	private Transform GetHit(Transform player) {

		Vector2 p = player.transform.position;
		
		Transform hit = null;
		
		foreach(Radius r in GetComponentsInChildren<Radius>()) {
			
			Vector2 c = r.transform.position;
			
			float dsqr = (p-c).sqrMagnitude;
			
			if(dsqr < r.value * r.value) {
				hit = r.transform;
				break;
			}
			
		}

		return hit;
	}

	private void RegisterHits() {

		Transform hit1 = GetHit (player1);
		Transform hit2 = GetHit (player2);

		if (hit1 == hit2) {

			RegisterHit (hit1, Slice.HitType.Both);

		} else {
			RegisterHit(hit1, Slice.HitType.First);
			RegisterHit(hit2, Slice.HitType.Second);
		}

	}

	private void RegisterHit(Transform t, Slice.HitType hit) {

		if (t == null) 
			return;


		Slice s = t.GetComponent<Slice> ();

		if (s != null)
			s.OnHit (hit);

	}
}
