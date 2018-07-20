using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour {


	[SerializeField] GameObject bridgeIn;
	[SerializeField] GameObject bridgeOut;

	Transform goal;

	BridgeDetector bridgeInDetect;
	BridgeDetector bridgeOutDetec;




	void Start (){
		

		goal = GameManager.Instance.LevelManager.Goal.transform;

		bridgeIn.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((goal.position.y - bridgeIn.transform.position.y), (goal.position.x - bridgeIn.transform.position.x)) * Mathf.Rad2Deg);
		bridgeOut.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((goal.position.y - bridgeOut.transform.position.y), (goal.position.x - bridgeOut.transform.position.x)) * Mathf.Rad2Deg);


		bridgeInDetect = bridgeIn.GetComponent <BridgeDetector> ();
		bridgeOutDetec = bridgeOut.GetComponent <BridgeDetector> ();

		bridgeInDetect.SetSides (bridgeIn, bridgeOut);
		bridgeOutDetec.SetSides (bridgeOut, bridgeIn);
	}


	void OnDrawGizmos(){

		Gizmos.color = Color.blue;
		//Gizmos.DrawLine (bridgeIn.transform.position, bridgeOut.transform.position);

	}



}
