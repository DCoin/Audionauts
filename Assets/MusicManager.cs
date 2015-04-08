using UnityEngine;
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

		var beat = BeatsPlayed;
		var bar = beat / 4.0f;
        var beatOfBar = (bar - ((int)bar)) * 4.0f + 1.0f;

        var text = string.Format("Beat:\n{0}\nBar:\n{1}\nBeat of Bar:\n{2}", beat, bar, beatOfBar);

        GUI.TextArea(new Rect(0f, 0f, 256f, 100f), text);
		
    }
}
