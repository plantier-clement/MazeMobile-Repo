using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public float NodePosX;
    public float NodePosY;
    public int NodeLayerId;
    public float LineLength;


    private void OnDrawGizmos() {

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(LineLength, 0, 0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(-LineLength, 0, 0));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, LineLength, 0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -LineLength, 0));
    }

}
