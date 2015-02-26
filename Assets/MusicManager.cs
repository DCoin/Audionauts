using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
	public int BeatsPerLoop = 32;

	private int _timesPlayed = 0;
	private int LastSamples = 0;

	public static MusicManager Instance { get; private set; }

	public float Position {
		get { return (float)audio.timeSamples / audio.clip.samples;}
	}

	public float TimesPlayed {
		get { return Position + _timesPlayed;}
	}

	public float BeatsPlayed {
		get { return TimesPlayed * BeatsPerLoop; }
	}

	void Start ()
	{
		if (Instance == null) {
			Instance = this;
		} else {
			Debug.LogError ("A scene should only have one MusicManager");
		}
	}
	
	void Update ()
	{
		if (audio.isPlaying) {
			if (audio.timeSamples < LastSamples) {
				_timesPlayed++;
			}

			LastSamples = audio.timeSamples;
		}
	}
}
