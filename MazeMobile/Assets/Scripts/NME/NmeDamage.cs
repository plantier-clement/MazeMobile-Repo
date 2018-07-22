using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Nme))]
public class NmeDamage : MonoBehaviour {



	[SerializeField] float damageOnContact = 1f;



	void OnTriggerEnter2D(Collider2D coll){

		if (coll.tag != "Player")
			return;

		Destructible hp = coll.GetComponent <PlayerHealth> ();
		hp.TakeDamage (damageOnContact);
	}

}
