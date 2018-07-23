using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class InputController : MonoBehaviour {


	public enum EInputMode{
		MENU,
		INGAME
	}

	public EInputMode InputMode = EInputMode.INGAME;
	public bool IsMoving;

	public bool MoveCW;
	public bool MoveCCW;

	public bool MoveCloser;
	public bool MoveAway;

	public bool Pause; 


	void Update(){

		if (InputMode == EInputMode.INGAME) { 
			#if UNITY_ANDROID
			MoveCCW = CrossPlatformInputManager.GetAxis ("Horizontal") > 0;
			MoveCW = CrossPlatformInputManager.GetAxis ("Horizontal") < 0;
			MoveCloser = CrossPlatformInputManager.GetButton ("MoveCloser");
			MoveAway = CrossPlatformInputManager.GetButton ("MoveAway");
			Pause = CrossPlatformInputManager.GetButton ("Pause");
			#endif


			#if UNITY_EDITOR
			MoveCCW = Input.GetAxis ("Horizontal") > 0 || CrossPlatformInputManager.GetAxis ("Horizontal") > 0;
			MoveCW = Input.GetAxis ("Horizontal") < 0 || CrossPlatformInputManager.GetAxis ("Horizontal") < 0;
			MoveCloser = Input.GetButtonDown ("MoveCloser") || CrossPlatformInputManager.GetButtonDown ("MoveCloser");
			MoveAway = Input.GetButtonDown ("MoveAway") || CrossPlatformInputManager.GetButtonDown ("MoveAway");
			Pause = Input.GetButtonDown ("Pause") || CrossPlatformInputManager.GetButtonDown ("Pause");
			#endif

			IsMoving = MoveCCW || MoveCW;
			return;

		}


		if (InputMode == EInputMode.MENU) {
			#if UNITY_ANDROID
			Pause = CrossPlatformInputManager.GetButton ("Pause");
			#endif


			#if UNITY_EDITOR
			Pause = Input.GetButtonDown ("Pause") || CrossPlatformInputManager.GetButtonDown ("Pause");
			#endif
		
		}

	}

	public void SetInputMode (EInputMode newMode) {
		InputMode = newMode;

	}

}