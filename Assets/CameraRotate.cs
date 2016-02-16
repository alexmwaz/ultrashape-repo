using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {

	//private Vector3 mStartCoord;

	// Use this for initialization
	void Start () {
		//mStartCoord = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up);
		//this.transform.position = mStartCoord;
	}
}