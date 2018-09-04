using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

          
    public string StartLevelName;

    private void Awake() {
        GameManager.Instance.InputController.SetInputMode(InputController.EInputMode.MENU);

    }

    private void Update() {

        if (GameManager.Instance.InputController.Quit) {
            QuitGame();
            return;

        }

        if (GameManager.Instance.InputController.Start) {
            StartGame(StartLevelName);
            return;
        }

    }


    void StartGame(string name) {
        SceneManager.LoadScene(name);
    }


    void QuitGame() {
        Application.Quit();

    }

}
