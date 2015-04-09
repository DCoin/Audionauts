using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animation))]
public class FadeController : MonoBehaviour
{

    private Animation _animation;

    public KeyCode key;

	// Use this for initialization
	void Start ()
	{
	    _animation = GetComponent<Animation>();

	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetKeyDown(key))
	    {

	        _animation.Play();

	    }
	
	}
}
