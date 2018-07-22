using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour {


	[SerializeField] GameObject bridgeIn;
	[SerializeField] GameObject bridgeOut;
	[SerializeField] GameObject linkPrefab;
	public float factor = 0.25f;

	Transform goal;

	BridgeDetector bridgeInDetect;
	BridgeDetector bridgeOutDetec;


	void Awake(){
		goal = GameManager.Instance.LevelManager.Goal.transform;

	}


	void Start (){
		
		OrientBridgesColl ();
		GetBridgeDetectorRef ();
		SetSidesForBridges ();

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


	void SetSidesForBridges(){
		bridgeInDetect.SetSides (bridgeIn, bridgeOut);
		bridgeOutDetec.SetSides (bridgeOut, bridgeIn);
	}


	void SpawnLink(){


		GameObject copy = Instantiate (linkPrefab, Vector3.zero, Quaternion.identity, this.transform);
		copy.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((goal.position.y - copy.transform.position.y), (goal.position.x - copy.transform.position.x)) * Mathf.Rad2Deg);
		copy.transform.position = Vector3.Lerp (bridgeIn.transform.position, bridgeOut.transform.position, 0.5f);

		RectTransform rt = copy.GetComponent <RectTransform>();

		float height;
		height = Vector3.Distance (bridgeIn.transform.localPosition, bridgeOut.transform.localPosition);
		rt.sizeDelta = new Vector2(height * factor, rt.sizeDelta.y);
		//copy.transform.SetParent (this.transform, true);

	}


	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawLine (bridgeIn.transform.position, bridgeOut.transform.position);
		
	}
}
