using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CSharpCompiler;
using System;
using System.IO;
using ThreeDGMA;
using UnityEngine.EventSystems;
using MaterialUI;

namespace RuntimeGizmos
{
public class ListFileElement : MonoBehaviour, IPointerClickHandler {

	public int index;
	public Text title;
	public Text desc;
	public GameObject objct;
	GameObject scriptui;
	GameObject objectui;
	public GameObject enterbtn;
	public GameObject editbtn;
	public GameObject selectbtn;
	public GameObject openmenubtn;
	public GameObject closemenubtn;
	public CheckboxConfig checkbox;
	public Toggle toggle;
	public GameObject FolderIcon;
	public GameObject FileIcon;
	public GameObject MaterialIcon;
	public GameObject ModelIcon;
	public GameObject CSharpIcon;
	public bool isFolder = false;
	public bool selecting = false;
	public string PathToThisFile = "/";
	public string FileExtension = "";
	public Controller controller;

	// Use this for initialization
	void Start () {
		controller = GameObject.FindWithTag("Controller").GetComponent<Controller>();
		scriptui = controller.scriptui;
		objectui = controller.objectui;
	}
	
	// Update is called once per frame
	void Update () {
		//selecting = controller.selecting;
	}

	public void set(int localindex, string text, string path, bool isFolder2, bool selecting2=false){
		//objct = obj;
		index=localindex;
		title.text=text;
		PathToThisFile = path;
		isFolder = isFolder2;
		FileExtension = text.Split(Convert.ToChar("."))[text.Split(Convert.ToChar(".")).Length-1].ToLower();
		if(!isFolder2){
			desc.text = text.Split(Convert.ToChar("."))[text.Split(Convert.ToChar(".")).Length-1] + " file";
			FileIcon.SetActive(true);
			enterbtn.SetActive(false);
			if(FileExtension=="ma"){
				desc.text="Material";
				FileIcon.SetActive(false);
				MaterialIcon.SetActive(true);
			}
			if(FileExtension == "fbx" || FileExtension == "obj" || FileExtension == "blend" || FileExtension == "stl" || FileExtension == "dae"){
				desc.text="3D Model";
				FileIcon.SetActive(false);
				ModelIcon.SetActive(true);
			}
			if(FileExtension=="cs"){
				desc.text="C# Script";
				FileIcon.SetActive(false);
				CSharpIcon.SetActive(true);
			}
		}
		if(isFolder2){
			enterbtn.SetActive(true);
			editbtn.SetActive(false);
			FolderIcon.SetActive(true);
		}
		else{
			if(selecting2){
				selecting = true;
				enterbtn.SetActive(false);
				editbtn.SetActive(false);
				selectbtn.SetActive(true);
			}
			else{
				enterbtn.SetActive(false);
				editbtn.SetActive(true);
			}
		}
	}

	public void enter(){
		if(!selecting){
			controller.loadFiles2(PathToThisFile);
		}
		else{
			controller.loadFiles2(PathToThisFile);
		}
	}

	public void edit(){
		if(FileExtension == "ma"){
			StreamReader sr = new StreamReader(controller.ProjectDirectory+"/Assets/"+PathToThisFile);
			Mat_3DGMA Mat = JsonUtility.FromJson<Mat_3DGMA>(sr.ReadToEnd());
			controller.fileEditingNow = PathToThisFile;
			controller.editMaterialColorPicker.GetComponent<FlexibleColorPicker>().startingColor = Mat.MainColor;
			controller.editorui.SetActive(false);
			controller.editmaterialui.SetActive(true);
			controller.editMaterialColorPicker.GetComponent<FlexibleColorPicker>().color = Mat.MainColor;
			controller.editMaterialMetallicSlider.value = Mat.Metallic;
			controller.editMaterialSmoothnessSlider.value = Mat.Smoothness;
			if(Mat.MainTexId != null)
				controller.tempMaterialTexId = Mat.MainTexId;
			sr.Close();
		}
		else if(FileExtension == "fbx" || FileExtension == "obj" || FileExtension == "blend" || FileExtension == "stl" || FileExtension == "dae"){
			controller.LoadModel(controller.ProjectDirectory+"/Assets/"+PathToThisFile);
		}
		else if(FileExtension == "sc"){
			StartCoroutine(controller.LoadScene(PathToThisFile));
		}
		else{
			Application.OpenURL(controller.ProjectDirectory+"/Assets/"+PathToThisFile);		
		}
	}

	public void setMaterial(){
		controller.currObject.GetComponent<ListObjectElement>().objct.GetComponent<ObjectController>().material = objct;
		controller.editorui.SetActive(true);
		controller.selectorui.SetActive(false);
		controller.reloadMaterial(objct);
	}

	public void open(){
		
	}

	public void setSelecting(){
		if(!isFolder){
			selecting = true;
			enterbtn.SetActive(false);
			editbtn.SetActive(false);
			selectbtn.SetActive(true);
		}
		else{
			enterbtn.SetActive(true);
			editbtn.SetActive(false);
		}
		openmenubtn.SetActive(false);
		closemenubtn.SetActive(false);
		checkbox.transform.parent.gameObject.SetActive(false);
		title.transform.parent.GetComponent<RectTransform>().offsetMax = new Vector2(0, title.transform.parent.GetComponent<RectTransform>().offsetMax.y);
	}

	public void OpenMenu(){
		//gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, 431.17f);
		gameObject.GetComponent<LayoutElement>().preferredHeight = 315.41f;
	}

	public void CloseMenu(){
		//gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, 90.2f);
		gameObject.GetComponent<LayoutElement>().preferredHeight = 90f;
	}

	public void ChooseFile2()
	{
		StartCoroutine(ChooseFile());
	}

	public IEnumerator ChooseFile(){
		if(controller.selectingReason == "Set material in compvar"){
			int thisHashCode = -1;
			Component component = (controller.selectingFor.component != null ? controller.selectingFor.component : controller.arrayEditingElement.component);
			ComponentVarElement mainCompVar = (controller.selectingFor.objct != null ? controller.selectingFor : controller.arrayEditingElement);
			var objects = controller.selectedTransforms;
			foreach(Transform child in objects){
				ObjectController thisObjectController = child.GetComponent<ObjectController>();
				thisHashCode = child.GetComponent(component.GetType()).GetHashCode();
				ComponentRef_3DGMA thisComponent = (ComponentRef_3DGMA)controller.components[thisHashCode];
				if(thisComponent != null){
					bool found = false;
					int index = -1;
					ComponentProp_3DGMA newPropInfo = new ComponentProp_3DGMA();
					newPropInfo.name = mainCompVar.CompVar.Name;
					for(int j=0; j<thisComponent.props.Count; j++){
						if(thisComponent.props[j].name == mainCompVar.CompVar.Name){
							found = true;
							newPropInfo = thisComponent.props[j];
							index = j;
						}
					}
					if(!controller.selectingFor.isArrayElement){
						newPropInfo.path = PathToThisFile;
					}
					else{
						newPropInfo.isArray = true;
						Array.Resize<string>( ref newPropInfo.arrayPaths, controller.arrayEditing.Length );
						newPropInfo.arrayPaths[controller.selectingFor.thisArrayIndex] = PathToThisFile;
					}
					if(found){
						thisComponent.props[index] = newPropInfo;
					}
					else{
						thisComponent.props.Add(newPropInfo);
					}
					thisObjectController.components[thisHashCode] = thisComponent;
				}
			}
		}

		if(controller.selectingReason == "Set material in compvar"){
			CoroutineWithData cd = new CoroutineWithData(this, controller.loadAsset(PathToThisFile));
			yield return cd.coroutine;
			controller.selectingFor.SetVarOfAllComponents(((LoadedAssetFile)(cd.result)).loadedMaterial);
			controller.selectingFor.UpdateFileSelectedText();
		}

		if(controller.selectingReason == "Set material texture"){
			controller.tempMaterialTexId = controller.GetMetadata(new FileInfo(controller.ProjectDirectory+"/Assets/"+PathToThisFile)).id;
		}

		if(controller.selectingReason == "Add Script Component"){
			StartCoroutine(AddScript());
		}

		if(controller.selectingReason != "Add Script Component")
			controller.CloseFileChooser();
		controller.StartCoroutine(controller.ScrollDownComponentsList());
	}

	public void OnPointerClick(PointerEventData eventData){
		if(checkbox.transform.parent.gameObject.activeSelf){
			Debug.Log("Click");
			toggle.isOn = !toggle.isOn;
			checkbox.ToggleCheckbox();
		}
	}

	public void ToggleToggled(bool isOn){
		if(isOn){
			//GetComponent<Mask>().showMaskGraphic = true;
			GetComponent<Image>().color = new Color(0, 0.3f, 1, 0.15f);
		}
		else{
			//GetComponent<Mask>().showMaskGraphic = false;
			GetComponent<Image>().color = new Color(0, 0, 0, 0);
		}
	}

	public IEnumerator AddScript(){
		controller.SetJobText("Compiling scripts...");
		yield return null;
		CoroutineWithData cd = new CoroutineWithData(this, controller.loadAsset(PathToThisFile, true));
		yield return cd.coroutine;
		LoadedAssetFile asset = (LoadedAssetFile)cd.result;
		var objects = controller.selectedTransforms;
        foreach(Transform child in objects){
			FakeComponent fakeComponent = child.gameObject.AddComponent<FakeComponent>();
			fakeComponent.scriptPath = PathToThisFile;
			fakeComponent.ComponentType2 = "C# Script";
			fakeComponent.ComponentName = asset.loadedType.Name;
			fakeComponent.loadedScriptAsset = asset;
			fakeComponent.CreateFakeComponent();
		}
		controller.LoadComponentList(controller.selectedObject);
		controller.ClearJobText();
		controller.StartCoroutine(controller.ScrollDownComponentsList());
		controller.CloseFileChooser();
	}	

}
}