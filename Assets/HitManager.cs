using UnityEngine;
using System.Collections;

public class HitManager : MonoBehaviour {

	public Transform players;
	
	public Transform player1;
	public Transform player2;
	public ScreenShake shaker;
	public InitialPosition shakeTarget;

	public static HitManager Instance { get; private set; }

	void Start ()
	{
		if (Instance == null) {
			Instance = this;
		} else {
			Debug.LogError ("A scene should only have one HitManager");
		}
	}

}
