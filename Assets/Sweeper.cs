using UnityEngine;
using System.Collections.Generic;
using InControl;

public class Sweeper : MonoBehaviour {

	public List<Note> notes = new List<Note> ();
	public Object BoxPrefab;

	private List<Note> triggers = new List<Note> ();
	private int life = 3;
	private Vector3 initialPosition;

	void Start () {
		initialPosition = transform.position;
		rigidbody.velocity = Vector3.left * -3;
	}

	void FixedUpdate () {
		if (transform.position.x >= 5) ResetPos();
	}

	void Update () {
		CheckControls ();
		triggers.Clear ();
	}

	void OnTriggerStay (Collider col) {
		var note = col.GetComponent<Note> ();
		if (note != null) triggers.Add (note);
	}

	void OnTriggerExit (Collider col) {
		var note = col.GetComponent<Note> ();
		if (note != null) {
			if (!note.WasHit ()) {
				life--;
				note.Missed();
			}
			note.Reset();
		}
	}

	void ResetPos () {
		transform.position = initialPosition;
	}

	void CheckControls () {
		CheckAction (InputControlType.Action1, "Action1", -3);
		CheckAction (InputControlType.Action2, "Action2", -1);
		CheckAction (InputControlType.Action3, "Action3", 1);
		CheckAction (InputControlType.Action4, "Action4", 3);
	}

	void CheckAction (InputControlType action, string tag, float offset)
	{
		var inputDevice = InputManager.ActiveDevice;
		if (inputDevice.GetControl(action).WasPressed) {
			bool hit = false;
			foreach (Note o in triggers) {
				if (o.tag == tag) {
					o.Hit();
					hit = true;
				}
			}
			if (!hit) {
				var o = Instantiate (BoxPrefab, new Vector3(transform.position.x, 2, offset), Quaternion.identity) as GameObject;
				var note = o.GetComponent<Note> ();
				note.tag = tag;
				notes.Add(note);
			}
		}
	}
}
