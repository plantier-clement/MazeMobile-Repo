using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class InputController : MonoBehaviour {

	public bool IsMoving;

	public bool MoveCW;
	public bool MoveCCW;

	public bool MoveCloser;
	public bool MoveAway;


	void Update(){


		#if UNITY_ANDROID
		MoveCCW = CrossPlatformInputManager.GetAxis ("Horizontal") > 0;
		MoveCW = CrossPlatformInputManager.GetAxis ("Horizontal") < 0;
		MoveCloser = CrossPlatformInputManager.GetButton ("MoveCloser");
		MoveAway = CrossPlatformInputManager.GetButton ("MoveAway");
		#endif

		#if UNITY_EDITOR
		MoveCCW = Input.GetAxis ("Horizontal") > 0 || CrossPlatformInputManager.GetAxis ("Horizontal") > 0;
		MoveCW = Input.GetAxis ("Horizontal") < 0 || CrossPlatformInputManager.GetAxis ("Horizontal") < 0;
		MoveCloser = Input.GetButtonUp ("MoveCloser") || CrossPlatformInputManager.GetButton ("MoveCloser");
		MoveAway = Input.GetButtonUp ("MoveAway") || CrossPlatformInputManager.GetButton ("MoveAway");
		#endif

		IsMoving = MoveCCW || MoveCW;
		return;
	}

}