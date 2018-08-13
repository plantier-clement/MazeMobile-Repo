using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nme : MonoBehaviour {


    public float NmeStartPosX;
    public float NmeStartPosY;
    public int NmeStartLayerId;



    private NmeMove m_NmeMove;
	public NmeMove NmeMove {
		get {
			if (m_NmeMove == null)
				m_NmeMove = GetComponent <NmeMove> ();
			return m_NmeMove;
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


	private NmeDamageOnHit m_NmeDamageOnHit;
	public NmeDamageOnHit NmeDamage {
		get {
			if (m_NmeDamageOnHit == null)
				m_NmeDamageOnHit = GetComponent <NmeDamageOnHit> ();
			return m_NmeDamageOnHit;
		}
	}


	private NmeDamageOnMove m_NmeDamageOnMove;
	public NmeDamageOnMove NmeDamageOnMove {
		get {
			if (m_NmeDamageOnMove == null)
				m_NmeDamageOnMove = GetComponent <NmeDamageOnMove> ();
			return m_NmeDamageOnMove;
		}
	}

}
