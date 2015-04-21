using UnityEngine;
using System.Collections;

public class GrooveManager : MonoBehaviour {
	
	public static GrooveManager Instance { get; private set; }

	[HideInInspector]
	public float Groove = 0;
	public float Decay = 0.999f;

	void Start () {
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Debug.LogError("A scene should only have one GrooveManager");
		}
	}

	void Update () {
		Groove *= Decay;
	}
	
	void OnGUI() {
		
		GUI.Label(new Rect(32, 50, 128, 128), Groove.ToString());
		
	}
}
