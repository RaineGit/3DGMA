using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CSharpCompiler;
using RuntimeGizmos;
using cakeslice;

public class ObjectPropsElement : MonoBehaviour
{

	public InputField Name;
	public GameObject objct;
    public Controller controller;
	bool refreshStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindWithTag("Controller").GetComponent<Controller>();
        if(!refreshStarted){
			refreshStarted = true;
			StartCoroutine(UpdateData());
		}
        UpdateNameText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NameInputChanged(){
        if(Name.text != "--"){
            try{
                var objects = controller.selectedTransforms;
                foreach(Transform child in objects){
                    child.gameObject.name = Name.text;
                }
            }
            catch{
                objct.name = Name.text;
            }
        }
    }

    public void DeleteObject(){
        var objects = controller.selectedTransforms;
        foreach(Transform child in objects){
            Destroy(child.gameObject);
        }
        controller.cam.GetComponent<TransformGizmo>().ClearTargets();
        controller.LoadComponentList(null);
    }

    public void DuplicateObject(){
        var objects = new List<Transform>(controller.selectedTransforms);
        controller.cam.GetComponent<TransformGizmo>().ClearTargets();
        foreach(Transform child2 in objects){
        	var clone = Instantiate(child2.gameObject, child2.gameObject.transform.parent);
            var children = controller.GetAllGameObjects(clone);
            children.Add(clone);
            var originalChildren = controller.GetAllGameObjects(child2.gameObject);
            originalChildren.Add(child2.gameObject);
            for(int i=0; i<children.Count; i++){
                var child = children[i];
                var originalChild = originalChildren[i];
                var comps = child.GetComponents<FakeComponent>();
                var originalComps = originalChild.GetComponents<FakeComponent>();
                for(int j=0; j<comps.Length; j++){
                    var comp = comps[j];
                    var originalComp = originalComps[j];
                    if(comp.ComponentType2 != "C# Script"){
                        comp.fakeValue = Instantiate(((Component)originalComp.fakeValue).gameObject, ((Component)originalComp.fakeValue).gameObject.transform.parent).GetComponents<Component>()[1];
                    }
                    else{
                        comp.loadedScriptAsset = originalComp.loadedScriptAsset;
                        comp.fakeValues = new Hashtable();
                        var values = originalComp.fakeValues;
                        List<string> keys = new List<string>();
                        foreach(string key in values.Keys){
                            keys.Add(key);
                        }
                        foreach(string key in keys){
                            comp.fakeValues[key] = ((FakeScriptValue)originalComp.fakeValues[key]).Clone();
                        }
                    }
                }
            }
            if(clone.GetComponent<cakeslice.Outline>() != null){
                Destroy(clone.GetComponent<cakeslice.Outline>());
            }
            controller.cam.GetComponent<TransformGizmo>().AddTarget(clone.transform);
        }
        controller.LoadComponentList(controller.selectedTransforms[0].gameObject);
    }

    public void UpdateObjectList(){
    	controller.loadObjects2(controller.objlistcurrent);
    }

    void OnEnable(){
		refreshStarted = true;
		StartCoroutine(UpdateData());
	}

	IEnumerator UpdateData(){
        UpdateNameText();
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(UpdateData());
	}

    void UpdateNameText(){
        if(objct!=null){
            if(controller.multipleSelection){
                string sharedName = null;
                List<Transform> objects = controller.selectedTransforms;
                if(objects.Count >= 1){
                    string sampleName = objects[0].name;
                    bool areAllNamesTheSame = true;
                    foreach(Transform child in objects){
                        if(sampleName != child.gameObject.name){
                            areAllNamesTheSame = false;
                        }
                    }
                    sharedName = areAllNamesTheSame ? sampleName : "--";
                    if(Name.text != sharedName && !Name.isFocused)
                        Name.text = sharedName;
                }
            }
            else{
                if(Name.text != objct.name && !Name.isFocused)
                    Name.text = objct.name;
            }
        }
    }
}
