using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSaver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public object Save(GameObject obj,bool saveinanother){
		List<object> saveobject = new List<object>();
		saveobject.Add(obj.transform.childCount > 0);
		saveobject.Add(obj.transform.position.x);
		saveobject.Add(obj.transform.position.y);
		saveobject.Add(obj.transform.position.z);
		if(obj.transform.childCount > 0){
			foreach (Transform child in obj.transform){
			saveobject.Add(Save(child.gameObject,true));
			}
		}
		
			return obj;
		
	}

}
