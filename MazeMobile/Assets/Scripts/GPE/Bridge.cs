using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour {

    [SerializeField] GameObject bridgeIn;
	[SerializeField] GameObject bridgeOut;
	[SerializeField] GameObject linkPrefab;

	Transform goal;

	BridgeDetector bridgeInDetect;
	BridgeDetector bridgeOutDetec;

	GameObject copy;

	void Awake(){
		goal = GameManager.Instance.LevelManager.Goal.transform;

	}


	void Start (){
		
		OrientBridgesColl ();
		GetBridgeDetectorRef ();
		SetSidesForBridgeDetector ();

		SpawnLink ();
	}


	void OrientBridgesColl(){
		bridgeIn.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((goal.position.y - bridgeIn.transform.position.y), (goal.position.x - bridgeIn.transform.position.x)) * Mathf.Rad2Deg);
		bridgeOut.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((goal.position.y - bridgeOut.transform.position.y), (goal.position.x - bridgeOut.transform.position.x)) * Mathf.Rad2Deg);

	}


	void GetBridgeDetectorRef(){
		bridgeInDetect = bridgeIn.GetComponent <BridgeDetector> ();
		bridgeOutDetec = bridgeOut.GetComponent <BridgeDetector> ();
	}


	void SetSidesForBridgeDetector(){
		bridgeInDetect.SetSides (bridgeIn, bridgeOut);
		bridgeOutDetec.SetSides (bridgeOut, bridgeIn);
	}


	void SpawnLink(){

		copy = Instantiate (linkPrefab, Vector3.zero, Quaternion.identity, this.transform);
		copy.transform.position = Vector3.Lerp (bridgeIn.transform.position, bridgeOut.transform.position, 0.5f);
		copy.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((goal.position.y - copy.transform.position.y), (goal.position.x - copy.transform.position.x)) * Mathf.Rad2Deg);

		SetLinkLength ();
	}


	void SetLinkLength(){
		
		RectTransform rt = copy.GetComponent <RectTransform>();

		float height;
		height = Vector3.Distance (bridgeIn.transform.localPosition, bridgeOut.transform.localPosition);
		rt.sizeDelta = new Vector2(height, rt.sizeDelta.y);
	}


	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawLine (bridgeIn.transform.position, bridgeOut.transform.position);
		
	}
}
