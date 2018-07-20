using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeDetector : MonoBehaviour {

	PlayerMove playerMove;

	GameObject thisSide;
	GameObject otherSide;


	void OnTriggerEnter2D (Collider2D coll) {
	
		if (coll.tag != "Player")
			return;
	
		playerMove = coll.GetComponent <PlayerMove>();
		playerMove.SetCanCrossBridge (true, thisSide.transform, otherSide.transform);

	}



	void OnTriggerExit2D(Collider2D coll) {
		if (coll.tag != "Player")
			return;
		
		playerMove = coll.GetComponent <PlayerMove>();
		playerMove.SetCanCrossBridge (false);
	
	}



	public void SetSides(GameObject myself, GameObject other){
		thisSide = myself;
		otherSide = other;
	}
}
