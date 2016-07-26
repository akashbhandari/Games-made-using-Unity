using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject Player;

	private Vector3 offset;

	void Start () 
	{
		// this was our default camera position 
		// but there was difficulty in rotating camera and seeing the other side of scene
		//offset = transform.position - Player.transform.position;
	}

	void Update () 
	{

		//transform.position = Player.transform.position + offset;

	}
}
