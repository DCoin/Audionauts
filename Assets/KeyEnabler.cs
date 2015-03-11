﻿using UnityEngine;
using System.Collections;

public class KeyEnabler : MonoBehaviour {

	public GameObject[] objects;

	public KeyCode key;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (key)) {
			foreach(GameObject obj in objects) {

				obj.SetActive(true);
			}
		}
	
	}
}
