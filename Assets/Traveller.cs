using UnityEngine;
using System.Collections.Generic;

public class Traveller : MonoBehaviour {


    public AudioSource sourceCross1;
    public AudioSource sourceCircle1;
    public AudioSource sourceTriangle1;
    public AudioSource sourceSquare1;
    public AudioSource sourceCross2;
    public AudioSource sourceCircle2;
    public AudioSource sourceSquare2;
    public AudioSource sourceTriangle2;

    private AudioSource[] sources1;
    private AudioSource[] sources2;

    public Transform playingParent;


    private List<AudioSource> playing = new List<AudioSource>();

    public bool enablePitchControl;

	// Use this for initialization
	void Start () {

        sources1 = new AudioSource[] { 
            sourceCross1,
            sourceCircle1,
            sourceSquare1,
            sourceTriangle1
        };

        sources2 = new AudioSource[] {
            sourceCross2,
            sourceCircle2,
            sourceSquare2,
            sourceTriangle2          
        };
	    
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = transform.position;

        pos.x = Input.GetAxis("Horizontal");
        pos.y = Input.GetAxis("Vertical");

        transform.position = pos;

        AudioSource[] sources = GetR1() ? sources2 : sources1;

        if (GetCrossDown()) {
            PlaySource(sources[0]);
        }

        if (GetCircleDown())
            PlaySource(sources[1]);

        if (GetSquareDown())
            PlaySource(sources[2]);


        if (GetTriangleDown())
            PlaySource(sources[3]);

        for (int i = playing.Count - 1; i >= 0; i--)
        {
            AudioSource source = playing[i];

            if (source.isPlaying)
            {
                if(enablePitchControl)
                    source.pitch = (pos.y + 2f) / 2f;
            }
            else
            {

                playing.Remove(source);
                GameObject.Destroy(source.gameObject);
            }

        }

	}

    private void PlaySource(AudioSource source)
    {
        AudioSource instance = (AudioSource) GameObject.Instantiate(source);
        instance.transform.position = this.transform.position;
        instance.transform.parent = playingParent;
        instance.Play();
        playing.Add(instance);
    }

    public static bool GetCrossDown()
    {
        return Input.GetKeyDown(KeyCode.Joystick2Button0);
        //return Input.GetButtonDown("Fire1");
    }

    public static bool GetCircleDown()
    {
        return Input.GetKeyDown(KeyCode.Joystick2Button1);
        //return Input.GetButtonDown("Fire2");
    }

    public static bool GetSquareDown() 
    {
        return Input.GetKeyDown(KeyCode.Joystick2Button2);
        //return Input.GetButtonDown("Fire3");
    }

    public static bool GetTriangleDown()
    {
        return Input.GetKeyDown(KeyCode.Joystick2Button3);
        //return Input.GetButtonDown("Jump");
    }

    public static bool GetR1()
    {
        return Input.GetKey(KeyCode.Joystick2Button5);
    }

}
