using UnityEngine;
using System.Collections;
using Vuforia;

public class LostMessage : MonoBehaviour {

	public GameObject tracking;
	public GameObject message;
	private TrackableBehaviour mTrackableBehaviour;

	// Use this for initialization
	void Start () {
		mTrackableBehaviour = tracking.GetComponent<TrackableBehaviour> ();
	}

	/*public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			message.SetActive (false);
		}
		else
		{
			message.SetActive (true);
		}
	}*/

	// Update is called once per frame
	void Update () {
		if (mTrackableBehaviour.CurrentStatus == TrackableBehaviour.Status.DETECTED ||
			mTrackableBehaviour.CurrentStatus == TrackableBehaviour.Status.TRACKED ||
			mTrackableBehaviour.CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
			if (message.activeSelf)
				message.SetActive (false);
		} else if (!message.activeSelf) {
			message.SetActive (true);
		} else {
			// do nothing	
		}
	}
}
