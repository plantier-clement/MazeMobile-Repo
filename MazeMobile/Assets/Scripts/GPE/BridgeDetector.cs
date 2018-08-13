using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeDetector : MonoBehaviour {

	public int layerIndex;

	PlayerMove playerMove;

	GameObject thisSide;
	GameObject otherSide;


	void Start(){
	
		if (layerIndex == GameManager.Instance.LevelManager.GoalLayer)
			transform.position = GameManager.Instance.LevelManager.Goal.transform.position;
	}



	void OnTriggerEnter2D (Collider2D coll) {
	
		if (coll.tag != "Player")
			return;
	
		playerMove = coll.GetComponent <PlayerMove>();
		playerMove.SetCanCrossBridge (true, thisSide, otherSide);

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
