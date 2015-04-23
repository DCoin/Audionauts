using UnityEngine;
using System.Collections;
using InControl;
using Assets.Scripts.Managers;

public class PauseMenu : MonoBehaviour {

	public GameObject pauseMenu;

	void Start () {
	}

	void Update () {
		if (ControllerManager.Controllers[0].MenuWasPressed || ControllerManager.Controllers[1].MenuWasPressed) {
			if (IsPaused) UnPause();
			else Pause ();
		}
	}
	
	public void Pause() {
		MusicManager.Instance.Pause();
		Time.timeScale = 0;
		pauseMenu.SetActive (true);
	}
	
	public void UnPause() {
		MusicManager.Instance.UnPause();
		Time.timeScale = 1;
		pauseMenu.SetActive (false);
	}
	
	public bool IsPaused {
		get { return Time.timeScale == 0; }
	}
}
