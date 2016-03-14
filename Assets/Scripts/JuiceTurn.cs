using UnityEngine;
using System.Collections;

public class JuiceTurn : MonoBehaviour {

	public char axis;
	public float threshold;
	public GameObject rotateHole;
	public GameObject next;
	public GameObject juice;

	private Vector3 endRotate;
	private Gyroscope gyro;
	private AudioSource placeClip;
	//private float mAxisAttitude;

	// Use this for initialization
	void Start () {
		placeClip = GameObject.Find("AUDIO_SNAP").GetComponent<AudioSource>();
		gyro = Input.gyro;
		if(!gyro.enabled)
		{
			gyro.enabled = true;
		}
		endRotate = rotateHole.transform.up;
	}
	
	// Update is called once per frame
	void Update () {
		switch (axis) {
		case 'x':
			//gameObject.transform.rotation = new Quaternion(gyro.attitude.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z, gyro.attitude.w);
			gameObject.transform.Rotate(gyro.rotationRateUnbiased.x, 0, 0);
			break;
		case 'y':
			//gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x, gyro.attitude.y, gameObject.transform.rotation.z, gyro.attitude.w);
			gameObject.transform.Rotate(0, gyro.rotationRateUnbiased.y, 0);
			break;
		case 'z':
			//gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gyro.attitude.z, gyro.attitude.w);
			gameObject.transform.Rotate (0, 0, gyro.rotationRateUnbiased.z, Space.Self);
			break;
		default:
			break;
		}

		if (Vector3.Angle (endRotate, gameObject.transform.up) < threshold) {
			if (!placeClip.isPlaying){
				placeClip.Play();
			}
			rotateHole.SetActive (true);
			juice.SetActive (true);
			next.SetActive (true);
			gameObject.SetActive (false);
		}
	}
}
