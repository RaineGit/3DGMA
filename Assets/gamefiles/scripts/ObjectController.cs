using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ThreeDGMA;
using CSharpCompiler;
using RuntimeGizmos;

public class ObjectController : MonoBehaviour {

	public List<String> Scripts = new List<String>();
	public List<MonoBehaviour> LoadedScripts = new List<MonoBehaviour>();
	public object[] Components = new object[2];
	public ListObjectElement element;
	public bool selected = false;
	public GameObject material;
	public string id;
	public GameObject lightSprite;
	public MeshCollider fakeCollider;
	Controller controller;
	public Hashtable components = new Hashtable();

	// Use this for initialization
	void Start () {
		controller = GameObject.FindWithTag("Controller").GetComponent<Controller>();
		Component[] comps = gameObject.GetComponents(typeof(Component));
		foreach(Component i in comps){
			ComponentRef_3DGMA tempCompRef = new ComponentRef_3DGMA();
			tempCompRef.hashCode = i.GetHashCode();
			tempCompRef.component = i;
			components[i.GetHashCode()] = tempCompRef;
			controller.components[i.GetHashCode()] = tempCompRef;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
