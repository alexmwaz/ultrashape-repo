using UnityEngine;
using System.Collections;

public class ExitMenu : MonoBehaviour {

	public GameObject Button;
	public GameObject Square1, Square2;
	public GameObject Play, Plane;
	public Vector3 GrowVector;
	private Collider mButtonCollider;
	private bool mEnterScene;

	// Use this for initialization
	void Start () {
		mButtonCollider = Button.GetComponent<MeshCollider>();
		mEnterScene = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (mEnterScene) {
			if (Plane.transform.position.z >= -13.0f)
			{
				Plane.transform.Translate(Vector3.down * Time.deltaTime * Plane.transform.position.z * 1.6f);
				Square1.transform.localScale += (GrowVector * Time.deltaTime);
				Square2.transform.localScale += (GrowVector * Time.deltaTime);
			}
			else {
				Application.LoadLevel("CHUNK");
			}
		}
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
				mButtonCollider = null;
				mEnterScene = true;
				Play.gameObject.SetActive(false);
			}
		}
	}
}
