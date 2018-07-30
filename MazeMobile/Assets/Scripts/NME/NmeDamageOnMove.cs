using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Nme))]
public class NmeDamageOnMove : MonoBehaviour {


	[SerializeField] float damage = 1f;

	Player player;

	bool isPlayerInside;

	void OnTriggerEnter2D (Collider2D coll) {
	
		if (coll.tag != "Player")
			return;

		player = coll.GetComponent <Player> ();
		isPlayerInside = true;

	}


	void OnTriggerExit2D (Collider2D coll) {

		if (coll.tag != "Player")
			return;

		player = null;
		isPlayerInside = false;
	}


	void Update (){
	
		if (!isPlayerInside)
			return;

		if (GameManager.Instance.InputController.IsMoving)
			player.PlayerHealth.TakeDamage (damage);
	}

}
