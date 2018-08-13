using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class PlayerHealth : Destructible {


	[SerializeField] float respawnTime;

	Transform spawnPoint;
	Transform defaultSpawnPoint;

	PlayerMove playerMove;
	RawImage img;
	[SerializeField] Color defaultColor;
	[SerializeField] Color deadColor;

	void Start(){

		defaultSpawnPoint = GameManager.Instance.LevelManager.Start.transform;
		SetSpawnPoint (defaultSpawnPoint);
		img = this.GetComponentInChildren <RawImage>();
		playerMove = this.GetComponent <PlayerMove> ();

	}


	public override void Die ()	{
		base.Die ();


		GameManager.Instance.EventBus.RaiseEvent ("OnPlayerDeath");
		GameManager.Instance.InputController.SetInputMode (InputController.EInputMode.MENU);


		SetPlayerColor (deadColor);
		GameManager.Instance.Timer.Add (PlayerRespawn, respawnTime);

	}


	void PlayerRespawn(){
		ResetHP ();
		SpawnAtSpawnPoint ();
		playerMove.UpdatePlayerLayer (GameManager.Instance.LevelManager.StartLayer);

		SetPlayerColor (defaultColor);
		GameManager.Instance.InputController.SetInputMode (InputController.EInputMode.INGAME);

	}


	void SpawnAtSpawnPoint(){
		transform.position = spawnPoint.position;
		transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((GameManager.Instance.LevelManager.Goal.transform.position.y - transform.position.y), 
            (GameManager.Instance.LevelManager.Goal.transform.position.x - transform.position.x)) * Mathf.Rad2Deg);
	}


	public void SetSpawnPoint(Transform newSpawnPoint){
		spawnPoint = newSpawnPoint;
	}


	void SetPlayerColor (Color color){
		img.color = color;
	}


}
