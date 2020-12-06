using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RuntimeGizmos;
using CSharpCompiler;

public class AddComponentButton : MonoBehaviour
{

	public GameObject objct;
	public GameObject objct2;
	public Controller controller;
	public GameObject addComponentButton;
    public GameObject addComponentMenu;
	public GameObject addScriptButton;

    // Start is called before the first frame update
    void Start()
    {
    	controller = GameObject.FindWithTag("Controller").GetComponent<Controller>();
        //gameObject.GetComponent<LayoutElement>().preferredHeight = 94.2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddComponent(string name){
        var objects = controller.selectedTransforms;
        foreach(Transform child in objects){
        	if(name == "Rigidbody"){
        		FakeComponent newComponent = child.gameObject.AddComponent<FakeComponent>();
                newComponent.ComponentName = "Rigidbody";
                newComponent.ComponentType = "UnityEngine.Rigidbody, UnityEngine.PhysicsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";
                newComponent.ComponentType2 = "UnityEngine.Rigidbody";
                newComponent.returnTo = gameObject.GetComponent<AddComponentButton>();
                newComponent.CreateFakeComponent();
        	}
            if(name == "Light"){
                child.gameObject.AddComponent<Light>();
            }
        	if(name == "BoxCollider"){
        		child.gameObject.AddComponent<BoxCollider>();
        	}
            if(name == "MeshCollider"){
                var comp = child.gameObject.AddComponent<MeshCollider>();
                if(child.gameObject.GetComponent<MeshFilter>() != null){
                    comp.sharedMesh = child.gameObject.GetComponent<MeshFilter>().mesh;
                }
            }
        }
        if(controller.selectedTransforms[0] != null)
            controller.LoadComponentList(controller.selectedTransforms[0].gameObject);
        controller.CheckSprites();
        controller.CloseAddComponentMenu();
        StartCoroutine(controller.ScrollDownComponentsList());
    }

    public void OpenMenu(){
    	//gameObject.GetComponent<LayoutElement>().preferredHeight = 447.5f;
        addComponentMenu.SetActive(true);
    	StartCoroutine(OpenMenuWait());
    }

    IEnumerator OpenMenuWait(){
    	yield return null;
    	gameObject.transform.parent.transform.parent.transform.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
    }

    public void CloseMenu(){
        addComponentMenu.SetActive(false);
    	//gameObject.GetComponent<LayoutElement>().preferredHeight = 94.2f;
    }

    public void OpenFileChooser(){
    	controller.OpenFileChooser();
		controller.selecting = true;
		controller.selectingReason = "Add Script Component";
    }
}
