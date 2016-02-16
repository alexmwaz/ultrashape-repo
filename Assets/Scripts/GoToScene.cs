using UnityEngine;
using System.Collections;

public class GoToScene : MonoBehaviour {

	public string Scene;
	private Collider mButtonCollider;

	// Use this for initialization
	void Start () {
		mButtonCollider = gameObject.GetComponent<MeshCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected virtual void OnEnable()
	{
		// Hook into the OnFingerTap event
		Lean.LeanTouch.OnFingerTap += OnFingerTap;
	}

	protected virtual void OnDisable()
	{
		// Unhook into the OnFingerTap event
		Lean.LeanTouch.OnFingerTap -= OnFingerTap;
	}

	public void OnFingerTap(Lean.LeanFinger finger)
	{
		if (finger.IsOverGui == false && mButtonCollider != null)
		{
			Ray hit = finger.GetRay();
			RaycastHit hitResult;

			if (mButtonCollider.Raycast(hit, out hitResult, 10.0f))
			{
				Application.LoadLevel(Scene);
			}
		}
	}
}
