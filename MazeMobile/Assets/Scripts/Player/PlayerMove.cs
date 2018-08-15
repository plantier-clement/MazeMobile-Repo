using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMove : MonoBehaviour {


	[System.Serializable]
	public class playerSpeed {
	
		public float innerSpeed = 80;
		public float mediumSpeed = 60;
		public float outerSpeed = 40;
	}

	[SerializeField] playerSpeed speed;
	[SerializeField] float bridgeCrossTime;
	[SerializeField] AnimationCurve bridgeCurve;

	[SerializeField] GameObject startBridgeIn;
	[SerializeField] GameObject startBridgeOut;

	bool canCrossBridge;
	bool canMoveAround;
	float speedToUse;

	Transform start;
	Transform goal;

	GameObject bridgeThisSide;
	GameObject bridgeOtherSide;

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

	void Awake(){
		start = GameManager.Instance.LevelManager.Start.transform;
		goal = 	GameManager.Instance.LevelManager.Goal.transform;
	}


	void Start(){
		PuzzleStart ();
	}


	void FixedUpdate(){
		
		FaceTowardsGoal ();
		if (!GameManager.Instance.Player.PlayerHealth.IsAlive)
			return;

        if (GameManager.Instance.IsGamePaused)
            return;
		
		UpdateMovement ();
		MoveAround ();
	}


	void PuzzleStart(){
		transform.position = start.position;
		currentLayerIndex = GameManager.Instance.LevelManager.StartLayer;

		bridgeThisSide = startBridgeIn;
		bridgeOtherSide = startBridgeOut;

	}


	void UpdateMovement (){

		if(InputController.MoveCW){
			playerMovement = -speedToUse;
			return;
		}

		if(InputController.MoveCCW){
			playerMovement = speedToUse;
			return;
		}

		if (InputController.MoveCloser && bridgeThisSide.tag == "BridgeIn" && canCrossBridge) {
			CrossBridge ();
			return;
		}

		if (InputController.MoveAway && bridgeThisSide.tag == "BridgeOut" && canCrossBridge) {
			CrossBridge ();
			return;
		}

		if (!InputController.IsMoving) {
			playerMovement = 0;
			return;
		}

	}


	void FaceTowardsGoal(){
		transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((goal.position.y - transform.position.y), (goal.position.x - transform.position.x)) * Mathf.Rad2Deg);
	}


	void MoveAround(){
		transform.RotateAround (goal.position, Vector3.forward, playerMovement * Time.deltaTime);
	}


	void CrossBridge(){
		StartCoroutine (LerpCross (bridgeCrossTime));

	}


	IEnumerator LerpCross (float duration) {
		float elapsedTime = 0;
		Vector3 desiredPosition = bridgeOtherSide.transform.position;

		while (elapsedTime <= duration) {
			elapsedTime += Time.deltaTime;

			float percent = Mathf.Clamp01(elapsedTime / duration);
			float curvePercent = bridgeCurve.Evaluate (percent);
			transform.position = Vector3.LerpUnclamped (transform.position, desiredPosition, curvePercent);

			yield return null;
		}
		transform.position = desiredPosition;

		UpdatePlayerLayer (bridgeThisSide);

	}



	void UpdatePlayerLayer (GameObject bridge) {
		currentLayerIndex = bridge.GetComponent<BridgeDetector> ().LayerID;
		UpdateSpeedByLayer ();
	}


	public void UpdatePlayerLayer (int newLayer) {
		currentLayerIndex = newLayer;
		UpdateSpeedByLayer ();
	}

	public void SetCanCrossBridge (bool value, GameObject depart, GameObject arrival){
		canCrossBridge = value;
		bridgeThisSide = depart;
		bridgeOtherSide = arrival;

	}


	public void SetCanCrossBridge (bool value){
		canCrossBridge = value;
	}


	void UpdateSpeedByLayer (){
		if(currentLayerIndex == 0)
			speedToUse = speed.innerSpeed;

		if (currentLayerIndex == 1)
			speedToUse = speed.mediumSpeed;

		if (currentLayerIndex == 2)
			speedToUse = speed.outerSpeed;

		if (currentLayerIndex >= GameManager.Instance.LevelManager.NbOfLayers)
			speedToUse = 0;

	}
}
