using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CSharpCompiler;
using TMPro;

namespace RuntimeGizmos
{
public class ListScriptElement : MonoBehaviour {

	public int index;
	public Text title;
	TMP_InputField input;
	InputField input2;
	InputField input3;
	GameObject scriptui;
	GameObject objectui;

	// Use this for initialization
	void Start () {
		input = GameObject.FindWithTag("Controller").GetComponent<Controller>().input;
		input2 = GameObject.FindWithTag("Controller").GetComponent<Controller>().input2;
		input3 = GameObject.FindWithTag("Controller").GetComponent<Controller>().input3;
		scriptui = GameObject.FindWithTag("Controller").GetComponent<Controller>().scriptui;
		objectui = GameObject.FindWithTag("Controller").GetComponent<Controller>().objectui;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void set(int localindex){
		index=localindex;
		title.text=GameObject.FindWithTag("Controller").GetComponent<Controller>().ScriptsName[index];
	}

	public void open(){
		input.text=GameObject.FindWithTag("Controller").GetComponent<Controller>().Scripts[index];
		input2.text=GameObject.FindWithTag("Controller").GetComponent<Controller>().ScriptsName[index];
		input3.text=GameObject.FindWithTag("Controller").GetComponent<Controller>().ScriptsClass[index];
		objectui.SetActive(false);
		scriptui.SetActive(true);
		GameObject.FindWithTag("Controller").GetComponent<Controller>().showAlert("Loaded");
	}

	public void add(){

		var workingwith = GameObject.FindWithTag("Controller").GetComponent<Controller>().currObject.GetComponent<ListObjectElement>().objct;

		if(!workingwith.GetComponent<ObjectController>()){
		workingwith.AddComponent<ObjectController>();
		}

		GameObject.FindWithTag("Controller").GetComponent<Controller>().currObject.GetComponent<ListObjectElement>().objct.GetComponent<ObjectController>().Scripts.Add(GameObject.FindWithTag("Controller").GetComponent<Controller>().ScriptsName[index]);
		GameObject.FindWithTag("Controller").GetComponent<Controller>().currObject.GetComponent<ListObjectElement>().open();
	}

}
}
