using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager {

	private GameObject gameObject;
	public bool IsGamePaused = false;


	private static GameManager m_Instance;
	public static GameManager Instance {
		get {
			if (m_Instance == null) {
				m_Instance = new GameManager ();
				m_Instance.gameObject = new GameObject ("_gameManager");
				m_Instance.gameObject.AddComponent <InputController>();
				m_Instance.gameObject.AddComponent <Timer>();
				m_Instance.gameObject.AddComponent <LevelManager>();

                if(Application.isPlaying) {
                    UnityEngine.Object.DontDestroyOnLoad(Instance.gameObject);
                }
            }
			return m_Instance;
		}
	}


	private InputController m_InputController;
	public InputController InputController {
		get {
			if (m_InputController == null)
				m_InputController = gameObject.GetComponent <InputController> ();
			return m_InputController;
		}
	}


	private Timer m_Timer;
	public Timer Timer {
		get {
			if (m_Timer == null)
				m_Timer = gameObject.GetComponent <Timer> ();
			return m_Timer;
		}
	}


	private EventBus m_EventBus;
	public EventBus EventBus{
		get {
			if (m_EventBus == null)
				m_EventBus = new EventBus ();
			return m_EventBus;
		}
	}


	private LevelManager m_levelManager;
	public LevelManager LevelManager {
		get {
			if (m_levelManager == null)
                m_levelManager = gameObject.GetComponent <LevelManager> ();
			return m_levelManager;
		}
	}


	private Player m_Player;
	public Player Player{
		get {
			return m_Player;
		}
		set {
			m_Player = value;
		}
	}


	private LevelFlow m_LevelFlow;
	public LevelFlow LevelFlow {
		get {
			return m_LevelFlow;
		}
		set {
			m_LevelFlow = value;
		}
	}


    public void PauseGame() {
        if(!IsGamePaused)
            IsGamePaused = true;
    }


    public void UnpauseGame() {
        if (IsGamePaused)
            IsGamePaused = false;
    }

}