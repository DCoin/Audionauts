using UnityEngine;
using System.Collections;

public class InitialPosition : MonoBehaviour {

	private Vector3 _initialPosition;

	public bool local;

	public Vector3 Position {
		get {
			return _initialPosition;
		}
	}

	void Awake () {

		if (local) {
			_initialPosition = this.transform.localPosition;
		} else {
			_initialPosition = this.transform.position;
		}
	}

	public void UpdateCurrentPosition(Vector3 position) {

		if (local) {
			this.transform.localPosition = position;
		} else {
			this.transform.position = position;
		}

	}

}
