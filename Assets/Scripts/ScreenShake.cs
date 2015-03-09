using UnityEngine;

public class ScreenShake : MonoBehaviour
{

	public float shakeAmount 	= 0.2f;
	public float shakeRate 		= 0.01f;
	public float shakeDuration 	= 0.3f;

	public InitialPosition target;

	private Vector3 initialPosition;

	public void Begin() {

		this.initialPosition = target.GetComponent<InitialPosition>().Position;

		InvokeRepeating("CameraShake", 0, shakeRate);
		Invoke("StopShaking", shakeDuration);

	}

	public void CameraShake() {

		if (shakeAmount > 0)
			target.UpdateCurrentPosition (initialPosition + (Vector3)Random.insideUnitCircle * shakeAmount);

	}
	
	void StopShaking() {

		CancelInvoke("CameraShake");
		target.UpdateCurrentPosition (initialPosition);
		Destroy(this.gameObject);
	}
	
}