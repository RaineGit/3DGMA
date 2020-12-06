using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantFollow : MonoBehaviour {

	public GameObject target;
	public bool isLight;
	public bool isCamera;
	public Renderer sprite;
	public Transform arrow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(target == null){
			Destroy(gameObject);
		}
		else{
			if(isLight && target.GetComponent<Light>() == null){
				Destroy(gameObject);
			}
			else if(isCamera && target.GetComponent<Camera>() == null){
				Destroy(gameObject);
			}
			if(isCamera || isLight){
				arrow.rotation = target.transform.rotation;
			}
			gameObject.transform.position = target.transform.position;
		}
	}
}
