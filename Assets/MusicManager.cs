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
		get { return (float)GetComponent<AudioSource>().timeSamples / GetComponent<AudioSource>().clip.samples;}
	}

	public float BPS {

		get { 
			AudioSource audio = GetComponent<AudioSource>();
			return BeatsPerLoop / audio.clip.length;
		}

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
		if (GetComponent<AudioSource>().isPlaying) {
			if (GetComponent<AudioSource>().timeSamples < LastSamples) {
				_timesPlayed++;
			}

			LastSamples = GetComponent<AudioSource>().timeSamples;
		}

	    
	}

    void OnGUI()
    {

        GUI.TextArea(new Rect(0f, 0f, 256f, 32f), BeatsPlayed.ToString());

    }
}
