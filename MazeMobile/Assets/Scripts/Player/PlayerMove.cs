using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {


	[SerializeField] float playerSpeed;
	[SerializeField] float bridgeSpeed;
	[SerializeField] AnimationCurve bridgeCurve;

	[SerializeField] Transform startBridgeIn;
	[SerializeField] Transform startBridgeOut;

	float playerCurrentRadius;
	bool canCrossBridge;

	Transform start;
	Transform goal;

	Transform bridgeThisSide;
	Transform bridgeOtherSide;

	float playerMovement;
	int currentLayerIndex;


	private InputController m_InputController;
	private InputController InputController {
		get {
			if (m_InputController == null)
				m_InputController = GameManager.Instance.InputController;
			return m_InputController;
		}
	}
		

	void Start(){
		start = GameManager.Instance.LevelManager.Start.transform;
		goal = 	GameManager.Instance.LevelManager.Goal.transform;
		PuzzleStart ();
	}


	void FixedUpdate(){
		UpdateMovement ();
		MoveAround ();
		FaceTowardsGoal ();

	}


	void PuzzleStart(){
		transform.position = start.position;
		bridgeThisSide = startBridgeIn;
		bridgeOtherSide = startBridgeOut;
	}


	void UpdateMovement (){
		if(InputController.MoveCW){
			playerMovement = -playerSpeed;
			return;
		}

		if(InputController.MoveCCW){
			playerMovement = playerSpeed;
			return;
		}

		if (InputController.MoveCloser && canCrossBridge && bridgeThisSide.tag == "BridgeIn") {
			CrossBridge ();
			return;
		}

		if (InputController.MoveAway && canCrossBridge && bridgeThisSide.tag == "BridgeOut") {
			CrossBridge ();
			return;
		}

		if (!InputController.IsMoving) {
			playerMovement = 0;
			return;
		}

	}


	void MoveAround(){
		transform.RotateAround (goal.position, Vector3.forward, playerMovement * Time.deltaTime);
	}


	void FaceTowardsGoal(){
		transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((goal.position.y - transform.position.y), (goal.position.x - transform.position.x)) * Mathf.Rad2Deg);
	}


	void CrossBridge(){


		/*
		Vector3 desiredPosition = bridgeOtherSide.position;
		transform.position = Vector3.MoveTowards (transform.position, desiredPosition, bridgeSpeed);
		*/
		StartCoroutine (LerpCross (bridgeSpeed));


	}


	IEnumerator LerpCross (float duration) {
		float elapsedTime = 0;
		Vector3 desiredPosition = bridgeOtherSide.position;

		while (elapsedTime <= duration) {
			elapsedTime += Time.deltaTime;


			float percent = Mathf.Clamp01(elapsedTime / duration);
			float curvePercent = bridgeCurve.Evaluate (percent);
			transform.position = Vector3.LerpUnclamped (transform.position, desiredPosition, curvePercent);

			yield return null;
		}
		transform.position = desiredPosition;
	}


	public void SetCanCrossBridge (bool value, Transform depart, Transform arrival){
		canCrossBridge = value;
		bridgeThisSide = depart;
		bridgeOtherSide = arrival;

	}


	public void SetCanCrossBridge (bool value){
		canCrossBridge = value;
	}

}
