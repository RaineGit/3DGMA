using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CSharpCompiler;

namespace RuntimeGizmos
{
public class ListObjectSElement : MonoBehaviour {

	public int index;
	public Text title;
	GameObject objct;
	GameObject objct2;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void set(int localindex,GameObject obj,GameObject obj2){
		objct = obj;
		objct2 = obj2;
		index=localindex;
		title.text=GameObject.FindWithTag("Controller").GetComponent<Controller>().ScriptsName[index];
	}

	public void remove(){
		for(int i=0;i<objct.GetComponent<ObjectController>().Scripts.Count;i++){
			if(objct.GetComponent<ObjectController>().Scripts[i]==GameObject.FindWithTag("Controller").GetComponent<Controller>().ScriptsName[index]){
				objct.GetComponent<ObjectController>().Scripts.Remove(objct.GetComponent<ObjectController>().Scripts[i]);
			}
		}
		objct2.GetComponent<ListObjectElement>().open();
	}
	

}
}
