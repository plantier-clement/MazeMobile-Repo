using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelFlow : MonoBehaviour {

    [Header("Level Names")]
    [SerializeField] string[] levels;
    [Space(5)]

    [Header("Level Start")]
	[SerializeField] float puzzleStartTimer;
    [Space(5)]

    [Header("Level End")]
	[SerializeField] float goalTimer;
    [SerializeField] float victoryScreenTimer;
    [Range(0,5)] [SerializeField] float defeatScreenTimer;


    bool isSingleLevel = false;


    void OnEnable() {
        SceneManager.sceneLoaded += Handle_OnLevelLoaded;
    }


    void OnDisable() {
        SceneManager.sceneLoaded -= Handle_OnLevelLoaded;
    }


    void Awake(){
		GameManager.Instance.LevelFlow = this;

        if (levels.Length == 1)
            isSingleLevel = true;

    }


    void Handle_OnLevelLoaded(Scene scene, LoadSceneMode mode) {

        if (GameManager.Instance.InputController.InputMode != InputController.EInputMode.MENU)
            GameManager.Instance.InputController.SetInputMode(InputController.EInputMode.MENU);

        GameManager.Instance.PauseGame();
        GameManager.Instance.Timer.Add(PuzzleStart, puzzleStartTimer);

    }


    void PuzzleStart() {
        GameManager.Instance.UnpauseGame();
        GameManager.Instance.InputController.SetInputMode(InputController.EInputMode.INGAME);

    }


	public void TriggerGoal(){
		GameManager.Instance.Timer.Add(GoalTriggered, goalTimer);
	}


	private void GoalTriggered(){
	
		print("trigger goal");

	}


}
