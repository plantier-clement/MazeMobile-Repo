using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


	private PlayerMove m_PlayerMove;
	public PlayerMove PlayerMove {
		get {
			if (m_PlayerMove == null)
				m_PlayerMove = GetComponent <PlayerMove> ();
			return m_PlayerMove;
		}
	}


	private PlayerHealth m_PlayerHealth;
	public PlayerHealth PlayerHealth {
		get {
			if (m_PlayerHealth == null)
				m_PlayerHealth = GetComponent <PlayerHealth> ();
			return m_PlayerHealth;
		}
	}


	void Awake() {
		GameManager.Instance.Player = this;
	}
	

}
