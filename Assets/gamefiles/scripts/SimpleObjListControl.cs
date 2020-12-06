using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using CSharpCompiler;

public class SimpleObjListControl : MonoBehaviour
{

    public bool selecting = false;
    public Text buttonText;
    public Controller controller;
    public bool isObjectList = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindWithTag("Controller").GetComponent<Controller>();
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(){
        if(isObjectList){
            buttonText.text = "..";
        }
        else{
            string[] path = controller.filelistcurrent.Split("/"[0]);
            buttonText.text = "Go back to \"<b>"+(path.Length < 3 ? "Assets" : path[path.Length-2])+"</b>\"";
        }
    }

    public void goBack(){
    	controller.loadObjects2(controller.objlistcurrent.transform.parent.gameObject);
    }

    public void goBack2(){
    	List<string> path = controller.filelistcurrent.Split("/"[0]).ToList();
    	path.RemoveRange(path.Count()-1, 1);
        if(!selecting){
            controller.loadFiles2(String.Join("/", path.ToArray()));
        }
        else{
            controller.loadFiles2(String.Join("/", path.ToArray()));
        }
    }
}

