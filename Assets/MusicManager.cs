using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class MusicManager : MonoBehaviour {
	
	public AudioClip bell1;
	public AudioClip bell2;
	public AudioClip bell3;
	public AudioClip bell4;
	public AudioSource s1;
	public AudioSource s2;
	public AudioSource s3;
	
	public List<AudioSource> circle = new List<AudioSource>();
	private int circlePosition = 0;
	public int mode = 0;

	private int actionsClicks = 0;

	private int samplesPerBeat;

	void Start() {
		samplesPerBeat = GetComponent<AudioSource>().clip.samples / 32;
	}

	void FixedUpdate () {
		var inputDevice = InputManager.ActiveDevice;
		switch (mode) {
		case 0:
			if (inputDevice.Action4.WasPressed) {
				switch (actionsClicks) {
				case 0:
					Play (s1);
					break;
				case 1:
					Play (s2);
					break;
				case 2:
					Play (s3);
					break;
				}
				actionsClicks++;
			}
			break;
		case 1:
			if (inputDevice.Action1.WasPressed) {
				if (s1.isPlaying) Stop(s1);
				else Play (s1);
			}
			if (inputDevice.Action2.WasPressed) {
				if (s2.isPlaying) Stop(s2);
				else Play (s2);
			}
			if (inputDevice.Action3.WasPressed) {
				if (s3.isPlaying) Stop(s3);
				else Play (s3);
			}
			break;
		case 2:
			if (inputDevice.Action1.WasPressed) {
				Play (bell1, samplesToNextBeat());
			}
			if (inputDevice.Action2.WasPressed) {
				Play (bell2, samplesToNextBeat());
			}
			if (inputDevice.Action3.WasPressed) {
				Play (bell3, samplesToNextBeat());
			}
			if (inputDevice.Action4.WasPressed) {
				Play (bell4, samplesToNextBeat());
			}
			break;
		}

		if (Input.GetKeyDown(KeyCode.K)) {
			x += 1000;
			Debug.Log ("k = " + x);
		}
		if (Input.GetKeyDown(KeyCode.L)) {
			var sample1 = GetComponent<AudioSource>().timeSamples;
			var tSample1 = GetComponent<AudioSource>().time;
			int k = 0;
			for (int i = 0; i < x; i++) {
				for (int j = 0; j < x; j++) {
					k++;
				}
			}
			var sample2 = GetComponent<AudioSource>().timeSamples;
			var tSample2 = GetComponent<AudioSource>().time;
			Debug.Log("k = " + k + "diff = " + (sample2 - sample1));
			Debug.Log ("tdiff = " + (tSample2-tSample1));
		}
	}

	int x = 1000;

	private int samplesToNextBeat () {
		return samplesPerBeat - (GetComponent<AudioSource>().timeSamples % samplesPerBeat);
	}
	
	private void Play (AudioSource source) {
		source.Play ();
		source.timeSamples = GetComponent<AudioSource>().timeSamples;
		Debug.Log ("PLAY");
	}

	private void Play (AudioClip clip, int delay = 0, int sample = 0) {
		circle [circlePosition].clip = clip;
		circle [circlePosition].timeSamples = sample;
		circle [circlePosition].Play ((ulong)delay);
		circlePosition = (circlePosition + 1) % circle.Count;
		Debug.Log ("PLAY2");
	}
	
	private void Stop (AudioSource source) {
		source.Stop ();
		source.timeSamples = 0;
	}
}