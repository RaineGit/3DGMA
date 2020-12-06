using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CSharpCompiler;
using System.Reflection;
using System;
using System.Linq;
using MaterialUI;

public class HiddenProperties
{
	public string name;
	public string[] properties;
}

namespace RuntimeGizmos
{
public class ComponentElement : MonoBehaviour {

	public int index;
	public Text title;
	GameObject objct;
	GameObject objct2;
	List<HiddenProperties> HP = new List<HiddenProperties>();
	public Transform VarList;
	public Toggle EnabledToggle;
	public Component component;
	bool hasEnabledProperty = false;
	bool doRefresh = true;
	bool refreshStarted = false;
	public Button OpenMenuButton;
	public Button OpenButton;
	public Button BackButton;
	public GameObject CompVarList;
	public GameObject Scrollbar;
	public GameObject menu;
	public MaterialUI.MenuArrowAnim menuButtonAnim;
	bool fakeComponent = false;
	bool script = false;
	Controller controller;
	float animStartTime;
	float animDeltaTime;

	bool anim1 = false;
	bool anim2 = false;

	// Use this for initialization
	void Start () {
		controller = GameObject.FindWithTag("Controller").GetComponent<Controller>();
		StartCoroutine(CheckOffscreen());
	}
	
	// Update is called once per frame
	void Update () {
		animDeltaTime = Time.realtimeSinceStartup - animStartTime;

		if(anim1)
		{
			if(animDeltaTime <= 0.3f)
			{
				menu.GetComponent<RectTransform>().sizeDelta = new Vector2(menu.GetComponent<RectTransform>().sizeDelta.x, Anim.Quint.SoftOut(0, 100f, animDeltaTime, 0.3f));
			}
			else{
				menu.GetComponent<RectTransform>().sizeDelta = new Vector2(menu.GetComponent<RectTransform>().sizeDelta.x, 100f);
				anim1 = false;
			}
		}

		if(anim2)
		{
			if(animDeltaTime <= 0.3f)
			{
				menu.GetComponent<RectTransform>().sizeDelta = new Vector2(menu.GetComponent<RectTransform>().sizeDelta.x, Anim.Quint.SoftOut(100f, 0, animDeltaTime, 0.3f));
			}
			else{
				menu.GetComponent<RectTransform>().sizeDelta = new Vector2(menu.GetComponent<RectTransform>().sizeDelta.x, 0f);
				anim2 = false;
				menu.SetActive(false);
			}
		}
	}

	public void set(int localindex, GameObject obj, GameObject obj2, Component comp, bool isFakeComponent=false, bool isScript=false){
		objct = obj;
		objct2 = obj2;
		index = localindex;
		fakeComponent = isFakeComponent;
		script = isScript;
		//title.text=GameObject.FindWithTag("Controller").GetComponent<Controller>().ScriptsName[index];
		//title.text = comp.GetType().ToString();
		if(!fakeComponent){
			title.text = (comp.GetType().ToString().Substring(0,12) == "UnityEngine." ? comp.GetType().ToString().Remove(0,12) : comp.GetType().ToString());
		}
		else{
			title.text = ((FakeComponent)comp).ComponentName;
		}
		component = comp;

		/*
		HiddenProperties a = new HiddenProperties();
		a.name = "UnityEngine.Transform";
		a.properties = new string[]{
			"localPosition",
			"eulerAngles",
			"localEulerAngles",
			"right", "up", "forward",
			"localRotation",
			"parent",
			"hasChanged",
			"hierarchyCapacity"
		};
		HP.Add(a);
		HiddenProperties b = new HiddenProperties();
		b.name = "UnityEngine.MeshRenderer";
		b.properties = new string[]{
			"additionalVertexStreams",
			"lightmapTilingOffset",
			"forceRenderingOff",
			"motionVectors",
			"renderingLayerMask",
			"rendererPriority",
			"rayTracingMode",
			"sortingLayerName",
			"sortingLayerId",
			"sortingOrder",
			"lightmapIndex",
			"realtimeLightmapIndex",
			"lightmapScaleOffset",
			"realtimeLightmapScaleOffset",
			"material"
		};
		HP.Add(b);
		HiddenProperties c = new HiddenProperties();
		c.name = "UnityEngine.BoxCollider";
		c.properties = new string[]{
			"contactOffset",
			"extents"
		};
		HP.Add(c);
		HiddenProperties d = new HiddenProperties();
		d.name = "UnityEngine.Rigidbody";
		d.properties = new string[]{
			"velocity",
			"angularVelocity",
			"maxDepenetrationVelocity",
			"centerOfMass",
			"inertiaTensorRotation",
			"inertiaTensor",
			"detectCollisions",
			"position",
			"rotation",
			"solverIterations",
			"sleepThreshold",
			"maxAngularVelocity",
			"solverVelocityIterations",
			"sleepVelocity",
			"sleepAngularVelocity",
			"useConeFriction",
			"solverIterationCount",
			"solverVelocityIterationCount"
		};
		HP.Add(d);
		HiddenProperties e = new HiddenProperties();
		e.name = "UnityEngine.Light";
		e.properties = new string[]{
			"shape",
			"innerSpotAngle",
			"colorTemperature",
			"useColorTemperature",
			"useBoundingSphereOverride",
			"boundingSphereOverride",
			"shadowCustomResolution",
			"useShadowMatrixOverride",
			"shadowMatrixOverride",
			"renderingLayerMask",
			"lightShadowCasterMode",
			"shadowAngle",
			"shadowSoftness",
			"shadowSoftnessFade",
			"lightShadowCullDistances",
			"bakedIndex",
			"pixelLightCount",
			"shadowConstantBias",
			"shadowObjectSizeBias",
			"attenuate",
			"lightmapBakeType",
			"lightmappingMode",
			"alreadyLightmapped",
			"layerShadowCullDistances"
		};
		HP.Add(e);

		//Type myObjectType = comp.GetType();
		FieldInfo[] fields = comp.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
		//Debug.Log(fields.Length);
        foreach (FieldInfo thisVar in fields)
        {
			//GameObject clone;
			//clone = Instantiate(GameObject.FindWithTag("Controller").GetComponent<Controller>().ComponentVarElement,VarList);
			//clone.GetComponent<ComponentVarElement>().set(index,obj,gameObject,thisVar,null,comp);
			//VarList.GetComponent<RectTransform>().sizeDelta = new Vector2(244.44f,(1*-90.2f)-90.2f);
			//clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(244.44f,(1*90.2f)+45.1f);
		}
		Type myObjectType = comp.GetType();
		PropertyInfo[] props = comp.GetType().GetProperties();
		if(fakeComponent && !isScript){
			myObjectType = Type.GetType(((FakeComponent)comp).ComponentType);
			props = myObjectType.GetProperties();
		}
		if(isScript){
			myObjectType = ((FakeComponent)comp).loadedScriptAsset.loadedType;
			props = myObjectType.GetProperties();
		}
        foreach (var thisVar in props)
        {
        	bool shouldGetHidden = false;
        	foreach(var i in HP){
        		if(i.name == (!fakeComponent ? comp.GetType().ToString() : myObjectType.ToString())){
        			foreach(var j in i.properties){
        				if(j == thisVar.Name){
        					shouldGetHidden = true;
        				}
        			}
        		}
        	}
        	if(thisVar.CanWrite && thisVar.Name!="enabled" && thisVar.Name!="name" && thisVar.Name!="tag" && thisVar.Name!="hideFlags" && (thisVar.Name.Length<6 || thisVar.Name.Substring(0,6)!="shared") && !shouldGetHidden){
        		GameObject clone;
				clone = Instantiate(GameObject.FindWithTag("Controller").GetComponent<Controller>().ComponentVarElement,VarList);
				clone.GetComponent<ComponentVarElement>().set(index, obj, gameObject, null, thisVar, comp, fakeComponent);
        	}

        	if(thisVar.Name == "enabled"){
        		hasEnabledProperty = true;
        		doRefresh = true;
        	}
        }
        */

        if(!fakeComponent)
        	hasEnabledProperty = comp.GetType().GetProperty("enabled") != null;
        else
        	hasEnabledProperty = ((FakeComponent)comp).loadedScriptAsset.loadedType.GetProperty("enabled") != null;

        EnabledToggle.interactable = hasEnabledProperty;
        if(!hasEnabledProperty){
        	EnabledToggle.GetComponent<MaterialUI.CheckboxConfig>().DisableCheckbox();
        	EnabledToggle.GetComponent<MaterialUI.CheckboxConfig>().TurnOnSilent();
        }
        if(doRefresh && !refreshStarted){
			refreshStarted = true;
			StartCoroutine(UpdateDataLoop());
		}
		UpdateData();
	}

	public void Open(){
		Debug.Log("Open");
		CompVarList.SetActive(true);
		Scrollbar.SetActive(true);
		OpenButton.gameObject.SetActive(false);
		BackButton.gameObject.SetActive(true);
		//gameObject.GetComponent<LayoutElement>().preferredHeight = 730.4f;
		transform.parent = controller.componentEditor.transform;
		controller.componentsPanel.SetActive(false);
		controller.componentEditor.SetActive(true);
		GetComponent<Image>().color = new Color(1, 1, 1, 0.156f);

		var comp = component;
		var isScript = script;
		var obj = objct;

		HiddenProperties a = new HiddenProperties();
		a.name = "UnityEngine.Transform";
		a.properties = new string[]{
			"localPosition",
			"eulerAngles",
			"localEulerAngles",
			"right", "up", "forward",
			"localRotation",
			"parent",
			"hasChanged",
			"hierarchyCapacity"
		};
		HP.Add(a);
		HiddenProperties b = new HiddenProperties();
		b.name = "UnityEngine.MeshRenderer";
		b.properties = new string[]{
			"additionalVertexStreams",
			"lightmapTilingOffset",
			"forceRenderingOff",
			"motionVectors",
			"renderingLayerMask",
			"rendererPriority",
			"rayTracingMode",
			"sortingLayerName",
			"sortingLayerId",
			"sortingOrder",
			"lightmapIndex",
			"realtimeLightmapIndex",
			"lightmapScaleOffset",
			"realtimeLightmapScaleOffset",
			"material"
		};
		HP.Add(b);
		HiddenProperties c = new HiddenProperties();
		c.name = "UnityEngine.BoxCollider";
		c.properties = new string[]{
			"contactOffset",
			"extents"
		};
		HP.Add(c);
		HiddenProperties d = new HiddenProperties();
		d.name = "UnityEngine.Rigidbody";
		d.properties = new string[]{
			"velocity",
			"angularVelocity",
			"maxDepenetrationVelocity",
			"centerOfMass",
			"inertiaTensorRotation",
			"inertiaTensor",
			"detectCollisions",
			"position",
			"rotation",
			"solverIterations",
			"sleepThreshold",
			"maxAngularVelocity",
			"solverVelocityIterations",
			"sleepVelocity",
			"sleepAngularVelocity",
			"useConeFriction",
			"solverIterationCount",
			"solverVelocityIterationCount"
		};
		HP.Add(d);
		HiddenProperties e = new HiddenProperties();
		e.name = "UnityEngine.Light";
		e.properties = new string[]{
			"shape",
			"innerSpotAngle",
			"colorTemperature",
			"useColorTemperature",
			"useBoundingSphereOverride",
			"boundingSphereOverride",
			"shadowCustomResolution",
			"useShadowMatrixOverride",
			"shadowMatrixOverride",
			"renderingLayerMask",
			"lightShadowCasterMode",
			"shadowAngle",
			"shadowSoftness",
			"shadowSoftnessFade",
			"lightShadowCullDistances",
			"bakedIndex",
			"pixelLightCount",
			"shadowConstantBias",
			"shadowObjectSizeBias",
			"attenuate",
			"lightmapBakeType",
			"lightmappingMode",
			"alreadyLightmapped",
			"layerShadowCullDistances"
		};
		HP.Add(e);

		//Type myObjectType = comp.GetType();
		FieldInfo[] fields = comp.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
		//Debug.Log(fields.Length);
        foreach (FieldInfo thisVar in fields)
        {
			//GameObject clone;
			//clone = Instantiate(GameObject.FindWithTag("Controller").GetComponent<Controller>().ComponentVarElement,VarList);
			//clone.GetComponent<ComponentVarElement>().set(index,obj,gameObject,thisVar,null,comp);
			//VarList.GetComponent<RectTransform>().sizeDelta = new Vector2(244.44f,(1*-90.2f)-90.2f);
			//clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(244.44f,(1*90.2f)+45.1f);
		}
		Type myObjectType = comp.GetType();
		PropertyInfo[] props = comp.GetType().GetProperties();
		if(fakeComponent && !isScript){
			myObjectType = Type.GetType(((FakeComponent)comp).ComponentType);
			props = myObjectType.GetProperties();
		}
		if(isScript){
			var values = ((FakeComponent)comp).fakeValues;
			List<string> keys = new List<string>();
			foreach(string key in ((FakeComponent)comp).fakeValues.Keys){
				keys.Add(key);
			}
			for(int i=0; i<keys.Count; i++){
				var fakeScriptValue = values[keys[i]];
				GameObject clone = Instantiate(GameObject.FindWithTag("Controller").GetComponent<Controller>().ComponentVarElement, VarList);
				clone.GetComponent<ComponentVarElement>().isFakeScriptValue = true;
				clone.GetComponent<ComponentVarElement>().fakeScriptValueName = keys[i];
				clone.GetComponent<ComponentVarElement>().varType = ((FakeScriptValue)fakeScriptValue).type;
				clone.GetComponent<ComponentVarElement>().set(index, obj, gameObject, null, null, comp, fakeComponent);
			}
		}
		else{
	        foreach (var thisVar in props)
	        {
	        	bool shouldGetHidden = false;
	        	foreach(var i in HP){
	        		if(i.name == (!fakeComponent ? comp.GetType().ToString() : myObjectType.ToString())){
	        			foreach(var j in i.properties){
	        				if(j == thisVar.Name){
	        					shouldGetHidden = true;
	        				}
	        			}
	        		}
	        	}
	        	if(thisVar.CanWrite && thisVar.Name != "enabled" && thisVar.Name != "name" && thisVar.Name != "tag" && thisVar.Name != "hideFlags" && (thisVar.Name.Length < 6 || thisVar.Name.Substring(0,6) != "shared") && !shouldGetHidden){
	        		GameObject clone = Instantiate(GameObject.FindWithTag("Controller").GetComponent<Controller>().ComponentVarElement, VarList);
					clone.GetComponent<ComponentVarElement>().set(index, obj, gameObject, null, thisVar, comp, fakeComponent);
	        	}

	        	if(thisVar.Name == "enabled"){
	        		hasEnabledProperty = true;
	        		doRefresh = true;
	        	}
	        }
    	}
	}

	public void Close(){
		controller.CloseComponentEditor();
		controller.LoadComponentList(objct);
	}

	public void remove(){
		if(component.GetType() != typeof(UnityEngine.Transform)){
			var objects = controller.selectedTransforms;
        	foreach(Transform child in objects){
        		if(child.GetComponent(component.GetType()) != null){
	        		try{
						Destroy(child.GetComponent(component.GetType()));
					}
					catch{}
				}
			}
			Destroy(gameObject);
		}
		else{
			controller.showAlert("You can't remove this component", "Error");
		}
		controller.CloseComponentEditor();
	}

	public void EnabledToggleChanged(){
		if(controller != null){
			var objects = controller.selectedTransforms;
			foreach(Transform child in objects){
				component.GetType().GetProperty("enabled").SetValue(child.GetComponent(component.GetType()), EnabledToggle.isOn, null);
			}
		}
	}
	
	void OnEnable(){
		if(doRefresh){
			refreshStarted = true;
			StartCoroutine(UpdateDataLoop());
		}
	}

	void UpdateData(){
		if(component != null && hasEnabledProperty){
			EnabledToggle.isOn = (bool)(component.GetType().GetProperty("enabled").GetValue(component, null));
			if((controller == null ? GameObject.FindWithTag("Controller").GetComponent<Controller>() : controller).multipleSelection){
				var isEqualInAllTheObjects = true;
				var objects = (controller == null ? GameObject.FindWithTag("Controller").GetComponent<Controller>() : controller).selectedTransforms;
				var loadedVar = (bool)(component.GetType().GetProperty("enabled").GetValue(component, null));
				foreach(Transform child in objects){
					var loadedVar2 = (bool)(component.GetType().GetProperty("enabled").GetValue(child.GetComponent(component.GetType()), null));
					if(loadedVar.ToString() != loadedVar2.ToString())
						isEqualInAllTheObjects = false;
				}
				if(!isEqualInAllTheObjects){
					EnabledToggle.GetComponent<CheckboxConfig>().middleImage.enabled = true;
					EnabledToggle.GetComponent<CheckboxConfig>().checkImage.enabled = false;
					EnabledToggle.GetComponent<CheckboxConfig>().frameImage.enabled = false;
				}
			}
		}
	}

	IEnumerator UpdateDataLoop(){
		UpdateData();
		if(doRefresh){
			yield return new WaitForSeconds(0.3f);
			StartCoroutine(UpdateDataLoop());
		}
	}

	public void CloseMenu(){
		Debug.Log("Close menu");
		//CloseMenuButton.onClick.Invoke();
	}

	public void OpenMenu(){
		Debug.Log("Open menu");
		OpenMenuButton.onClick.Invoke();
	}

	public void OpenMenuButtonClicked(){
		animStartTime = Time.realtimeSinceStartup;
		if(menu.activeSelf){
			menuButtonAnim.Menu();
			anim1 = false;
			anim2 = true;
		}
		else{
			menuButtonAnim.Arrow();
			anim1 = true;
			anim2 = false;
			menu.SetActive(true);
		}
	}

	public IEnumerator CheckOffscreen(){
		yield return new WaitForSeconds(1f);
		if(transform.position.y<0 || transform.position.y>Screen.height){
			CloseMenu();
		}
		StartCoroutine(CheckOffscreen());
	}

}
}
