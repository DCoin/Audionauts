using UnityEngine;
using System.Collections;

public class OffBeater : MonoBehaviour {


	public int A;
	public int B;

	void OnValidate() {

		Vector3 pos = transform.localPosition;

		float a = A;
		float b = B;

		pos.z = 4.0f * a / b;

		transform.localPosition = pos;

	}


}
