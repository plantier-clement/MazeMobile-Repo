using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {



	public enum EInputMode {
		MENU,
		GAME
	}

	public EInputMode InputMode = EInputMode.GAME;


	public bool IsMoving;

	public bool MoveCW;
	public bool MoveCCW;

	public bool MoveCloser;
	public bool MoveAway;


	void Update(){
	
		if (InputMode == EInputMode.MENU) {
			// stuff
			return;
		}

		if (InputMode == EInputMode.GAME) {
			// stuff
			MoveCCW = Input.GetAxis ("Horizontal") > 0;
			MoveCW = Input.GetAxis ("Horizontal") < 0;
			MoveCloser = Input.GetButtonUp ("MoveCloser");
			MoveAway = Input.GetButtonUp ("MoveAway");


			IsMoving = MoveCCW || MoveCW;
			return;
		}
			
			

	}


	public void SetInputMode (EInputMode newMode) {
		InputMode = newMode;
	}
}
