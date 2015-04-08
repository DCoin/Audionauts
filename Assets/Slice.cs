using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public class Slice : MonoBehaviour {

	public delegate void HitEventHandler(object sender);

	public event HitEventHandler Hit;

	public ColorTable colorTable;

	public enum SliceType { First, Second, Both, Any, None };

	public enum HitType { First, Second, Both };

	public SliceType sliceType;

	public float angle;

	public AudioSource audioSource;

	public Transform playingParent;

	private List<AudioSource> playing = new List<AudioSource>();

	private AudioSource mAudio;

	void Start() {
		mAudio = GetComponent<AudioSource>();
	}

	void Update() {

		for (int i = playing.Count - 1; i >= 0; i--)
		{
			AudioSource source = playing[i];
			
			if (!source.isPlaying) {
				playing.Remove(source);
				GameObject.Destroy(source.gameObject);
			}
			
		}


	}

	public Color GetColor() {

		switch (sliceType) {
			case SliceType.First:
				return colorTable.colorFirst;
			case SliceType.Second:
				return colorTable.colorSecond;
			case SliceType.Both:
				return colorTable.colorBoth;
			case SliceType.Any:
				return colorTable.colorAny;
			case SliceType.None:
				return colorTable.colorNone;
			default:
				return Color.gray;
		}

	}

	void OnValidate() {

		SpriteRenderer r = GetComponent<SpriteRenderer>();
		r.color = GetColor();

	}

	public void OnHit(HitType hit) {

		Animation a = this.GetComponent<Animation> ();

		if (a != null) {

			a.Play();

		}

		if (sliceType == SliceType.First && hit == HitType.First
			|| sliceType == SliceType.Second && hit == HitType.Second
			|| sliceType == SliceType.Both && hit == HitType.Both
			|| sliceType == SliceType.Any) {

			AcceptHit ();
		} else {
			DeclineHit();
		}

	}

	private void AcceptHit() {

		if(Hit != null) {
			Hit(this);
		}

		PlaySound();

	}

	public void PlaySound() {

		if (audioSource != null)
		{
			PlaySource(audioSource);
		}
		if (mAudio != null) {
			mAudio.Play();
		}


	}

	private void DeclineHit() {

	}

	private void PlaySource(AudioSource source)
	{
		AudioSource instance = (AudioSource)GameObject.Instantiate(source);
		instance.transform.position = this.transform.position;
		instance.transform.parent = playingParent;
		instance.Play();
		playing.Add(instance);
	}
}
