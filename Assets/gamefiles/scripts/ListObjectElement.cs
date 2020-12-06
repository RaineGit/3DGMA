using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CSharpCompiler;
using System.Reflection;
using System;
using RuntimeGizmos;
using UnityEngine.EventSystems;

public class ListObjectElement : MonoBehaviour, IPointerClickHandler {

	public int index;
	public Text title;
	public string id;
	public GameObject objct;
	GameObject scriptui;
	GameObject objectui;
	public GameObject enterbtn;
	public Toggle checkbox;
	Controller controller;

	// Use this for initialization
	void Start () {
		controller = GameObject.FindWithTag("Controller").GetComponent<Controller>();
		if(objct.transform.childCount == 0){
			enterbtn.SetActive(false);
		}
		scriptui = controller.scriptui;
		objectui = controller.objectui;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void set(int localindex,string text,GameObject obj){
		objct = obj;
		index = localindex;
		title.text = text;
		if(objct.GetComponent<ObjectController>() != null && objct.GetComponent<ObjectController>().selected){
			//GetComponent<Image>().color = new Color(0, 0.67f, 1, 0.18f);
			SetCheckboxOn(true);
		}
	}

	public void enter(){
		controller.loadObjListByScript(gameObject);
	}

	public void open(){
		if(objct.GetComponent<ObjectController>() != null){
			if(objct.GetComponent<ObjectController>().selected){
				controller.cam.GetComponent<TransformGizmo>().RemoveTarget(objct.transform);
				var objects = controller.selectedTransforms;
				if(objects.Count > 0){
					controller.LoadComponentList(objects[0].gameObject);
				}
				else{
					controller.LoadComponentList(null);
				}
			}
			else{
				controller.LoadComponentList(objct);
			}
		}
	}
	
	public void OnPointerClick(PointerEventData eventData){
		open();
	}

	public void SetCheckboxOn(bool isOn){
		if(checkbox.isOn != isOn){
			Mute(checkbox.onValueChanged);
			checkbox.isOn = isOn;
			Unmute(checkbox.onValueChanged);
		}
	}

	public void Mute( UnityEngine.Events.UnityEventBase ev )
	{
	    int count = ev.GetPersistentEventCount();
	    for ( int i = 0 ; i < count ; i++ )
	    {
	    	if(i != 0)
	        	ev.SetPersistentListenerState( i, UnityEngine.Events.UnityEventCallState.Off );
	    }
	}
	 
	public void Unmute( UnityEngine.Events.UnityEventBase ev )
	{
	    int count = ev.GetPersistentEventCount();
	    for ( int i = 0 ; i < count ; i++ )
	    {
	    	if(i != 0)
	        	ev.SetPersistentListenerState( i, UnityEngine.Events.UnityEventCallState.RuntimeOnly );
	    }
	}

}