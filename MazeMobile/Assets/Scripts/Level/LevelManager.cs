using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {


	private GameObject m_Goal;
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


	private GameObject[] m_Layers;
	public GameObject[] Layers {
		get {
			if (m_Layers == null)
				m_Layers = GameObject.FindGameObjectsWithTag ("Layer");
			return m_Layers;
		}
	}


	private int m_NbOfLayers;
	public int NbOfLayers {
		get {
			if (m_NbOfLayers == 0)
				m_NbOfLayers = Layers.Length;
			return m_NbOfLayers;
		}
	}


}
