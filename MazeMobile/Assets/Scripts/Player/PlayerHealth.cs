using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerHealth : Destructible {


	[SerializeField] float respawnTime;
	[SerializeField] Transform spawnPoint;


	public override void Die ()	{
		base.Die ();

		GameManager.Instance.EventBus.RaiseEvent ("OnPlayerDeath");

		GameManager.Instance.InputController.SetInputMode (InputController.EInputMode.MENU);


		GameManager.Instance.Timer.Add (PlayerRespawn, respawnTime);

	}


	void PlayerRespawn(){
		ResetHP ();
		SpawnAtSpawnPoint ();
		GameManager.Instance.InputController.SetInputMode (InputController.EInputMode.INGAME);

	}


	void SpawnAtSpawnPoint(){

		transform.position = spawnPoint.position;
		transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((GameManager.Instance.LevelManager.Goal.transform.position.y - transform.position.y), (GameManager.Instance.LevelManager.Goal.transform.position.x - transform.position.x)) * Mathf.Rad2Deg);
	
	}


	public void SetSpawnPoint(Transform newSpawnPoint){
		spawnPoint = newSpawnPoint;
	}

}
