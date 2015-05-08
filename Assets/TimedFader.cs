using Assets.Scripts.Managers;
using UnityEngine;

public class TimedFader : MonoBehaviour {

    public float duration;

    public string scene;

    void LoadScene() {

        Application.LoadLevel(scene);
    }
	
	// Update is called once per frame
	void Update () {

	    var d = duration;

	    duration -= Time.deltaTime;

	    if (duration <= 0 && d > 0) {
	        FadeManager.Instance.EndScene(LoadScene);
	    } 

	}
}
