using UnityEngine;
using System.Collections;

public class alienMovement : MonoBehaviour {
	void Update ()
	{
		transform.Rotate (new Vector3 (0, 90, 0) * Time.deltaTime);

		//transform.Translate(Vector3.forward * Time.deltaTime);
		//transform.Translate(Vector3.up * Time.deltaTime, Space.World);
	}

}
