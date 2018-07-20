using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {


	[SerializeField] GameObject m_Goal;
	public GameObject Goal {
		get {
			if (m_Goal == null)
				m_Goal = GameObject.Find ("GOAL");
			return m_Goal;
		}
	}


	private GameObject m_Start;
	public GameObject Start {
		get {
			if (m_Start == null)
				m_Start = GameObject.Find ("START");
			return m_Start;
		}
	}


	private GameObject m_Player;
	public GameObject Player {
		get {
			if (m_Player == null)
				m_Player = GameObject.Find ("Player");
			return m_Player;
		}
	}

}
