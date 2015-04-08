﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using UnityEditor;

[RequireComponent (typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
	public int BeatsPerLoop = 32;

	private int _timesPlayed = 0;
	private int LastSamples = 0;

    public Transform startAtBar;

    private bool positionSet = false;


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

		    if (!positionSet)
		    {

		        var startTime = 0f;

		        if (startAtBar != null)
		        {
                    startTime = startAtBar.localPosition.z;
                }

                startTime /= BPS;

                var src = GetComponent<AudioSource>();


		        var l = src.clip.length;

		        _timesPlayed = 0;

		        while (startTime >= l)
		        {
		            startTime -= l;
		            _timesPlayed++;

		        }

                src.time = startTime;

		        positionSet = true;
		    }


			if (GetComponent<AudioSource>().timeSamples < LastSamples) {
				_timesPlayed++;
			}

			LastSamples = GetComponent<AudioSource>().timeSamples;
		}

	    
	}

    void OnGUI()
    {

		float beats = BeatsPlayed;
		float bars = beats / 4.0f;
		float beatOfBar = (bars - ((int)bars)) * 4.0f + 1.0f;

        GUI.TextArea(new Rect(0f, 0f, 256f, 32f), "Beat: " + beats.ToString());
		
		GUI.TextArea (new Rect (0f, 32f, 256f, 32f), "Bar: " + bars.ToString ());

		GUI.TextArea (new Rect (0f, 64f, 256f, 32f), "Beat of Bar: " + beatOfBar.ToString ());
    }
}
