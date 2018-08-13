using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            return;
		}

        if (GameManager.Instance.InputController.Quit) {
            QuitToMenu("MainMenu");
        }
	}


	void PauseGame(){
		Time.timeScale = 0;
		escapeMenuPanel.SetActive (true);
		GameManager.Instance.InputController.SetInputMode (InputController.EInputMode.MENU);
        GameManager.Instance.PauseGame();
	}


	void ResumeGame(){
		Time.timeScale = 1;
		escapeMenuPanel.SetActive (false);
		GameManager.Instance.InputController.SetInputMode (InputController.EInputMode.INGAME);
        GameManager.Instance.UnpauseGame();
	}


    void QuitToMenu(string name) {
        SceneManager.LoadScene(name);
    }
}
