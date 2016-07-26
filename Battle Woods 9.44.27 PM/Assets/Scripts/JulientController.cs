using UnityEngine;
using System.Collections;

public class JulientController : MonoBehaviour {

	Animator julientAnim;
	// Use this for initialization
	void Start () {
		julientAnim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Translate(Vector3.forward * Time.deltaTime);
		//transform.Translate(Vector3.up * Time.deltaTime, Space.World);
		julientAnim.SetBool ("isWalking", true);
	}
}
