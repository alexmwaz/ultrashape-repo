using UnityEngine;

// This script will spawn a prefab when you tap the screen
public class ChunkPlace : MonoBehaviour
{
	public GameObject[] Vertices;
	public float Treshold;

	private Collider[] mPieceCollider;
	private int mCount;

	// Use this for initialization
	void Start () {
		mCount = 0;
		mPieceCollider = new Collider[5];
		mPieceCollider[0] = transform.FindChild("chunk-p1").GetComponent<BoxCollider>();
		mPieceCollider[1] = transform.FindChild("chunk-p2").GetComponent<BoxCollider>();
		mPieceCollider[2] = transform.FindChild("chunk-p3").GetComponent<BoxCollider>();
		mPieceCollider[3] = transform.FindChild("chunk-p4").GetComponent<BoxCollider>();
		mPieceCollider[4] = transform.FindChild("chunk-p5").GetComponent<BoxCollider>();
		//Vertices = new GameObject[6];
		//mTrackableBehaviour = GetComponent<TrackableBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
		if (mCount == 5) {
			mCount = -1;
			transform.FindChild("chunk-p1-in").gameObject.SetActive(false);
			transform.FindChild("chunk-p2-in").gameObject.SetActive(false);
			transform.FindChild("chunk-p3-in").gameObject.SetActive(false);
			transform.FindChild("chunk-p4-in").gameObject.SetActive(false);
			transform.FindChild("chunk-p5-in").gameObject.SetActive(false);
			transform.FindChild("chunk-e1").gameObject.SetActive(false);
			transform.FindChild("chunk-e2").gameObject.SetActive(false);
			transform.FindChild("base").gameObject.SetActive(true);
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

	public void swap(int i) {
		switch(i) {
		case 0:
			transform.FindChild("chunk-p1").gameObject.SetActive(false);
			transform.FindChild("chunk-p1-in").gameObject.SetActive(true);
			break;
		case 1:
			transform.FindChild("chunk-p2").gameObject.SetActive(false);
			transform.FindChild("chunk-p2-in").gameObject.SetActive(true);
			break;
		case 2:
			transform.FindChild("chunk-p3").gameObject.SetActive(false);
			transform.FindChild("chunk-p3-in").gameObject.SetActive(true);
			break;
		case 3:
			transform.FindChild("chunk-p4").gameObject.SetActive(false);
			transform.FindChild("chunk-p4-in").gameObject.SetActive(true);
			break;
		case 4:
			transform.FindChild("chunk-p5").gameObject.SetActive(false);
			transform.FindChild("chunk-p5-in").gameObject.SetActive(true);
			break;
		default:
			break;
		}
		mCount++;
	}
	
	public void OnFingerTap(Lean.LeanFinger finger)
	{
		// Does the prefab exist?
		if (true)
		{
			// Make sure the finger isn't over any GUI elements
			if (finger.IsOverGui == false)
			{
				Ray hit = finger.GetRay();
				RaycastHit hitResult;
				var position = hit.origin;

				Vector3 referenceForward;
				Vector3 newDirection;
				float angle1, angle2, angle3;

				for (int i = 0; i < 5; i++) {
					if (mPieceCollider[i] == null)
						continue;

					if (mPieceCollider[i].Raycast(hit, out hitResult, 100.0f)) // see if collide
					{
						int j = i * 6;
						referenceForward = Vertices[j].transform.position - position;
						newDirection = Vertices[j+3].transform.position - position;
						angle1 = Vector3.Angle(newDirection, referenceForward);
						
						referenceForward = Vertices[j+1].transform.position - position;
						newDirection = Vertices[j+4].transform.position - position;
						angle2 = Vector3.Angle(newDirection, referenceForward);
						
						referenceForward = Vertices[j+2].transform.position - position;
						newDirection = Vertices[j+5].transform.position - position;
						angle3 = Vector3.Angle(newDirection, referenceForward);
						
						if (angle1 <= Treshold && angle2 <= Treshold && angle3 <= Treshold)
						{
							//position = hitResult.point;
							//var rotation = Quaternion.identity;
							//var clone    = (GameObject)Instantiate(Prefab, position, rotation);
							// Make sure the prefab gets destroyed after some time
							//Destroy(clone, 2.0f);
							swap (i);
							mPieceCollider[i] = null;
							break;
						}
						break;
					}
				}
			}
		}
	}
}