using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    Scene m_CurrentScene;
    public Scene CurrentScene {
        get {
            return m_CurrentScene;
        }
    }
    int subLevelIndex;


    bool m_IsSingleLevel = false;
    public bool IsSingleLevel {
        get {
            return m_IsSingleLevel;
        }
    }
    Text levelNameTxt;
    Text subLevelNameTxt;

    void OnEnable() {
        SceneManager.sceneLoaded += Handle_OnLevelLoaded;
    }


    void OnDisable() {
        SceneManager.sceneLoaded -= Handle_OnLevelLoaded;
    }


    void Awake(){
		GameManager.Instance.LevelFlow = this;

        levelNameTxt = GameObject.Find("LevelNameText").GetComponent<Text>();
        subLevelNameTxt = GameObject.Find("SubLevelNameText").GetComponent<Text>();
    }


    void Handle_OnLevelLoaded(Scene scene, LoadSceneMode mode) {

        m_CurrentScene = scene;

        if (levels.Length == 1)
            m_IsSingleLevel = true;

        if (GameManager.Instance.InputController.InputMode != InputController.EInputMode.MENU)
            GameManager.Instance.InputController.SetInputMode(InputController.EInputMode.MENU);

        levelNameTxt.text = m_CurrentScene.name;
        subLevelNameTxt.text = (subLevelIndex + 1) + " - " + levels.Length;

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

        TransitionToNextSubLvl();

	}


    void TransitionToNextSubLvl() {

        if(subLevelIndex == levels.Length - 1) {
            LevelComplete();
            return;
        }
        print("transition to next sub level");
        subLevelIndex++;

    }


    void LevelComplete() {
        print("level complete");
    }


    public int GetCurrentSubLvlIndex() {
        return subLevelIndex;
    }

}
