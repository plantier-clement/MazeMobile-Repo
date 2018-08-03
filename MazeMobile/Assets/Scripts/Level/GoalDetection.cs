using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GoalDetection : MonoBehaviour {




	void OnTriggerEnter2D(Collider2D coll){

		if(coll.tag != "Player")
			return;

		GameManager.Instance.LevelFlow.TriggerGoal();

	}



}
