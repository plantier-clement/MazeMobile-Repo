using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nme : MonoBehaviour {


	private NmeMove m_NmeMove;
	public NmeMove NmeMove {
		get {
			if (m_NmeMove == null)
				m_NmeMove = GetComponent <NmeMove> ();
			return m_NmeMove;
		}
	}


	private NmeDamage m_NmeDamage;
	public NmeDamage NmeDamage {
		get {
			if (m_NmeDamage == null)
				m_NmeDamage = GetComponent <NmeDamage> ();
			return m_NmeDamage;
		}
	}


	private NmeHealth m_NmeHealth;
	public NmeHealth NmeHealth {
		get {
			if (m_NmeHealth == null)
				m_NmeHealth = GetComponent <NmeHealth> ();
			return m_NmeHealth;
		}
	}

}
