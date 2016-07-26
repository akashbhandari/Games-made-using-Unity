using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	// this is for tracking the player which is an empty game object attached to the player
	public Transform track;

	// this is the movement speed of the enemy after spawning while following the player
	private float moveSpeed = 15;

	// animation of the enemies
	public Animator enemyAnim;


	void Start(){

		//getting the animator component for the enemy
		enemyAnim = GetComponent<Animator> ();
	}

	void Update () {

		//making enemy rotate around the player using AI rotation
		transform.LookAt (track);

		//creating a variable for moving the enemy after spawning from some point
		float move = moveSpeed * Time.deltaTime;

		//transforming the position of the spawn enemy to track the player
		transform.position = Vector3.MoveTowards (transform.position, track.position, move);

		//so animating the enemies so that they'd move as well as animate according to the player position
		if (move != 0) {
			enemyAnim.SetBool ("isWalking", true);
		} else {
			enemyAnim.SetBool ("isWalking", false);
		}
	
	}
}
