using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Nme))]
public class NmeMove : MonoBehaviour {


	[SerializeField] float speed;
	[SerializeField] bool clockwiseRotation;

	float movement;
	bool isMoving;
	GameObject goal;


	void Start () {
		goal = GameManager.Instance.LevelManager.Goal; 

		transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((goal.transform.position.y - transform.position.y), (goal.transform.position.x - transform.position.x)) * Mathf.Rad2Deg);

	}


	void FixedUpdate () {
        if(!GameManager.Instance.IsGamePaused)
		    Move ();
	}


	void Move(){

		movement = clockwiseRotation ? -1 : 1 ;

		transform.RotateAround (goal.transform.position, Vector3.forward, movement * speed * Time.deltaTime);
	}

}
