using UnityEngine;
using System.Collections;

public class CameraOccluder : MonoBehaviour {

	public GameObject frontView;
	public GameObject backView;

	public GameObject [] winningSet;

	public Material notPlaced;
	public Material placed;
	public Material victory;

	public AudioSource placeClip;

	private bool finished;

	private Vector3 mFrontPos;
	private Vector3 mBackPos;

	private int direction; // 0 none, 1 front, 2 back
	private int frontItr;
	private int backItr;

	// Use this for initialization
	void Start () {
		finished = false;
		mFrontPos = frontView.transform.position;
		mBackPos = backView.transform.position;

		Renderer[] renderF = frontView.GetComponentsInChildren<Renderer> ();
		foreach (Renderer f in renderF){
			f.material.renderQueue = 2001;
		}
		Renderer[] renderB = backView.GetComponentsInChildren<Renderer> ();
		foreach (Renderer b in renderB){
			b.material.renderQueue = 2001;
		}

		direction = 0;
		frontItr = 0;
		backItr = 0;
	}

	protected virtual void OnEnable() {
		// Hook into the OnFingerTap event
		Lean.LeanTouch.OnFingerTap += OnFingerTap;
	}

	protected virtual void OnDisable() {
		// Unhook into the OnFingerTap event
		Lean.LeanTouch.OnFingerTap -= OnFingerTap;
	}

	public void OnFingerTap(Lean.LeanFinger finger) {
		if (finger.IsOverGui == false && !finished) {
			if (!placeClip.isPlaying){
				placeClip.Play();
			}
			Ray hit = finger.GetRay();
			RaycastHit hitResult;
			bool hitFirst = false;
			bool hitPlay = false;

			if (direction == 1) {
				for (int i = 0; i < frontView.transform.childCount; i++) {
					if (frontView.transform.GetChild (i).GetComponent<BoxCollider> ().Raycast (hit, out hitResult, 1000.0f)) {
						if (frontView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue == 2000) {
							frontView.transform.GetChild (i).GetComponent<Renderer> ().material = notPlaced;
							frontView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue = 2001;
						} else if (frontView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue == 2001) {
							frontView.transform.GetChild (i).GetComponent<Renderer> ().material = placed;
							frontView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue = 2000;
						}
						hitFirst = true;
						hitPlay = true;
						break;
					}
				}
				for (int i = 0; i < backView.transform.childCount; i++) {
					if (hitFirst)
						break;
					if (backView.transform.GetChild (i).GetComponent<BoxCollider> ().Raycast (hit, out hitResult, 1000.0f)) {
						if (backView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue == 2000) {
							backView.transform.GetChild (i).GetComponent<Renderer> ().material = notPlaced;
							backView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue = 2001;
						} else if (backView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue == 2001) {
							backView.transform.GetChild (i).GetComponent<Renderer> ().material = placed;
							backView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue = 2000;
						}
						hitPlay = true;
						break;
					}
				}
			} else {
				for (int i = 0; i < backView.transform.childCount; i++) {
					if (backView.transform.GetChild (i).GetComponent<BoxCollider> ().Raycast (hit, out hitResult, 1000.0f)) {
						if (backView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue == 2000) {
							backView.transform.GetChild (i).GetComponent<Renderer> ().material = notPlaced;
							backView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue = 2001;
						} else if (backView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue == 2001) {
							backView.transform.GetChild (i).GetComponent<Renderer> ().material = placed;
							backView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue = 2000;
						}
						hitFirst = true;
						hitPlay = true;
						break;
					}
				}
				for (int i = 0; i < frontView.transform.childCount; i++) {
					if (hitFirst)
						break;
					if (frontView.transform.GetChild (i).GetComponent<BoxCollider> ().Raycast (hit, out hitResult, 1000.0f)) {
						if (frontView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue == 2000) {
							frontView.transform.GetChild (i).GetComponent<Renderer> ().material = notPlaced;
							frontView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue = 2001;
						} else if (frontView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue == 2001) {
							frontView.transform.GetChild (i).GetComponent<Renderer> ().material = placed;
							frontView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue = 2000;
						}
						hitPlay = true;
						break;
					}
				}
			}

			/*Renderer[] renderF = frontView.GetComponentsInChildren<Renderer> ();
			foreach (Renderer f in renderF){
				if (f.material.renderQueue == 2000 || f.material.renderQueue == 2001) {
					if (f.GetComponent<BoxCollider> ().Raycast (hit, out hitResult, 1000.0f)) {
						if (f.material == notPlaced) {
							f.material = placed;
							f.material.renderQueue = 2000;
						} else {
							f.material = notPlaced;
							f.material.renderQueue = 2001;
						}
						break;
					}
				}
			}
				
			Renderer[] renderB = backView.GetComponentsInChildren<Renderer> ();
			foreach (Renderer b in renderB){
				if (b.material.renderQueue == 2000 || b.material.renderQueue == 2001) {
					if (b.GetComponent<BoxCollider> ().Raycast (hit, out hitResult, 1000.0f)) {
						if (b.material == notPlaced) {
							b.material = placed;
							b.material.renderQueue = 2000;
						} else {
							b.material = notPlaced;
							b.material.renderQueue = 2001;
						}
						break;
					}
				}
			}*/
		}
	}

	// 2000 : visible do not become invisible
	// 2001 : visible
	// 2002 : cull plane
	// 2003 : invisible
	// 2004 : invisible do not become visible

	void changeSides(int newDirection) {
		direction = newDirection;
		if (direction == 1) {
			frontItr = ++frontItr % frontView.transform.childCount;
			for (int i = 0; i < frontView.transform.childCount; i++) {
				if (frontView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue == 2000)
					continue;
				else if (i == frontItr)
					frontView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue = 2001;
				else
					frontView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue = 2004;
			}
		} else {
			backItr = ++backItr % backView.transform.childCount;
			for (int i = 0; i < backView.transform.childCount; i++) {
				if (backView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue == 2000)
					continue;
				else if (i == backItr)
					backView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue = 2001;
				else
					backView.transform.GetChild (i).GetComponent<Renderer> ().material.renderQueue = 2004;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (finished) {
			winningSet [0].transform.Rotate (Vector3.up * Time.deltaTime * 12, Space.World);
			winningSet [1].transform.Rotate (Vector3.down * Time.deltaTime * 12, Space.World);
			winningSet [2].transform.Rotate (Vector3.up * Time.deltaTime * 12, Space.World);
			winningSet [3].transform.Rotate (Vector3.down * Time.deltaTime * 12, Space.World);
			return;
		}
		
		if ((gameObject.transform.position - mFrontPos).sqrMagnitude <= (gameObject.transform.position - mBackPos).sqrMagnitude) {
			if (direction == 0 || direction == 2) {
				changeSides (1);
			}
			Renderer[] renderB = backView.GetComponentsInChildren<Renderer> ();
			foreach (Renderer b in renderB){
				if (b.material.renderQueue != 2000 && b.material.renderQueue != 2004)
					b.material.renderQueue = 2003;
			}
		} else {
			if (direction == 0 || direction == 1) {
				changeSides (2);
			}
			Renderer[] renderF = frontView.GetComponentsInChildren<Renderer> ();
			foreach (Renderer f in renderF){
				if (f.material.renderQueue != 2000 && f.material.renderQueue != 2004)
					f.material.renderQueue = 2003;
			}
		}

		if (winningSet [0].GetComponent<Renderer> ().material.renderQueue == 2000 &&
			winningSet [1].GetComponent<Renderer> ().material.renderQueue == 2000 &&
			winningSet [2].GetComponent<Renderer> ().material.renderQueue == 2000 &&
			winningSet [3].GetComponent<Renderer> ().material.renderQueue == 2000) {
			finished = true;

			Renderer[] renderB = backView.GetComponentsInChildren<Renderer> ();
			foreach (Renderer b in renderB){
				b.material.renderQueue = 2004;
			}
			Renderer[] renderF = frontView.GetComponentsInChildren<Renderer> ();
			foreach (Renderer f in renderF){
				f.material.renderQueue = 2004;
			}

			winningSet [0].GetComponent<Renderer> ().material.renderQueue = 2000;
			winningSet [1].GetComponent<Renderer> ().material.renderQueue = 2000;
			winningSet [2].GetComponent<Renderer> ().material.renderQueue = 2000;
			winningSet [3].GetComponent<Renderer> ().material.renderQueue = 2000;

			winningSet [0].GetComponent<Renderer> ().material = victory;
			winningSet [1].GetComponent<Renderer> ().material = victory;
			winningSet [2].GetComponent<Renderer> ().material = victory;
			winningSet [3].GetComponent<Renderer> ().material = victory;
		}
	}

	public void ButtonSkip() {
		if (!finished) {
			finished = true;

			Renderer[] renderB = backView.GetComponentsInChildren<Renderer> ();
			foreach (Renderer b in renderB) {
				b.material.renderQueue = 2004;
			}
			Renderer[] renderF = frontView.GetComponentsInChildren<Renderer> ();
			foreach (Renderer f in renderF) {
				f.material.renderQueue = 2004;
			}

			winningSet [0].GetComponent<Renderer> ().material.renderQueue = 2000;
			winningSet [1].GetComponent<Renderer> ().material.renderQueue = 2000;
			winningSet [2].GetComponent<Renderer> ().material.renderQueue = 2000;
			winningSet [3].GetComponent<Renderer> ().material.renderQueue = 2000;

			winningSet [0].GetComponent<Renderer> ().material = victory;
			winningSet [1].GetComponent<Renderer> ().material = victory;
			winningSet [2].GetComponent<Renderer> ().material = victory;
			winningSet [3].GetComponent<Renderer> ().material = victory;
		} else {
			Application.LoadLevel("MENU-D2");
		}
	}
}
