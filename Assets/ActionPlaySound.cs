using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ActionPlaySound : MenuAction {

    public AudioClip[] Sounds;

    private AudioSource source;

    private System.Random rnd;

    public override void Execute() {

        int i = rnd.Next(0, Sounds.Length);

        AudioClip clip = Sounds[i];

        source.clip = clip;

        source.Play();

    }

    void Start() {

        source = GetComponent<AudioSource>();

        rnd = new System.Random();

    }

}
