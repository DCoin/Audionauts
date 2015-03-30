using UnityEngine;

public class GrooveManager : MonoBehaviour {

	public float maxGroove;

	public float depletionRate;

	private float currentGroove;

	public float GroovePCT {
		get {
			return currentGroove / maxGroove;
		}
	}
	
	public static GrooveManager Instance { get; private set; }
	
	void Start () {

		currentGroove = maxGroove;

		if (Instance == null) {
			Instance = this;
		} else {
			Debug.LogError ("A scene should only have one GrooveManager");
		}
	}

	void Update () {

		currentGroove -= Time.deltaTime * depletionRate;
		if(currentGroove < 0f) {
			currentGroove = 0f;
		}
	}

}
