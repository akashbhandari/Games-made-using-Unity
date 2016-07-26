using UnityEngine;
using System.Collections;
using UnityEngine.UI; 		//a namespace

public class ballMovement : MonoBehaviour {		//all varibales here

	public float speed;
	public Text countText;
	public Text winText;
	public float jump;
	const float increasingSpeed = 1.5f;

	private Rigidbody rb;
	private int count;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText (); //used for counting
		winText.text = "";
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal") * increasingSpeed;
		float moveVertical = Input.GetAxis ("Vertical")* increasingSpeed;
		float jump = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, jump, moveVertical);

		rb.AddForce (movement * speed);

	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();

		}
	}

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= 14)
		{
			winText.text = "You Win!";
		}
	}
}



