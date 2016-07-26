using UnityEngine;
using System.Collections;

public class FireBallShooter : MonoBehaviour {


	GameObject prefab;

	// for controlling the speed of the fire ball thrown from the player
	float speedOfFireBall = 40;

	// inheritating from another class PLayerController for animating the player when it throwns the fire ball
	PlayerController playerController;

	//this is the number of fire balls given to the player at first 
	public int numberOfFireBalls = 10;

	// Use this for initialization
	void Start () {
		
		// loading that fireball as prefab
		prefab = Resources.Load ("fireBall") as GameObject;

		// inheritating from another class PlayerController
		playerController = GetComponent<PlayerController> ();
	}

	// Update is called once per frame
	void Update () {

		//using left mouse button to through fireball into the enemies
		if(Input.GetMouseButtonDown(0)){
			
			// checking whether the number of fire balls are there or not
			if(numberOfFireBalls > 0){
				
				// for palying the audio when the player shoots the fire balls
				playerController.PlayShootClip();

				// calling ThrowBall function so that player animates and throws fire ball perfectly 
				StartCoroutine(ThrowBall ());


				//decreasing the number of fire balls by one every time player throws one fire ball
				numberOfFireBalls = numberOfFireBalls - 1;

				//decreasing the number of fireballs when player shoots it
				playerController.fireballCount = playerController.fireballCount-1;

				//setting the starting value of the UI text Text property
				playerController.SetCountText ();

			};
		}
	}
		
	IEnumerator ThrowBall() {

		// now using the class called PlayerController for playing the animation
		// to play the animation of zombie attacking
		playerController.anim.Play("zombie_attack", -1, 0f);

		// this makes player to wait for one second before thorwing the fire ball so that it looks like realistic
		yield return new WaitForSeconds (1f);

		// moving the fireball according to the position of the player and the camera position and little bit above
		Vector3 fireBallPosition = transform.position + transform.forward * 2 + (transform.up * 9);

		// instantiating the fireball from the prefab resources folder
		GameObject fireBall = Instantiate (prefab, fireBallPosition, Quaternion.identity) as GameObject;

		// getting fireBall as rigid body
		Rigidbody rb = fireBall.GetComponent<Rigidbody> ();

		//giving this fireball a velosity of forward movement along with the speed
		rb.velocity = transform.forward * speedOfFireBall;
	}

}
