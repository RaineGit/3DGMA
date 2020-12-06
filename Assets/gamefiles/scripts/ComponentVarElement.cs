using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CSharpCompiler;
using System.Reflection;
using System;
using UnityEditor;
using Lean.Gui;
using ThreeDGMA;
using System.Linq;
using MaterialUI;

namespace RuntimeGizmos
{
public class ComponentVarElement : MonoBehaviour {

	public int index;
	public Text title;
	public GameObject objct;
	GameObject objct2;
	public Component component;
	Controller controller;
	bool fakeComponent = false;
	public bool isField = false;
	public bool isListElement = false;
	public bool isFakeScriptValue = false;
	public string fakeScriptValueName = "";
	public bool isArrayElement = false;
	public int thisArrayIndex = 0;
	public Type varType;
	public System.Reflection.PropertyInfo CompVar;
	bool doRefresh = false;
	bool refreshStarted = false;
	bool Vector3SliderSelected = false;
	bool NumberSliderSelected = false;
	public GameObject BoolUI;
	public GameObject Vector3UI;
	public GameObject NumberUI;
	public GameObject StringUI;
	public GameObject TypeUI;
	public GameObject FileUI;
	public GameObject ArrayUI;
	public GameObject ColorUI;
	public Toggle BoolToggle;
	public GameObject BoolToggleTab;
	public GameObject BoolTogglePanel;
	public InputField Vector3XInput;
	public InputField Vector3YInput;
	public InputField Vector3ZInput;
	public Slider Vector3XSlider;
	public Slider Vector3YSlider;
	public Slider Vector3ZSlider;
	public InputField NumberInput;
	public Slider NumberSlider;
	public InputField StringInput;
	public Dropdown TypeDropdown;
	public Text TypeDropdownLabel;
	public Text EditArrayButtonText;
	public Button EditArrayButton;
	public Text arrayWarning;
	public Text FileSelectedText;
	string lastType = "";
	bool isTypeSelector = false;
	bool isFirstCheck = true;

	// Use this for initialization
	void Start () {
		controller = GameObject.FindWithTag("Controller").GetComponent<Controller>();
	}
	
	// Update is called once per frame
	void Update () {
		if(varType == typeof(Vector3) || varType == typeof(Quaternion)){
			if(!Vector3SliderSelected){
				Vector3XSlider.value = 0;
				Vector3YSlider.value = 0;
				Vector3ZSlider.value = 0;
			}
			else{
				if(varType == typeof(Vector3)){
					Vector3 old = ((Vector3)GetVar());
					SetVar(new Vector3(old.x+Vector3XSlider.value, old.y+Vector3YSlider.value, old.z+Vector3ZSlider.value));
					if(controller.multipleSelection){
						var objects = controller.selectedTransforms;
						foreach(Transform child in objects){
							if(!GameObject.ReferenceEquals(child.gameObject, objct)){
								Vector3 old2 = ((Vector3)GetVar(child.GetComponent(component.GetType())));
								SetVar(new Vector3(old2.x+Vector3XSlider.value, old2.y+Vector3YSlider.value, old2.z+Vector3ZSlider.value), child.GetComponent(component.GetType()));
							}
						}
					}
					//Vector3XInput.text = (old.x+Vector3XSlider.value).ToString();
					//Vector3YInput.text = (old.y+Vector3YSlider.value).ToString();
					//Vector3ZInput.text = (old.z+Vector3ZSlider.value).ToString();
				}
				else{
					Vector3 old = ((Quaternion)GetVar()).eulerAngles;
					Quaternion old2 = ((Quaternion)GetVar());
					old2.eulerAngles = new Vector3(old.x+Vector3XSlider.value*5, old.y+Vector3YSlider.value*5, old.z+Vector3ZSlider.value*5);
					SetVar(old2);
					if(controller.multipleSelection){
						var objects = controller.selectedTransforms;
						foreach(Transform child in objects){
							if(!GameObject.ReferenceEquals(child.gameObject, objct)){
								Vector3 old3 = ((Quaternion)GetVar(child.GetComponent(component.GetType()))).eulerAngles;
								Quaternion old4 = ((Quaternion)GetVar(child.GetComponent(component.GetType())));
								old4.eulerAngles = new Vector3(old3.x+Vector3XSlider.value*5, old3.y+Vector3YSlider.value*5, old3.z+Vector3ZSlider.value*5);
								SetVar(old4, child.GetComponent(component.GetType()));
							}
						}
					}
					//Vector3XInput.text = (old.x+Vector3XSlider.value).ToString();
					//Vector3YInput.text = (old.y+Vector3YSlider.value).ToString();
					//Vector3ZInput.text = (old.z+Vector3ZSlider.value).ToString();
				}
			}
		}
		if(varType == typeof(int) || varType == typeof(float) || varType == typeof(double)){
			if(!NumberSliderSelected){
				NumberSlider.value = 0;
			}
			else{
				if(varType == typeof(int)){
					if(controller.multipleSelection){
						var objects = controller.selectedTransforms;
						foreach(Transform child in objects){
							int old = ((int)GetVar(child.GetComponent(component.GetType())));
							SetVar(old+(int)NumberSlider.value, child.GetComponent(component.GetType()));
						}
					}
					else{
						int old = ((int)GetVar());
						SetVar(old+(int)NumberSlider.value);
					}
				}
				if(varType == typeof(float)){
					if(controller.multipleSelection){
						var objects = controller.selectedTransforms;
						foreach(Transform child in objects){
							float old = ((float)GetVar(child.GetComponent(component.GetType())));
							SetVar(old+(float)NumberSlider.value, child.GetComponent(component.GetType()));
						}
					}
					else{
						float old = ((float)GetVar());
						SetVar(old+(float)NumberSlider.value);
					}
				}
				if(varType == typeof(double)){
					if(controller.multipleSelection){
						var objects = controller.selectedTransforms;
						foreach(Transform child in objects){
							double old = ((double)GetVar(child.GetComponent(component.GetType())));
							SetVar(old+(double)NumberSlider.value, child.GetComponent(component.GetType()));
						}
					}
					else{
						double old = ((double)GetVar());
						SetVar(old+(double)NumberSlider.value);
					}
				}
			}
		}
	}

	public void set(int localindex, GameObject obj, GameObject obj2, System.Reflection.FieldInfo comp2, System.Reflection.PropertyInfo comp3, Component comp4, bool isFakeComponent=false, bool isArrayElement2=false, int arrayIndex=0){
		controller = GameObject.FindWithTag("Controller").GetComponent<Controller>();
		objct = obj;
		objct2 = obj2;
		index = localindex;
		fakeComponent = isFakeComponent;
		thisArrayIndex = arrayIndex;
		isArrayElement = isArrayElement2;
		component = comp4;
		CompVar = comp3;
		if(!isFakeScriptValue){
			if(!isArrayElement2){
				varType = comp3.PropertyType; 
			}
			else{
				varType = controller.arrayType;
			}
		}
		else{
			isField = ((FakeScriptValue)((FakeComponent)component).fakeValues[fakeScriptValueName]).isField;
		}
		//title.text=GameObject.FindWithTag("Controller").GetComponent<Controller>().ScriptsName[index];
		var isFI = false;
		if(isFI){
			title.text = comp2.Name;
		}
		else{
			string CompVarName = "";
			if(!isArrayElement2){
				if(!isFakeScriptValue){
					for(var i=0;i<comp3.Name.Length;i++){
						if(i==0){
							CompVarName += Char.ToUpper(comp3.Name[i]);
						}
						else{
							if(Char.IsLetter(comp3.Name[i-1]) && !Char.IsUpper(comp3.Name[i-1]) && Char.IsLetter(comp3.Name[i]) && Char.IsUpper(comp3.Name[i])){
								CompVarName += " "+Char.ToUpper(comp3.Name[i]);
							}
							else{
								CompVarName += comp3.Name[i];
							}
						}
					}
				}
				else{
					CompVarName = fakeScriptValueName;
				}
			}
			else{
				CompVarName = "Element " + arrayIndex.ToString();
			}
			title.text = CompVarName;

			if(!isArrayElement2 && comp4.GetType().ToString()=="UnityEngine.Light" && CompVarName=="Shadow Radius") title.text = "Baked Shadow Radius";

			bool definedType = false;
			gameObject.GetComponent<LayoutElement>().preferredHeight = 86.4f;
			if(varType == typeof(bool)){
				gameObject.GetComponent<LayoutElement>().preferredHeight = 156.5f;
				BoolUI.SetActive(true);
				//BoolToggle.isOn = (bool)GetVar();
				doRefresh = true;
				definedType = true;
			}
			if(varType == typeof(Vector3)){
				gameObject.GetComponent<LayoutElement>().preferredHeight = 507.84f;
				Vector3UI.SetActive(true);
				Vector3XInput.text = ((Vector3)GetVar()).x.ToString();
				Vector3YInput.text = ((Vector3)GetVar()).y.ToString();
				Vector3ZInput.text = ((Vector3)GetVar()).z.ToString();
				doRefresh = true;
				definedType = true;
			}
			if(varType == typeof(Quaternion)){
				gameObject.GetComponent<LayoutElement>().preferredHeight = 507.84f;
				Vector3UI.SetActive(true);
				Vector3XInput.text = ((Quaternion)GetVar()).eulerAngles.x.ToString();
				Vector3YInput.text = ((Quaternion)GetVar()).eulerAngles.y.ToString();
				Vector3ZInput.text = ((Quaternion)GetVar()).eulerAngles.z.ToString();
				doRefresh = true;
				definedType = true;
			}
			if(varType == typeof(int) || varType == typeof(float) || varType == typeof(double)){
				gameObject.GetComponent<LayoutElement>().preferredHeight = 242.87f;
				NumberUI.SetActive(true);
				//NumberInput.text = GetVar().ToString();
				doRefresh = true;
				if(varType == typeof(int)){
					NumberInput.contentType = InputField.ContentType.IntegerNumber;
					NumberSlider.wholeNumbers = true;
					NumberSlider.minValue = -2;
					NumberSlider.maxValue = 2;
				}
				else{
					NumberInput.contentType = InputField.ContentType.DecimalNumber;
					NumberSlider.wholeNumbers = false;
					NumberSlider.minValue = -0.2f;
					NumberSlider.maxValue = 0.2f;
				}
				definedType = true;
			}
			if(varType == typeof(string)){
				gameObject.GetComponent<LayoutElement>().preferredHeight = 195.81f;
				StringUI.SetActive(true);
				//StringInput.text = GetVar().ToString();
				doRefresh = true;
				definedType = true;
			}
			if(varType == typeof(Material)){
				gameObject.GetComponent<LayoutElement>().preferredHeight = 228.3f;
				FileUI.SetActive(true);
				doRefresh = true;
				definedType = true;
				FileSelectedText.text = "Empty";
				UpdateFileSelectedText();
			}
			if(varType.IsArray){
				gameObject.GetComponent<LayoutElement>().preferredHeight = 162.8f;
				ArrayUI.SetActive(true);
				//doRefresh = true;
				definedType = true;
				if(controller.multipleSelection){
					EditArrayButton.gameObject.SetActive(false);
					arrayWarning.gameObject.SetActive(true);
				}
				else{
					if(varType.GetElementType().IsArray || (varType.GetElementType().IsGenericType && varType.GetElementType().GetGenericTypeDefinition() == typeof(List<>))){
						EditArrayButton.gameObject.SetActive(false);
					}
				}
			}
			if(varType.IsGenericType && varType.GetGenericTypeDefinition() == typeof(List<>)) { 
				gameObject.GetComponent<LayoutElement>().preferredHeight = 162.8f;
				ArrayUI.SetActive(true);
				EditArrayButtonText.text = "Edit list";
				//doRefresh = true;
				definedType = true;
				if(varType.GetGenericArguments()[0].IsArray || (varType.GetGenericArguments()[0].IsGenericType && varType.GetGenericArguments()[0].GetGenericTypeDefinition() == typeof(List<>))){
					EditArrayButton.gameObject.SetActive(false);
				}
			}
			if(varType == typeof(Color)){
				gameObject.GetComponent<LayoutElement>().preferredHeight = 162.8f;
				ColorUI.SetActive(true);
				//doRefresh = true;
				definedType = true;
			}
			if(!definedType){
				if(varType.IsEnum){
					FieldInfo[] fields = varType.GetFields(/*BindingFlags.NonPublic | */BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
					TypeDropdown.options.Clear();
					for(int i=(fields[0].Name=="value__" ? 1 : 0);i<fields.Length;i++){
						if(fields[i].FieldType==varType){
							TypeDropdown.options.Add(new Dropdown.OptionData(fields[i].Name));
						}
					}
					component = comp4;
					CompVar = comp3;
					isTypeSelector = true;
					UpdateTypeDropdownSelection();
					gameObject.GetComponent<LayoutElement>().preferredHeight = 162.8f;
					TypeUI.SetActive(true);
					doRefresh = true;
					definedType = true;
				}
			}
			if(doRefresh && !refreshStarted){
				refreshStarted = true;
				StartCoroutine(UpdateDataLoop());
			}
		}
		//Debug.Log();
	}

	public void remove(){
		for(int i=0;i<objct.GetComponent<ObjectController>().Scripts.Count;i++){
			if(objct.GetComponent<ObjectController>().Scripts[i]==GameObject.FindWithTag("Controller").GetComponent<Controller>().ScriptsName[index]){
				objct.GetComponent<ObjectController>().Scripts.Remove(objct.GetComponent<ObjectController>().Scripts[i]);
			}
		}
		objct2.GetComponent<ListObjectElement>().open();
	}

	public void BoolToggleChanged(bool isOn){
		if(!controller.multipleSelection)
			SetVar(isOn);
		else{
			if(!BoolToggle.GetComponent<CheckboxConfig>().middleImage.enabled)
				SetVarOfAllComponents(isOn);
			else{
				SetVarOfAllComponents(false);
				foreach(Transform child in transform.parent){
					if(child.GetComponent<ComponentVarElement>().isTypeSelector){
						child.GetComponent<ComponentVarElement>().lastType = "--";
					}
				}
			}
		}
	}

	public void Vector3Changed(){
		if(true && component!=null && !String.IsNullOrEmpty(Vector3XInput.text) && !String.IsNullOrEmpty(Vector3YInput.text) && !String.IsNullOrEmpty(Vector3ZInput.text) && (Vector3XInput.isFocused || Vector3YInput.isFocused || Vector3ZInput.isFocused)){
			if(varType == typeof(Vector3)){
				if(!controller.multipleSelection){
					var loadedVar = GetVar();
					SetVar(new Vector3(Vector3XInput.text != "--" && Vector3XInput.text != "-" ? float.Parse(Vector3XInput.text) : ((Vector3)loadedVar).x, Vector3YInput.text != "--" && Vector3YInput.text != "-" ? float.Parse(Vector3YInput.text) : ((Vector3)loadedVar).y, Vector3ZInput.text != "--" && Vector3ZInput.text != "-" ? float.Parse(Vector3ZInput.text) : ((Vector3)loadedVar).z));
				}
				else{
					var objects = controller.selectedTransforms;
					foreach(Transform child in objects){
						var loadedVar = GetVar(child.GetComponent(component.GetType()));
						SetVar(new Vector3(Vector3XInput.text != "--" && Vector3XInput.text != "-" ? float.Parse(Vector3XInput.text) : ((Vector3)loadedVar).x, Vector3YInput.text != "--" && Vector3YInput.text != "-" ? float.Parse(Vector3YInput.text) : ((Vector3)loadedVar).y, Vector3ZInput.text != "--" && Vector3ZInput.text != "-" ? float.Parse(Vector3ZInput.text) : ((Vector3)loadedVar).z), child.GetComponent(component.GetType()));
					}
				}
			}
			else{
				if(!controller.multipleSelection){
					var loadedVar = GetVar();
					Quaternion old = (Quaternion)loadedVar;
					old.eulerAngles = new Vector3(Vector3XInput.text != "--" && Vector3XInput.text != "-" ? float.Parse(Vector3XInput.text) : ((Vector3)loadedVar).x, Vector3YInput.text != "--" && Vector3YInput.text != "-" ? float.Parse(Vector3YInput.text) : ((Vector3)loadedVar).y, Vector3ZInput.text != "--" && Vector3ZInput.text != "-" ? float.Parse(Vector3ZInput.text) : ((Vector3)loadedVar).z);
					SetVar(old);
				}
				else{
					var objects = controller.selectedTransforms;
					foreach(Transform child in objects){
						var loadedVar = GetVar(child.GetComponent(component.GetType()));
						Quaternion old = (Quaternion)loadedVar;
						old.eulerAngles = new Vector3(Vector3XInput.text != "--" && Vector3XInput.text != "-" ? float.Parse(Vector3XInput.text) : ((Vector3)loadedVar).x, Vector3YInput.text != "--" && Vector3YInput.text != "-" ? float.Parse(Vector3YInput.text) : ((Vector3)loadedVar).y, Vector3ZInput.text != "--" && Vector3ZInput.text != "-" ? float.Parse(Vector3ZInput.text) : ((Vector3)loadedVar).z);
						SetVar(old, child.GetComponent(component.GetType()));
					}
				}
			}
		}
	}

	public void Vector3SliderSelectedEvent(){
		Vector3SliderSelected = true;
		UpdateData();
	}

	public void Vector3SliderDeselectedEvent(){
		Vector3SliderSelected = false;
		UpdateData();
	}

	public void Vector3SliderChanged(){

	}

	public void NumberSliderSelectedEvent(){
		NumberSliderSelected = true;
	}

	public void NumberSliderDeselectedEvent(){
		NumberSliderSelected = false;
	}

	public void NumberInputChanged(){
		if(NumberInput.isFocused){
			if(component!=null && !String.IsNullOrEmpty(NumberInput.text) && NumberInput.text != "--" && NumberInput.text != "-"){
				if(varType == typeof(int)){
					if(controller.multipleSelection)
						SetVarOfAllComponents(int.Parse(NumberInput.text));
					else
						SetVar(int.Parse(NumberInput.text));
				}
				if(varType == typeof(float)){
					if(controller.multipleSelection)
						SetVarOfAllComponents(float.Parse(NumberInput.text));
					else
						SetVar(float.Parse(NumberInput.text));
				}
				if(varType == typeof(double)){
					if(controller.multipleSelection)
						SetVarOfAllComponents(double.Parse(NumberInput.text));
					else
						SetVar(double.Parse(NumberInput.text));
				}
			}
		}
	}

	public void StringInputChanged(){
		if(component!=null && StringInput.text != "--"){
			if(!controller.multipleSelection)
				SetVar(StringInput.text);
			else
				SetVarOfAllComponents(StringInput.text);
		}
	}

	public void StringInputShowTextEditor(){
		GameObject.FindWithTag("Controller").GetComponent<Controller>().TextEditorEditingComponent = component;
		GameObject.FindWithTag("Controller").GetComponent<Controller>().TextEditorEditingCompVar = CompVar;
		GameObject.FindWithTag("Controller").GetComponent<Controller>().TextEditorInput.text = GetVar().ToString();
		GameObject.FindWithTag("Controller").GetComponent<Controller>().texteditorui.SetActive(true);
	}

	public void TypeDropdownChanged(){
		if(TypeDropdown.options[TypeDropdown.value].text != "--"){
			FieldInfo[] fields = varType.GetFields(/*BindingFlags.NonPublic | */BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
			List<object> list = new List<object>();
			for(int i=(fields[0].Name=="value__" ? 1 : 0);i<fields.Length;i++){
				if(fields[i].FieldType==varType){
					list.Add(fields[i].GetValue(GetVar()));
				}
			}
			SetVarOfAllComponents(Convert.ChangeType(list[TypeDropdown.value - (TypeDropdown.options[0].text == "--" ? 1 : 0)], varType));
			if(TypeDropdown.options[0].text == "--")
				TypeDropdown.options.RemoveAt(0);
		}
	}

	public void UpdateTypeDropdownSelection(){
		if(TypeDropdown.options[0].text == "--")
			TypeDropdown.options.RemoveAt(0);

		FieldInfo[] fields = varType.GetFields(/*BindingFlags.NonPublic | */BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
		List<object> list = new List<object>();
		for(int i=(fields[0].Name=="value__" ? 1 : 0);i<fields.Length;i++){
			if(fields[i].FieldType==varType){
				list.Add(fields[i].GetValue(GetVar()));
			}
		}
		var loadedVar = GetVar();
		if(controller.multipleSelection){
			var isEqualInAllTheObjects = true;
			var objects = controller.selectedTransforms;
			foreach(Transform child in objects){
				var loadedVar2 = GetVar(child.GetComponent(component.GetType()));
				if(loadedVar.ToString() != loadedVar2.ToString())
					isEqualInAllTheObjects = false;
			}
			if(!isEqualInAllTheObjects){
				if(TypeDropdown.options[0] == null || TypeDropdown.options[0].text != "--")
					TypeDropdown.options.Insert(0, new Dropdown.OptionData("--"));
				TypeDropdown.value = 0;
				TypeDropdownLabel.text = "--";
			}
			else
				for(int i=0;i<list.Count;i++){
					if(loadedVar.ToString() == Convert.ChangeType(list[i], varType).ToString()){
						TypeDropdown.value = i;
						lastType = loadedVar.ToString();
						TypeDropdownLabel.text = loadedVar.ToString();
					}
				}
		}
		else{
			for(int i=0;i<list.Count;i++){
				if(loadedVar.ToString() == Convert.ChangeType(list[i], varType).ToString()){
					TypeDropdown.value = i;
					lastType = loadedVar.ToString();
					TypeDropdownLabel.text = loadedVar.ToString();
				}
			}
		}
	}

	public void OpenFileChooser(){
		controller.OpenFileChooser();
		controller.selecting = true;
		controller.selectingFor = gameObject.GetComponent<ComponentVarElement>();
		controller.selectingReason = "Set material in compvar";
	}

	public void CloseFileChooser(){
		controller.selectorui.SetActive(false);
		controller.selecting = false;
	}

	void OnEnable(){
		if(doRefresh){
			refreshStarted = true;
			StartCoroutine(UpdateDataLoop());
		}
	}

	IEnumerator UpdateDataLoop(){
		UpdateData();
		if(doRefresh){
			yield return new WaitForSeconds(Vector3SliderSelected || NumberSliderSelected ? 0.1f : 0.3f);
			StartCoroutine(UpdateDataLoop());
		}
	}

	void UpdateData(){
		if(component!=null){
			var loadedVar = GetVar();
			if(varType == typeof(Vector3)){
				if(!Vector3XInput.isFocused && !Vector3YInput.isFocused && !Vector3ZInput.isFocused){
					Vector3XInput.text = ((Vector3)loadedVar).x.ToString();
					Vector3YInput.text = ((Vector3)loadedVar).y.ToString();
					Vector3ZInput.text = ((Vector3)loadedVar).z.ToString();
					if(controller.multipleSelection){
						var isXEqualInAllTheObjects = true;
						var isYEqualInAllTheObjects = true;
						var isZEqualInAllTheObjects = true;
						var objects = controller.selectedTransforms;
						foreach(Transform child in objects){
							var loadedVar2 = GetVar(child.GetComponent(component.GetType()));
							if(((Vector3)loadedVar).x.ToString() != ((Vector3)loadedVar2).x.ToString())
								isXEqualInAllTheObjects = false;
							if(((Vector3)loadedVar).y.ToString() != ((Vector3)loadedVar2).y.ToString())
								isYEqualInAllTheObjects = false;
							if(((Vector3)loadedVar).z.ToString() != ((Vector3)loadedVar2).z.ToString())
								isZEqualInAllTheObjects = false;
						}
						if(!isXEqualInAllTheObjects)
							Vector3XInput.text = "--";
						if(!isYEqualInAllTheObjects)
							Vector3YInput.text = "--";
						if(!isZEqualInAllTheObjects)
							Vector3ZInput.text = "--";
					}
				}
			}
			if(varType == typeof(Quaternion)){
				if(!Vector3XInput.isFocused && !Vector3YInput.isFocused && !Vector3ZInput.isFocused){
					Vector3XInput.text = ((Quaternion)loadedVar).eulerAngles.x.ToString();
					Vector3YInput.text = ((Quaternion)loadedVar).eulerAngles.y.ToString();
					Vector3ZInput.text = ((Quaternion)loadedVar).eulerAngles.z.ToString();
					if(controller.multipleSelection){
						var isXEqualInAllTheObjects = true;
						var isYEqualInAllTheObjects = true;
						var isZEqualInAllTheObjects = true;
						var objects = controller.selectedTransforms;
						foreach(Transform child in objects){
							var loadedVar2 = GetVar(child.GetComponent(component.GetType()));
							if(((Quaternion)loadedVar).eulerAngles.x.ToString() != ((Quaternion)loadedVar2).eulerAngles.x.ToString())
								isXEqualInAllTheObjects = false;
							if(((Quaternion)loadedVar).eulerAngles.y.ToString() != ((Quaternion)loadedVar2).eulerAngles.y.ToString())
								isYEqualInAllTheObjects = false;
							if(((Quaternion)loadedVar).eulerAngles.z.ToString() != ((Quaternion)loadedVar2).eulerAngles.z.ToString())
								isZEqualInAllTheObjects = false;
						}
						if(!isXEqualInAllTheObjects)
							Vector3XInput.text = "--";
						if(!isYEqualInAllTheObjects)
							Vector3YInput.text = "--";
						if(!isZEqualInAllTheObjects)
							Vector3ZInput.text = "--";
					}
				}
			}
			if(varType == typeof(bool)){
				if(controller.multipleSelection){
					var isEqualInAllTheObjects = true;
					var objects = controller.selectedTransforms;
					foreach(Transform child in objects){
						var loadedVar2 = GetVar(child.GetComponent(component.GetType()));
						if(loadedVar.ToString() != loadedVar2.ToString())
							isEqualInAllTheObjects = false;
					}
					if(!isEqualInAllTheObjects){
						BoolToggle.GetComponent<CheckboxConfig>().middleImage.enabled = true;
						BoolToggle.GetComponent<CheckboxConfig>().frameImage.enabled = false;
						BoolToggle.GetComponent<CheckboxConfig>().checkImage.enabled = false;
					}
					else{
						BoolToggle.isOn = (bool)GetVar();
					}
				}
				else{
					BoolToggle.isOn = (bool)GetVar();
				}
			}
			if(varType == typeof(int) || varType == typeof(float) || varType == typeof(double)){
				if(NumberInput.text != "" || isFirstCheck){
					if(controller.multipleSelection){
						var isEqualInAllTheObjects = true;
						var objects = controller.selectedTransforms;
						foreach(Transform child in objects){
							var loadedVar2 = GetVar(child.GetComponent(component.GetType()));
							if(loadedVar.ToString() != loadedVar2.ToString())
								isEqualInAllTheObjects = false;
						}
						if(!isEqualInAllTheObjects)
							NumberInput.text = "-";
						else
							NumberInput.text = GetVar().ToString();
					}
					else{
						NumberInput.text = GetVar().ToString();
					}
				}
			}
			if(varType == typeof(string)){
				if(controller.multipleSelection){
					var isEqualInAllTheObjects = true;
					var objects = controller.selectedTransforms;
					foreach(Transform child in objects){
						var loadedVar2 = GetVar(child.GetComponent(component.GetType()));
						if(loadedVar.ToString() != loadedVar2.ToString())
							isEqualInAllTheObjects = false;
					}
					if(!isEqualInAllTheObjects)
						StringInput.text = "--";
					else
						StringInput.text = GetVar().ToString();
				}
				else{
					StringInput.text = GetVar().ToString();
				}
			}
			if(isTypeSelector && lastType != GetVar().ToString()){
				UpdateTypeDropdownSelection();
			}
			if(varType == typeof(Material)){
				//FileSelectedText.text = "Selected File: " + ((Material)GetVar()).name;
			}
		}
		isFirstCheck = false;
	}

	public void SetVar(object newValue, Component customComponent = null){
		if(!isArrayElement){
			if(!fakeComponent){
				CompVar.SetValue((customComponent == null ? component : customComponent), newValue, null);
			}
			else{
				if(!isFakeScriptValue){
					Type type = Type.GetType(((FakeComponent)(customComponent == null ? component : customComponent)).ComponentType);
					if(((FakeComponent)(customComponent == null ? component : customComponent)).ComponentType2 == "C# Script"){
						type = ((FakeComponent)(customComponent == null ? component : customComponent)).loadedScriptAsset.loadedType;
					}
					PropertyInfo prop = type.GetProperty(CompVar.Name);
					prop.SetValue(((FakeComponent)(customComponent == null ? component : customComponent)).fakeValue, newValue, null);
				}
				else{
					try{
						((FakeScriptValue)((FakeComponent)(customComponent == null ? component : customComponent)).fakeValues[fakeScriptValueName]).val = newValue;
					}
					catch{}
				}
			}
		}
		else{
			if(isFakeScriptValue){
				((Array)((FakeScriptValue)((FakeComponent)(customComponent == null ? component : customComponent)).fakeValues[fakeScriptValueName]).val).SetValue(newValue, thisArrayIndex); 
			}
			else{
				controller.arrayEditing[thisArrayIndex] = newValue;
				controller.UpdateArrayEditor();
			}
		}
	}

	public void SetVarOfAllComponents(object newValue){
		var objects = controller.selectedTransforms;
		foreach(Transform child in objects){
			SetVar(newValue, child.GetComponent(component.GetType()));
		}
	}

	public object GetVar(Component customComponent = null){
		if(!isArrayElement){
			if(!fakeComponent){
				return CompVar.GetValue((customComponent == null ? component : customComponent), null);
			}
			else{
				if(!isFakeScriptValue){
					Type type = Type.GetType(((FakeComponent)(customComponent == null ? component : customComponent)).ComponentType);
					if(((FakeComponent)(customComponent == null ? component : customComponent)).ComponentType2 == "C# Script"){
						type = ((FakeComponent)(customComponent == null ? component : customComponent)).loadedScriptAsset.loadedType;
					}
					PropertyInfo prop = type.GetProperty(CompVar.Name);
					return prop.GetValue(((FakeComponent)(customComponent == null ? component : customComponent)).fakeValue, null);
				}
				else{
					try{
						return ((FakeScriptValue)((FakeComponent)(customComponent == null ? component : customComponent)).fakeValues[fakeScriptValueName]).val;
					}
					catch{}
				}
			}
		}
		else{
			if(isFakeScriptValue){
				if(!isListElement){
					object result;
					result = ((Array)((FakeScriptValue)((FakeComponent)(customComponent == null ? component : customComponent)).fakeValues[fakeScriptValueName]).val).GetValue(thisArrayIndex);
					if(result == null){
						if(varType != typeof(string)){
							((Array)((FakeScriptValue)((FakeComponent)(customComponent == null ? component : customComponent)).fakeValues[fakeScriptValueName]).val).SetValue(Activator.CreateInstance(varType), thisArrayIndex);
							result = Activator.CreateInstance(varType);
						}
						else{
							((Array)((FakeScriptValue)((FakeComponent)(customComponent == null ? component : customComponent)).fakeValues[fakeScriptValueName]).val).SetValue(System.String.Empty, thisArrayIndex);
							result = System.String.Empty;
						}
					}
					return result;
				}
				else
					return ((IEnumerable)((FakeScriptValue)((FakeComponent)(customComponent == null ? component : customComponent)).fakeValues[fakeScriptValueName]).val).Cast<object>().ToList()[thisArrayIndex];
			}
			else{
				return controller.arrayEditing[thisArrayIndex];
			}
		}
		return null;
	}

	public void OpenArrayEditor(){
		controller.arrayType = varType.GetElementType();
		if(varType.IsGenericType && varType.GetGenericTypeDefinition() == typeof(List<>)){
			controller.arrayEditing = ((IEnumerable)GetVar()).Cast<object>().ToArray();
			Debug.Log(controller.arrayEditing.Length);
		}
		else{
			controller.arrayEditing = ((Array)GetVar()).Cast<object>().ToArray();
		}
		controller.arrayEditingElement = gameObject.GetComponent<ComponentVarElement>();
		controller.arrayEditorArrayName.text = title.text;
		controller.OpenArrayEditor();
		controller.UpdateArrayEditor();
	}

	public void OpenColorEditor(){
		controller.colorEditing = gameObject.GetComponent<ComponentVarElement>();
		controller.OpenColorEditor();
	}

	public void UpdateFileSelectedText(){
		var objects = controller.selectedTransforms;
		bool isEqualInAllTheObjects = true;
		string sample = null;
		foreach(Transform child in objects){
			int thisHashCode = (isArrayElement ? controller.arrayEditingElement.component.GetHashCode() : child.GetComponent(component.GetType()).GetHashCode());
			ComponentVarElement mainCompVar = (isArrayElement ? controller.arrayEditingElement : gameObject.GetComponent<ComponentVarElement>());
			ObjectController thisObjectController = (child.gameObject != null ? child.gameObject : controller.arrayEditingElement.objct).GetComponent<ObjectController>();
			Debug.Log(thisObjectController);
			ComponentRef_3DGMA thisController = (ComponentRef_3DGMA)thisObjectController.components[thisHashCode];
			if(thisController != null){
				bool found = false;
				int index = -1;
				for(int j=0; j<thisController.props.Count; j++){
					if(thisController.props[j].name == mainCompVar.CompVar.Name){
						found = true;
						index = j;
					}
				}
				if(found){
					if(!isArrayElement){
						if(sample == null)
							sample = thisController.props[index].path;
						if(sample != thisController.props[index].path)
							isEqualInAllTheObjects = false;
					}
					else{
						if(sample == null)
							sample = thisController.props[index].arrayPaths[thisArrayIndex];
						if(sample != thisController.props[index].arrayPaths[thisArrayIndex])
							isEqualInAllTheObjects = false;
					}
				}
				else{
					if(sample == null)
						sample = "null";
					if(sample != "null")
						isEqualInAllTheObjects = false;
				}
			}
		}
		if(isEqualInAllTheObjects)
			FileSelectedText.text = "Selected File:\n" + (sample == null ? "null" : sample);
		else
			FileSelectedText.text = "Selected File:\n--";
		if(FileSelectedText.text=="Empty" || FileSelectedText.text=="Selected File:\n"){
			FileSelectedText.text = "Selected File:\nnull";
		}
	}

}
}
