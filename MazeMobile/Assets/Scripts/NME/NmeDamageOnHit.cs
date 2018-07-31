using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Nme))]
public class NmeDamageOnHit : MonoBehaviour {



	[SerializeField] float damageOnContact = 1f;


	void OnTriggerEnter2D(Collider2D coll){

		if (coll.tag != "Player")
			return;

		Destructible playerHealth = coll.GetComponent <PlayerHealth> ();

		if (!playerHealth.IsAlive)
			return;
		
		playerHealth.TakeDamage (damageOnContact);
	}

}
