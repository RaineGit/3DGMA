using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour {

	public bool always;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(always)LookNow();
	}

	public void LookNow(){
		gameObject.transform.LookAt(Camera.main.gameObject.transform.position);
	}
}
