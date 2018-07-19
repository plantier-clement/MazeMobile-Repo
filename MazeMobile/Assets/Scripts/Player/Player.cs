using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {



	[SerializeField] Transform start;
	[SerializeField] Transform goal;

	[SerializeField] float playerSpeed;

	[SerializeField] float radiusSpeed;
	[SerializeField] float radiusStep;



	float playerMovement;
	float playerRadius;
	int currentLayerIndex;

	bool canMoveVert;

	private InputController m_InputController;
	private InputController InputController {
		get {
			if (m_InputController == null)
				m_InputController = GameManager.Instance.InputController;
			return m_InputController;
		}
	}



	void Start(){
		PuzzleStart ();
	}


	void FixedUpdate(){
		UpdateMovement ();
		MoveAround ();
		FaceTowardsGoal ();

	}


	void PuzzleStart(){
		transform.position = start.position;
		playerRadius = Vector3.Distance(transform.position, goal.position);
		canMoveVert = true;

	}


	void MoveAround(){
		transform.RotateAround (goal.position, Vector3.forward, playerMovement * Time.deltaTime);


		/*
		Vector3 desiredPosition = (transform.position - goal.position).normalized * playerRadius + goal.position;
		transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
		*/
		// new Vector3 (goal.position.x + (Mathf.Cos (playerPosition)*playerRadius), goal.position.y + (Mathf.Sin (playerPosition)*playerRadius), 0);

	}


	void MoveVertical(){

		Vector3 desiredPosition = (transform.position - goal.position).normalized * playerRadius + goal.position;
		transform.position = Vector3.Lerp (transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
		ToggleCanMoveVert ();
	
	}


	void FaceTowardsGoal(){
		transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((goal.position.y - transform.position.y), (goal.position.x - transform.position.x)) * Mathf.Rad2Deg);

	}


	public void UpdateMovement (){
		if(InputController.MoveCW){
			playerMovement = -playerSpeed;
			return;
		}

		if(InputController.MoveCCW){
			playerMovement = playerSpeed;
			return;
		}

		if (InputController.MoveCloser && canMoveVert) {
			ToggleCanMoveVert ();
			playerRadius -= radiusStep;
			MoveVertical ();
			return;
		}

		if (InputController.MoveAway && canMoveVert) {
			ToggleCanMoveVert ();
			playerRadius += radiusStep;
			MoveVertical ();
			return;
		}

		if (!InputController.IsMoving) {
			playerMovement = 0;
			return;
		}

	}


	void ToggleCanMoveVert(){
		canMoveVert = !canMoveVert;
	}



}
