using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour {

	[SerializeField] int layerId;
	public float radius;

	void OnDrawGizmos (){

		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, new Vector3 (transform.position.x, transform.position.y - radius, transform.position.z));
	}




}
