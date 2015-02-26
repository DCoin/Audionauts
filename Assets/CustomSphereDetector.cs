using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof (Radius))]
public class CustomSphereDetector : MonoBehaviour {

    public AudioSource audioSource;

    public Transform targets;

    private Radius radius;

    public float lastDistance;

    public Transform playingParent;

    private List<AudioSource> playing = new List<AudioSource>();

    public BarController grooveBar;

    public int points;

	// Use this for initialization
	void Start () {

        lastDistance = CalcDistance();

        radius = GetComponent<Radius>();

	}

    private float CalcDistance()
    {
        return transform.position.z - targets.position.z;
    }
	
	// Update is called once per frame
	void Update () {

        float distance = CalcDistance();

        if (lastDistance > 0f && distance < 0f)
        {
            ResolveCollision();
        }

        lastDistance = distance;

        for (int i = playing.Count - 1; i >= 0; i--)
        {
            AudioSource source = playing[i];

            if (!source.isPlaying) {
                playing.Remove(source);
                GameObject.Destroy(source.gameObject);
            }

        }

	}

    public float dist;

    private void ResolveCollision()
    {

        foreach (Radius targetRadius in targets.GetComponentsInChildren<Radius>())
        {
            Transform targetTransform = targetRadius.transform;

            Vector2 a = transform.position;
            Vector2 b = targetTransform.position;

            dist = (a - b).magnitude;

            float ra = radius.value;
            float rb = targetRadius.value;

            if (dist > ra + rb)
            {
                OnMiss();
            }
            else if (dist <= Mathf.Abs(rb - ra))
            {
                OnHit();
            }
            else
            {
                OnOverlap();
            }
        }

    }

    private void OnMiss()
    {
        //Debug.Log("Complete miss.");
    }

    private void OnOverlap()
    {
        //Debug.Log("Overlap.");
    }

    private void OnHit()
    {
        if (audioSource != null)
        {
            PlaySource(audioSource);
        }

        grooveBar.CurrentValue += points;

        //Debug.Log("Inside.");
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
