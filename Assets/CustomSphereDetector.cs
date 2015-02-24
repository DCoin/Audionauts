using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof (SphereCollider))]
public class CustomSphereDetector : MonoBehaviour {


    public AudioSource audioSource;


    public Transform targetTransform;
    public SphereCollider targetCollider;

    private SphereCollider myCollider;

    public float lastDistance;

    public Transform playingParent;

    private List<AudioSource> playing = new List<AudioSource>();

	// Use this for initialization
	void Start () {

        lastDistance = CalcDistance();

        myCollider = GetComponent<SphereCollider>();

	}

    private float CalcDistance()
    {
        return transform.position.z - targetTransform.position.z;
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

    private void ResolveCollision()
    {
        Vector2 a = transform.position;
        Vector2 b = targetTransform.position;

        float dist = (a - b).magnitude;

        float ra = myCollider.radius;
        float rb = targetCollider.radius;

        if (dist > ra + rb)
        {
            OnMiss();
        }
        else if (dist > Mathf.Abs(ra - rb))
        {
            OnOverlap();
        }
        else
        {
            OnHit();
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
        PlaySource(audioSource);
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
