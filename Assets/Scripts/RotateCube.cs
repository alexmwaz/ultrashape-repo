using UnityEngine;
using System.Collections;

public class RotateCube : MonoBehaviour {
	
	public Vector3 rotateVector;
	//public Vector3 pushVector;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (rotateVector * Time.deltaTime, Space.Self);
		//transform.Translate (pushVector * Time.deltaTime, Space.World);
	}
}
