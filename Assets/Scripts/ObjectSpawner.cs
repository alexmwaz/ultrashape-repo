using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vuforia;

public class ObjectSpawner : MonoBehaviour {

    public Transform Prefab;

    private Collider mSpawningCollider;

    private List<Transform> clones;

    private TrackableBehaviour mTrackableBehaviour;

	// Use this for initialization
	void Start () {
        mSpawningCollider = transform.FindChild("SpawningPlane").GetComponent<MeshCollider>();
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
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
        // Does the prefab exist?
        if (Prefab != null)
        {
            // Make sure the finger isn't over any GUI elements
            if (finger.IsOverGui == false)
            {
                // Clone the prefab, and place it on the spawning plane where the finger was tapped
                Ray hit = finger.GetRay();
                RaycastHit hitResult;

                if (mSpawningCollider.Raycast(hit, out hitResult, 100.0f))
                {
                    Transform clone = GameObject.Instantiate(Prefab) as Transform;
                    clone.transform.position = hitResult.point;

                    // Set image target as parent
                    clone.transform.parent = transform;

                    // Set image target's TrackableBehaviour component as parent (in the event that the above does not work)
                    //clone.transform.parent = mTrackableBehaviour.transform;

                    clone.rotation = Quaternion.identity;

                    clones.Add(clone);
                }
            }
        }
    }
}
