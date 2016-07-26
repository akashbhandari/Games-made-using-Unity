using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	AudioSource playerAudio;
	public AudioClip shootAudio;
	public AudioClip enemyDeathAudio; 
	public AudioClip playerDeathAudio;
	public AudioClip tresureBoxAudio;
	public AudioClip winningAudio;
	public AudioClip loseAudio;

	public Text fireballCountText;
	public int fireballCount;

	public Animator anim;
    
	// inheritating from another class FireBallShooter
	FireBallShooter fireBallShooter;

	//
	Move move;

	// for controlling the speed of the player
	private float speed = 25f;

	// for controlling the rotation of the player
	private float rotationSpeed = 100f;

	//death points of the player
	public float playerDeathPoints = 0.5f;

	// giving player a current health for loading the health bar
	public float currentHealth = 0f;

	// maximum health points given to the player 
	public float max_health = 100f;

	// giving the player a game object health bar so that user can track the health of the player
	public GameObject healthBar;

	// to count the number of fire balls
	private int countFireBall;

	// Use this for initialization
	void Start(){

		// initializing the animator controller of the player
		anim = GetComponent<Animator> ();

		// initializing the audio source
		playerAudio = GetComponent<AudioSource> ();

		// initializing the class FireBallShooter to make some changes in another class according to some effects of this class
		fireBallShooter = GetComponent<FireBallShooter> ();


		//fireball count
		fireballCount = 10;

		//setting the starting value of the UI text Text property
		SetCountText ();

		//moving
		move = GetComponent<Move> ();

		// at assigning the current health of the player to be maximum health so that we'd decrease the health bar according to the hit points of the enemies
		currentHealth = max_health;

		// setting our count to be zero at first
		countFireBall = fireBallShooter.numberOfFireBalls;
	}

		
	// Update is called once per frame
	public void Update(){

		// controlling the animations of the player according to some keys of the keyboard
		if (Input.GetKeyDown ("1")) {
			anim.Play ("breakdance_uprock_to_ground", -1, 0f);
		}

		if (Input.GetKeyDown ("2")) {
			anim.Play ("breakdance_uprock_to_ground_1", -1, 0f);
		}

		if (Input.GetKeyDown ("3")) {
			anim.Play ("guitar_playing", -1, 0f);
		}

		if (Input.GetKeyDown ("4")) {
			anim.Play ("butterfly_twirl", -1, 0f);
		}

		if (Input.GetKeyDown ("5")) {
			anim.Play ("catwalk_walk", -1, 0f);
		}

		if (Input.GetKeyDown ("6")) {
			anim.Play ("shopping_cart_dance", -1, 0f);
		}

		if (Input.GetKeyDown ("7")) {
			anim.Play ("tut_hip_hop_dance", -1, 0f);
		}

		if (Input.GetKeyDown ("8")) {
			anim.Play ("yawn", -1, 0f);
		}


		// making some connections with player through addition of various animations 
		// that is player dies or does something interesting when we touch or click it
		if (Input.GetMouseButtonDown (1)) {

			// randomness between 0 and 5
			int n = Random.Range (0, 5);

			switch (n) {
			case 0:
				anim.Play ("death_from_front_headshot", -1, 0f);
				break;
			case 1:
				anim.Play ("Standing_React_Death_Bsckward", -1, 0f);
				break;
			case 2:
				anim.Play ("death", -1, 0f);
				break;
			case 3:
				anim.Play ("zombie_death", -1, 0f);
				break;
			case 4:
				anim.Play ("flying_back_death", -1, 0f);
				break;
			default:
				anim.Play ("dying_backwards", -1, 0f);
				break;
			}
		
		}
			
			// moving the player accoring to the arrow keys pressed
			float translation = Input.GetAxis ("Vertical") * speed;
			float rotation = Input.GetAxis ("Horizontal") * rotationSpeed;

			translation *= Time.deltaTime;
			rotation *= Time.deltaTime;
			transform.Translate (0, 0, translation);
			transform.Rotate (0, rotation, 0);	

			//its working totally opposite of the code so I have no idea why its behaving totally different?
			if (Input.GetAxis ("Vertical") == 0)
				playerAudio.Play ();

			// animating the player according to his movements
			if (translation != 0) {
				anim.SetBool ("isRunning", true);
			} else {

				anim.SetBool ("isRunning", false);
			}
		}
		
	//
	void OnTriggerEnter(Collider other) {
		// checking whether the player collides our treasure box or not 
		// if it collides than doing two things at a time
		if (other.gameObject.CompareTag("Pick Up")) {

			//
			PlayTreasureBoxClip();

			//increasing the number of fire balls every time player gets the treasure boxes by 10
			fireBallShooter.numberOfFireBalls = fireBallShooter.numberOfFireBalls + 10;

			//adding fireball when player touches the treasure box
			fireballCount = fireballCount +10;

			//setting the starting value of the UI text Text property
			SetCountText ();

			// and then setting it to false that is destroying it
			other.gameObject.SetActive(false);
		}
			

		// checking whether the player triggers the enemies 
		// if so then manipulating player death points which is health of the player depending on the enemies hit points
		if(other.gameObject.CompareTag("Enemy")){

			// playing the player's screaming sound when it loses it's all health points
			PlayPlayerDeathClip();

			//
			//move.enemyAnim.SetBool ("isEnemyAttacking", true);

		
			// checking whether the player's heath (player dead points) is greater than 10 or not 
			// which is logically assigning certain health points to the player
			if(playerDeathPoints >= max_health){

				// this is for making the whole game to wait for a second so that we'd show the dead animation of player before the restarting of the game
				StartCoroutine(PlayerDeadAnimationTimerMethod());

				//losing audio when player is dead
				PlayLosingClip ();

			}

			// similarly if player triggers : logically speaking, if enemies hits the player then increasing the death points of the player 
			playerDeathPoints = playerDeathPoints + 1;

			// for decreasing the player's health according to the player bar
			InvokeRepeating("DecreaseHealth",0f,0f);

		}

		if (other.gameObject.CompareTag ("Girl")) {
			StartCoroutine (PresentingWinText());
		}

	}

	//fireballCount function
	public void SetCountText () {
		//setting the starting value of the UI text Text property
		fireballCountText.text = "Fire Balls: " + fireballCount.ToString ();
	}

	// win text method
	// using the same fireballs number text i.e same canvas
	// overriding with win text
	public void WinText(){
		
		//
		PlayWinningClip();

		fireballCountText.text = "You Won";
		fireballCountText.color = Color.black;
		//fireballCountText.fontSize = 10;
		fireballCountText.transform.position = new Vector2 (Screen.width/2,Screen.height/2);
	}


	//When player throws the bomb this audio is played.
	public void PlayShootClip () {
		playerAudio.PlayOneShot(shootAudio, 1.0f);
		//Debug.Log ("play shoot");
	}


	//When player throws the bomb and if it hits the player than this sound clip is on
	public void PlayEnemyDeathClip () {
		playerAudio.PlayOneShot(enemyDeathAudio, 1.0f);
	}

	//When player losses it's health than this audio is played
	public void PlayPlayerDeathClip () {
		playerAudio.PlayOneShot(playerDeathAudio, 1.0f);
	}

	//
	public void PlayTreasureBoxClip(){
		playerAudio.PlayOneShot (tresureBoxAudio, 1.0f);
	}

	//
	public void PlayWinningClip(){
		playerAudio.PlayOneShot (winningAudio, 1.0f);
	}

	public void PlayLosingClip() {
		playerAudio.PlayOneShot (loseAudio, 1.0f);

	}

	// creating DecreaseHealth method to use it in invoke repeating
	void DecreaseHealth(){
		// so decreasing the player's health one point by another point
		currentHealth -= 1f;

		// clamping the player health according to the health bar of the player 
		// so that we'd decrease the health bar according to the enemies hit points
		float calc_Health = currentHealth / max_health;

		// and then also calling another function called SetHealthBar
		// where we will be able to decrease the health bar according to the player health points available and enemies hit points
		SetHealthBar(calc_Health);
	}

	// creating the SetHealthBar method for tracking the progression of the health decreasement of the player according the given health points of the player
	public void SetHealthBar(float myHealth){

		// so transforming the healthbar position with different color 
		// so that we'd so the progression of the decreasement of the player health points
		healthBar.transform.localScale = new Vector3 (myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}

	//
	public IEnumerator PlayerDeadAnimationTimerMethod() {


		// playing player dead animation for 3 seconds
		anim.SetBool ("isDead", true);

		// waiting for three seconds
		yield return new WaitForSeconds(2f);

		// after three seconds restarting the game
		// after the player death 
		// giving user to play it many chances
		// that is user will be able to continue it
		Application.LoadLevel(0);
	}

	public IEnumerator PresentingWinText(){

		WinText ();

		yield return new WaitForSeconds (2.5f);

		Application.LoadLevel (0);
	}

}
	