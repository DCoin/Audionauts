using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFlicker : MonoBehaviour
{

    public float FlickerRate;

    private SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start ()
	{

	    _spriteRenderer = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update ()
	{

        _spriteRenderer.enabled = (((int) (Time.time * FlickerRate))%2 == 0);

	}
}
