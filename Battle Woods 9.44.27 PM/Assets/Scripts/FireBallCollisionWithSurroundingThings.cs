using UnityEngine;
using System.Collections;

public class FireBallCollisionWithSurroundingThings : MonoBehaviour {

	// for inheriting the 
//	PlayerController playerController;

	void Start(){
		//playerController = GetComponent<PlayerController>();
	}
		

	void OnTriggerEnter(Collider other) {
		// checking whether the fire ball collides enemy or not
		if (other.gameObject.CompareTag("Enemy")) {

			// similarly palying the enemy screaming sound when it gets hit from the fire ball
			//playerController.PlayEnemyDeathClip();
			GetComponent<AudioSource>().Play();

			//and then setting it to false that is destroying it
			other.gameObject.SetActive(false);


		}
	}

	// this is the script function which is going to send trigger events
	/*void OnTriggerEnter(Collider col){

		switch(col.tag)
		{
		case "Enemy":
			Destroy (GameObject.FindWithTag("Enemy"));
			break;
		
		case "Treasure":
			break;

		case "House":
			break;

		case "Trees":
			break;

		case "Girl":
			break;

		case "Bridge":
			break;

		case "Terrains":
			break;
		
			

		}

	}*/

	/*
	//this is the script function which is going to send collision events 
	void OnCollisionEnter(Collider col){

		switch(col.gameObject.tag){

			case "Enemy":
				Destroy (GameObject.FindWithTag("Enemy"));
				break;

			case "Treasure":
				break;

			case "House":
				break;

			case "Trees":
				break;

			case "Girl":
				break;

			case "Bridge":
				break;

			case "Terrains":
				break;
				
		}

	}*/


}
