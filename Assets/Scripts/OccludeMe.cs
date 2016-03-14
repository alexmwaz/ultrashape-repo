using UnityEngine;
using System.Collections;

public class OccludeMe : MonoBehaviour {

	public char occludeType;

	// Use this for initialization
	void Start(){
		//Renderer[] renders = GetComponentsInChildren<Renderer> ();
		//foreach (Renderer rendr in renders){
		//	rendr.material.renderQueue = 2002; // set their renderQueue
		//}
		if (occludeType == 'A')
			gameObject.GetComponent<Renderer>().material.renderQueue = 2003;
	}
}