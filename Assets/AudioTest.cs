using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioTest : MonoBehaviour {
	public AudioClip sourceClip;
	private AudioSource audio1;
	private AudioSource audio2;
	private AudioClip cutClip1;
	private AudioClip cutClip2;
	private float overlap = 0.2F;
	private int len1 = 0;
	private int len2 = 0;
	void Start() {
		GameObject child;
		child = new GameObject("Player1");
		child.transform.parent = gameObject.transform;
		audio1 = child.AddComponent<AudioSource>() as AudioSource;
		child = new GameObject("Player2");
		child.transform.parent = gameObject.transform;
		audio2 = child.AddComponent<AudioSource>() as AudioSource;
		int overlapSamples;
		if (sourceClip != null) {
			len1 = sourceClip.samples / 2;
			len2 = sourceClip.samples - len1;
			overlapSamples = (int)(overlap * sourceClip.frequency);
			cutClip1 = AudioClip.Create("cut1", len1 + overlapSamples, sourceClip.channels, sourceClip.frequency, false, false);
			cutClip2 = AudioClip.Create("cut2", len2 + overlapSamples, sourceClip.channels, sourceClip.frequency, false, false);
			float[] smp1 = new float[(len1 + overlapSamples) * sourceClip.channels];
			float[] smp2 = new float[(len2 + overlapSamples) * sourceClip.channels];
			sourceClip.GetData(smp1, 0);
			sourceClip.GetData(smp2, len1 - overlapSamples);
			cutClip1.SetData(smp1, 0);
			cutClip2.SetData(smp2, 0);
		} else {
			overlapSamples = (int)(overlap * cutClip1.frequency);
			len1 = cutClip1.samples - overlapSamples;
			len2 = cutClip2.samples - overlapSamples;
		}
	}
	void OnGUI() {
		if (GUI.Button(new Rect(10, 50, 230, 40), "Trigger source"))
			audio1.PlayOneShot(sourceClip);
		
		if (GUI.Button(new Rect(10, 100, 230, 40), "Trigger cut 1"))
			audio1.PlayOneShot(cutClip1);
		
		if (GUI.Button(new Rect(10, 150, 230, 40), "Trigger cut 2"))
			audio1.PlayOneShot(cutClip2);
		
		if (GUI.Button(new Rect(10, 200, 230, 40), "Play stitched")) {
			audio1.clip = cutClip1;
			audio2.clip = cutClip2;
			double t0 = AudioSettings.dspTime + 3.0F;
			double clipTime1 = len1;
			clipTime1 /= cutClip1.frequency;
			audio1.PlayScheduled(t0);
			audio1.SetScheduledEndTime(t0 + clipTime1);
			Debug.Log("t0 = " + t0 + ", clipTime1 = " + clipTime1 + ", cutClip1.frequency = " + cutClip1.frequency);
			Debug.Log("cutClip2.frequency = " + cutClip2.frequency + ", samplerate = " + AudioSettings.outputSampleRate);
			audio2.PlayScheduled(t0 + clipTime1);
			audio2.time = overlap;
		}
	}
}