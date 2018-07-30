using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	[SerializeField] GameObject escapeMenuPanel;


	void Awake () {
		escapeMenuPanel.SetActive (false);
	}


	void Update () {

		if (GameManager.Instance.InputController.Pause) {
		
			if (!GameManager.Instance.IsGamePaused) {
				PauseGame ();
				return;
			}

			if (GameManager.Instance.IsGamePaused) {
				ResumeGame ();
				return;
			}
		}
	}


	void PauseGame(){
		Time.timeScale = 0;
		escapeMenuPanel.SetActive (true);
		GameManager.Instance.InputController.SetInputMode (InputController.EInputMode.MENU);
		GameManager.Instance.IsGamePaused = true;
	}


	void ResumeGame(){
		Time.timeScale = 1;
		escapeMenuPanel.SetActive (false);
		GameManager.Instance.InputController.SetInputMode (InputController.EInputMode.INGAME);
		GameManager.Instance.IsGamePaused = false;
	}
}
