using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Nme))]
public class NmeHealth : Destructible {


	public override void Die ()	{
		base.Die ();

		GameManager.Instance.EventBus.RaiseEvent ("OnNmeDeath");


	}



}
