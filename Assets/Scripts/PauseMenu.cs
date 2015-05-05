using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using InControl;
using Assets.Scripts.Managers;

public class PauseMenu : MonoBehaviour {

	public GameObject pauseMenu;
	public Slider slider;
	public EventSystem eventSystem;

	private float defaultTimeScale;

	void Start () {
		var volume = PlayerPrefs.GetFloat("Volume", 1);
		AudioListener.volume = volume;
		slider.value = volume;
		defaultTimeScale = Time.timeScale;
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
		Cursor.visible = true;
		eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
	}
	
	public void UnPause() {
		MusicManager.Instance.UnPause();
		Time.timeScale = defaultTimeScale;
		pauseMenu.SetActive (false);
		Cursor.visible = false;
	}
	
	public bool IsPaused {
		get { return Time.timeScale == 0; }
	}

	public void Restart() {
		Time.timeScale = defaultTimeScale;
		Application.LoadLevel(Application.loadedLevel);
	}

	public void Quit() {
		Time.timeScale = defaultTimeScale;
		Application.LoadLevel("Menu");
	}

	public void Volume (float volume) {
		AudioListener.volume = volume;
		PlayerPrefs.SetFloat("Volume", volume);
	}
}
