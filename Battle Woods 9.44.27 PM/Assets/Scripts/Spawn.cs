using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	//creating array for enemies for spawning them and moving them in a group 
	public GameObject[] enemies;

	//this says how many enemies are there in the game or number of enemies spawning
	public int numberOfEnemies;

	//position vector for spawning those enemies
	private Vector3 spawnPoint;

	void Update () {

		//initializing prefab called "Enemy"
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		//so number of enemies is to be equal to the length of the enemies array
		numberOfEnemies = enemies.Length;

		//if number of enemies is not equal to given number then it will continue to spawn those enemies
		if(numberOfEnemies != 50){

			//invoking enemies until the number of enemies is not equal to the given number of the enemies
			InvokeRepeating ("spawnEnemy", 1, 5f);
		}




	}

	//making the method for spawning the enemy
	void spawnEnemy()
	{
		//this is the random x position of the enemies to be spawned 
		spawnPoint.x = Random.Range (62,1901);

		//setting the enemies y position to be fixed so that it seems like coming from the underground
		spawnPoint.y = 720f;

		//setting the enemies z axis to be random 
		spawnPoint.z = Random.Range (1617,3100);

		//finally instantiating the enemies
		Instantiate (enemies[UnityEngine.Random.Range(0,enemies.Length - 1)],spawnPoint,Quaternion.identity);

		//invoke should also be canceled
		CancelInvoke ();
	}


}
