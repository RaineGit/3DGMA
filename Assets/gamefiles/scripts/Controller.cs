using RuntimeGizmos;
using Microsoft.CSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;
using System.IO;
using Examples;
using TMPro;
using UnityEngine.Android;
using Lean.Gui;
using ThreeDGMA;
using TriLib;
using cakeslice;
using UnityEngine.Events;
using MaterialUI;
using Newtonsoft.Json;

namespace ThreeDGMA
{
	public class Project_3DGMA
	{
		public string path;
		public DateTime lastTimeOpened;
 	}

	public class ProjectInfo_3DGMA
	{
		public string lastTimeOpened;
		public string type = "3d_game";
 	}

	public class Log_3DGMA
	{
		public string logText = "";
		public string stackTrace = "";
		public int repeated = 0;
		public LogType type;
		public LogElement element;
	}

	public class ComponentProp_3DGMA
	{
		public string name = "";
		public string path = "/sdcard/3DGMA/Projects/TestProject/aaaawsgajgvn/thisfiledoesnotexist9012571325913560317856075.txt";
		public bool isArray = false;
		public string[] arrayPaths = new string[1];
	}

	public class ComponentRef_3DGMA
	{
		public int hashCode = 0;
		public Component component;
		public List<ComponentProp_3DGMA> props = new List<ComponentProp_3DGMA>();
	}

	public class LoadedAssetFile
	{
		public string path = "/sdcard/3DGMA/Projects/TestProject/aaaawsgajgvn/thisfiledoesnotexist9012571325913560317856075.txt";
		public string type = "";
		public FileMetadata_3DGMA meta;
		public Material loadedMaterial;
		public Type loadedType;
		public Texture loadedTexture;
	}

	public class Mat_3DGMA
	{
    	public string MainTexId;
    	public Color MainColor = new Color(1, 1, 1, 1);
    	public bool isEmissionEnabled = false;
    	public Color EmissionColor = new Color(0, 0, 0, 0);
    	public float Metallic = 0;
    	public float Smoothness = 0;
	}

	public class TypeHolder_3DGMA
	{
		public Type type;
	}

	public class FileMetadata_3DGMA
	{
		public string version = "";
		public string id = "";
	}

	public class FileMetadata_3DGMA2
	{
		public string path = "/sdcard/3DGMA/Projects/TestProject/aaaawsgajgvn/thisfiledoesnotexist9012571325913560317856075.txt";
		public FileMetadata_3DGMA meta;
	}

	[Serializable]
	public class SceneFileGameObject_3DGMA
	{
		public string id;
		public string name;
		public List<SceneFileGameObject_3DGMA> children = new List<SceneFileGameObject_3DGMA>();
		public List<SceneFileComponent_3DGMA> components = new List<SceneFileComponent_3DGMA>();
	}

	[Serializable]
	public class SceneFileComponent_3DGMA
	{
		public Type type;
		public List<FakeHashtableElement_3DGMA> props = new List<FakeHashtableElement_3DGMA>();
		public List<FakeHashtableElement_3DGMA> fields = new List<FakeHashtableElement_3DGMA>();
		public bool isFakeComponent;
		public string fakeComponentName = "";
		public string fakeComponentName2 = "";
		public bool isScript;
		public string scriptId = "";
	}

	[Serializable]
	public class FakeHashtableElement_3DGMA
	{
		public string name;
		public Type type;
		public object value;
		public bool custom = false;
	}

}

namespace CSharpCompiler
{ 

public class Controller : MonoBehaviour {

	public string AppDirectory = "/sdcard/3DGMA";
	public string ProjectDirectory = "/sdcard/3DGMA/Projects/TestProject";
	public double lastAssetsFolderChange = 0;
	public bool busyCheckingFiles = false;
	public bool busy = false;
	public bool checkAgain = false;
	public bool checkAssetsFolderChanges = false;
	public bool restartFileImport = false;
	public Hashtable cachedPaths = new Hashtable();
	public Hashtable cachedAccessDates = new Hashtable();
	FileSystemWatcher watcher = new FileSystemWatcher();
	public List<LoadedAssetFile> loadedAssets = new List<LoadedAssetFile>();
	public List<FileMetadata_3DGMA2> loadedMetas = new List<FileMetadata_3DGMA2>();
	public Hashtable components = new Hashtable();
	public double timerStartTime = 0;
	public Camera cam;
	public GameObject scene;
	GameObject originalScene;
	public GameObject fakescene;
	public bool isRunning = false;
	public Text preinput;
	public bool selecting = false;
	public bool multipleSelection = false;
	public UnityEngine.UI.Toggle multipleSelectionCheckbox;
	public ComponentVarElement selectingFor;
	public string selectingReason = "";
	public object[] arrayEditing;
	public Type arrayType;
	public ComponentVarElement arrayEditingElement;
	public ComponentVarElement colorEditing;
	public Material materialTemplate;
	public TMP_InputField input;
	public InputField input2;
	public InputField input3;
	public InputField input4;
	public GameObject ScriptListElement;
	public GameObject ObjectListElement;
	public GameObject ObjectListElementBack;
	public GameObject ObjectSListElement;
	public GameObject FileListElement;
	public GameObject FileListElementBack;
	public GameObject ScriptList;
	public GameObject ObjectSList;
	public GameObject ComponentElement;
	public GameObject ComponentVarElement;
	public GameObject AddComponentElement;
	public GameObject ObjectPropsElement;
	public GameObject LogListElement;
	public GameObject SomeComponentsWereHiddenText;
	public GameObject FileList;
	public GameObject FileList2;
	public Text fileExplorerPathText;
	public Text objectListPathText;
	public GameObject AlertObject;
	public Text AlertText;
	public GameObject newAlert;
	public Text newAlertTitle;
	public Text newAlertText;
	public GameObject newAlertNormalButtons;
	public GameObject newAlertCancelContinueButtons;
	public Button newAlertCancelButton;
	public Button newAlertContinueButton;
	public GameObject scriptui;
	public GameObject objectui;
	public InputField objname;
	public InputField objtag;
	public Text editorobjname;
	public GameObject currObject;
	public GameObject fakeconsole;
	public ScrollRect fakeconsole2;
	public GameObject logViewer;
	public GameObject logConsole;
	public Text logViewerText;
	public Text logViewerType;
	public GameObject logConsoleLast200Text;
	public LeanToggle scrollWithOutputToggle;
    public GameObject compiling;
    public Text compilingText;
	public Camera maincam;
	public GameObject editorui;
	public GameObject runtimeui;
	public GameObject editmaterialui;
	public GameObject selectorui;
	public Canvas joystickui;
	public GameObject texteditorui;
	public GameObject arrayeditorui;
	public GameObject coloreditorui;
	public GameObject storagepermissionui;
	public List<String> Scripts = new List<String>();
	public List<String> ScriptsName = new List<String>();
	public List<String> ScriptsClass = new List<String>();
	public List<String> ScriptsUsing = new List<String>();
	public List<MonoBehaviour> AddedScripts = new List<MonoBehaviour>();
	static List<String> assemblyLocations = new List<String>();
	public List<Log_3DGMA> logs = new List<Log_3DGMA>();
	public int realLogCount = 0;
	public List<String> logs2 = new List<String>();
	int objnum = 0;
	public GameObject objectCube;
	public GameObject objectPlane;
	public GameObject objectCam;
	public GameObject objectPointLight;

	public GameObject objlistcurrent;
	public int objlistcurrent_childcount;
	public string filelistcurrent = "/";

	public GameObject floatingIcon;
	public Sprite lightIcon;
	public Sprite cameraIcon;

	public GameObject file;
	public GameObject EmptyFolder;
	public GameObject EmptyFile;
	public string fileEditingNow;
	public GameObject editMaterialColorPicker;
	public Slider editMaterialMetallicSlider;
	public Slider editMaterialSmoothnessSlider;
	public Color defMaterialColor;

	public GameObject setMaterialButton;

	public GameObject testObject;

	public TMP_InputField TextEditorInput;
	public Component TextEditorEditingComponent;
	public System.Reflection.PropertyInfo TextEditorEditingCompVar;

	public Text NotificationText;
	public GameObject NotificationGO;

	public GameObject AxisThing;

	public Rigidbody emptyRigidbody;
	public GameObject samplesObject;

	public GameObject hideInArrayEditor;
	public Text arrayEditorArrayName;
	public GameObject arrayEditorViewportContent;
	public InputField arraySizeInput;

	public FlexibleColorPicker colorEditorColorPicker;
	public Text jobText;
	public GameObject busyScreen;
	public GameObject selectedObject;

	public GameObject componentEditor;
	public GameObject componentsPanel;

	public List<Transform> selectedTransforms = new List<Transform>();
	public List<string> selectedFiles = new List<string>();
	public Button fileChooserSelectFolderButton;
	public Text fileChooserSelectFolderButtonText;

	public Button addComponentButton;
	public RectTransform addComponentButtonPlus;
	public RectTransform addComponentPanel;
	public GameObject addComponentCancelPanel;
	public GameObject addComponentPanel2;
	public GameObject addComponentPanel3;
	float animStartTime;
	float animDeltaTime;
	bool openAddCompAnim = false;
	bool closeAddCompAnim = false;

	public GameObject renamePanel;
	public InputField renameInput;

	public string tempMaterialTexId;

	Mesh primitiveCube;
	Mesh primitiveSphere;
	Mesh primitivePlane;
	public Material defaultMaterial;

	public List<Transform> panels;
	public Transform topPanelSlot;
	public Transform bottomPanelSlot;
	public Transform hiddenPanelSlot;
	public GameObject openInspectorButton;
	public GameObject bottomPanelDropdown;
	public GameObject bottomPanelFSButton;
	public GameObject topMaximizeButton;
	public GameObject topMinimizeButton;

	public bool inSceneEditingMode = false;

	string url = "";

	void Awake(){
		if(Application.isEditor){
			AppDirectory = "C:/Users/mcleo/Desktop/3DGMA";
			if(!(new DirectoryInfo(AppDirectory).Exists)){
				AppDirectory = "/home/mcleocito/Desktop/3DGMA";
			}
		}
		ProjectDirectory = AppDirectory+"/Projects/TestProject";
		if(PlayerPrefs.HasKey("currentProjectPath")){
			ProjectDirectory = PlayerPrefs.GetString("currentProjectPath");
		}
 		if(!Directory.Exists(AppDirectory)){    
    		Directory.CreateDirectory(AppDirectory);
		}
		if(!Directory.Exists(ProjectDirectory)){    
    		Directory.CreateDirectory(ProjectDirectory);
		}
		if(!Directory.Exists(ProjectDirectory+"/Assets")){    
    		Directory.CreateDirectory(ProjectDirectory+"/Assets");
		}
	}

	void Start(){
		try{
			objlistcurrent = scene;
			originalScene = scene;
			cam = Camera.main;
			input.text = preinput.text;
			foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies()){
				assemblyLocations.Add(assembly.Location);
			}
			var temp = GetAllGameObjects(scene);
			for(int i=0;i<temp.Count;i++){
				if(temp[i].GetComponent<Light>()!=null){
					Debug.Log(temp[i]);
				}
			}
			loadFiles2("/");
			GameObject.Find("/Canvas").transform.GetChild(0).gameObject.SetActive(true);

			#if PLATFORM_ANDROID
	        if(!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead)){
	            AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.READ_EXTERNAL_STORAGE");
	            if(result != AndroidRuntimePermissions.Permission.Granted){
					storagepermissionui.SetActive(true);
	            }		
	        }
	        if(!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite)){
	            AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.WRITE_EXTERNAL_STORAGE");
	            if(result != AndroidRuntimePermissions.Permission.Granted){
					storagepermissionui.SetActive(true);
	            }
	        }
	        #endif

	        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
	        primitiveCube = go.GetComponent<MeshFilter>().sharedMesh;
	        Destroy(go);

	        go = GameObject.CreatePrimitive(PrimitiveType.Plane);
	        primitivePlane = go.GetComponent<MeshFilter>().sharedMesh;
	        Destroy(go);

	        go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
	        primitiveSphere = go.GetComponent<MeshFilter>().sharedMesh;
	        Destroy(go);

	        loadObjListByLast();
	        CheckSprites();
	        StartCoroutine(CheckFilesMetadata());
	        StartCoroutine(CheckIfObjectListNeedsUpdate());
	        FileInfo[] allFiles = GetAllFiles("/");
	    	cachedPaths.Clear();
	    	DateTime dt1970 = new DateTime(1970, 1, 1);
	    	for(int i=0; i<allFiles.Count(); i++){
	    		FileInfo file = allFiles[i];
	    		cachedPaths.Add(file.FullName, true);
	    		DateTime current2 = file.LastAccessTime;
	    		TimeSpan span2 = current2 - dt1970;
	    		cachedAccessDates.Add(file.FullName, span2.TotalMilliseconds);
	    	}
	        DateTime current = DateTime.Now;
	    	TimeSpan span = current - dt1970;
	        lastAssetsFolderChange = span.TotalMilliseconds;
	        
	        watcher.Path = ProjectDirectory+"/Assets/";

	        // Watch for changes in LastAccess and LastWrite times, and
	        // the renaming of files or directories.
	        watcher.NotifyFilter = NotifyFilters.LastAccess
	                             | NotifyFilters.LastWrite
	                             | NotifyFilters.FileName
	                             | NotifyFilters.DirectoryName;

	        // Add event handlers.
	        watcher.Changed += CheckAssetsFolderChanges;
	        watcher.Created += CheckAssetsFolderChanges;
	        watcher.Deleted += CheckAssetsFolderChanges;
	        watcher.Renamed += CheckAssetsFolderChanges;

	        watcher.IncludeSubdirectories = true;

	        // Begin watching.
	        watcher.EnableRaisingEvents = true;

	        setPortrait();
	    }
	    catch(Exception err){
	    	showAlert("An error has occurred while starting 3DGMA, we recommend you to close the app as it couldn't start itself properly and will be really unstable. Please, report this bug in our subreddit r/3DGMA along with an screenshot of this message:\n" + err.Message, "Serious error");
	    }

        //StartCoroutine(LoadSampleNewScene());
	}

	IEnumerator LoadSampleNewScene(){
		yield return null;
		TextAsset txt = (TextAsset)Resources.Load("NewScene", typeof(TextAsset));
		string content = txt.text;
		LoadScene(null, content);
	}

	public void CloseApp(){
		Application.Quit();
	}

	public void ChangePanelTop(int index){
		ChangePanel(index, true);
	}

	public void ChangePanelBottom(int index){
		ChangePanel(index, false);
	}

	public void OpenInspector(){
		PanelExitFullscreen();
		ChangePanel(3, panels[1].parent == bottomPanelSlot);
	}

	public void PanelGoFullscreen(bool isTop){
		if(!isTop){
			int prevPanel = 0;
			for(int i=0; i<panels.Count(); i++){
				if(panels[i].parent == bottomPanelSlot && panels[i].parent != hiddenPanelSlot)
					prevPanel = i;
			}
			ChangePanel(prevPanel, true);
		}
		topPanelSlot.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0f);
		topPanelSlot.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		bottomPanelDropdown.SetActive(false);
		bottomPanelFSButton.SetActive(false);
		bottomPanelSlot.GetComponent<RectTransform>().offsetMin = new Vector2(0f, -900f);
		bottomPanelSlot.GetComponent<RectTransform>().offsetMax = new Vector2(0f, -900f);
		topMaximizeButton.SetActive(false);
		topMinimizeButton.SetActive(true);
	}

	public void PanelExitFullscreen(){
		topPanelSlot.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0.5f);
		topPanelSlot.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		bottomPanelDropdown.SetActive(true);
		bottomPanelFSButton.SetActive(true);
		bottomPanelSlot.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		bottomPanelSlot.GetComponent<RectTransform>().offsetMax = new Vector2(0f, -61.81f);
		topMinimizeButton.SetActive(false);
		topMaximizeButton.SetActive(true);
	}

	public void ChangePanel(int panelIndex, bool isTop = true){
		int prevPanel = 0;
		bool swap = (isTop && panels[panelIndex].parent == bottomPanelSlot || !isTop && panels[panelIndex].parent == topPanelSlot) && panels[panelIndex].parent != hiddenPanelSlot;
		if(swap){
			for(int i=0; i<panels.Count(); i++){
				if(panels[i].parent == (isTop ? topPanelSlot : bottomPanelSlot) && panels[i].parent != hiddenPanelSlot)
					prevPanel = i;
			}
		}
		foreach(var child in panels){
			if((child.parent == topPanelSlot || !isTop) && (child.parent == bottomPanelSlot || isTop)){
				child.SetParent(hiddenPanelSlot, false);
				if(panelIndex == 2 || panelIndex == 4)
					panels[panelIndex].gameObject.SetActive(true);
			}
		}
		panels[panelIndex].SetParent(isTop ? topPanelSlot : bottomPanelSlot, false);
		panels[panelIndex].gameObject.SetActive(true);
		if(panelIndex == 3)
			openInspectorButton.SetActive(false);
		if(swap)
			ChangePanel(prevPanel, !isTop);
	}

	public void SetSceneEditingModeTo(bool newBool){
		inSceneEditingMode = newBool;
	}

	public void ToggleMultipleSelectionCheckbox(){
		ToggleMultipleSelection(multipleSelectionCheckbox.isOn);
	}

	public void ToggleMultipleSelection(bool isOn){
		multipleSelection = isOn;
		if(multipleSelection){
			var selectedObjects = selectedTransforms;
			foreach(Transform child in selectedObjects){
				List<GameObject> children = GetAllGameObjects(child.gameObject);
				children.Add(child.gameObject);
				foreach(GameObject child2 in children){
					if(child2.GetComponent<cakeslice.Outline>() != null){
						child2.GetComponent<cakeslice.Outline>().color = 1;
					}
				}
			}
		}
		else{
			if(selectedTransforms.Count >= 1)
				LoadComponentList(selectedTransforms[0].gameObject);
			else
				LoadComponentList(null);
		}
	}

	public void run(){
		cam.GetComponent<TransformGizmo>().ClearTargets();
		StartCoroutine(runwait());
	}

	public void stop(){
		foreach (Transform child in scene.transform){
			for(int i=0;i<child.gameObject.GetComponent<ObjectController>().LoadedScripts.Count;i++){
				Destroy(child.gameObject.GetComponent<ObjectController>().LoadedScripts[i]);
			}
			for(int i=0;i<child.gameObject.GetComponent<ObjectController>().LoadedScripts.Count;i++){
				child.gameObject.GetComponent<ObjectController>().LoadedScripts.Remove(child.gameObject.GetComponent<ObjectController>().LoadedScripts[i]);
			}
		}
		for(int i=0;i<GameObject.FindGameObjectsWithTag("cam").Length;i++){
			GameObject.FindGameObjectsWithTag("cam")[i].transform.GetChild(0).gameObject.GetComponent<Camera>().enabled=false;
		}
		Destroy(scene);
		scene = originalScene;
		scene.SetActive(true);
		fakescene.SetActive(true);
		maincam.enabled=true;
		isRunning = false;
		SimulationStopped();
	}

	public void loadScripts(){
		foreach (Transform child in ScriptList.transform){
			if(currObject!=child.gameObject){
				Destroy(child.gameObject);
			}
			else{
				child.gameObject.SetActive(false);
			}			
		}
		for(int i=0;i<ScriptsName.Count;i++){
			GameObject clone;
			clone = Instantiate(ScriptListElement,ScriptList.transform);
			clone.GetComponent<ListScriptElement>().set(i);
			//clone.transform.SetParent(ScriptList.transform);
			//ScriptList.GetComponent<RectTransform>().sizeDelta = new Vector2(244.44f,(i*-90.2f)-90.2f);
			//clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(244.44f,(i*90.2f)+45.1f);
		}
	}

	public void loadObjects(){
		loadObjects2(scene);
	}

	public void loadObjects2(GameObject obj){
		foreach (Transform child in ScriptList.transform){
			if(currObject!=child.gameObject){
				Destroy(child.gameObject);
			}
			else{
				child.gameObject.SetActive(false);
			}
		}
		int i=0;

		if(obj!=scene){
			var clone = Instantiate(ObjectListElementBack, ScriptList.transform);
			clone.GetComponent<SimpleObjListControl>().isObjectList = true;
			clone.GetComponent<SimpleObjListControl>().UpdateText();
		}

		foreach (Transform child in obj.transform){
			GameObject clone;
			clone = Instantiate(ObjectListElement,ScriptList.transform);
			clone.GetComponent<ListObjectElement>().set(i,child.gameObject.name,child.gameObject);
			//clone.transform.SetParent(ScriptList.transform);
			//ScriptList.GetComponent<RectTransform>().sizeDelta = new Vector2(244.44f,(i*-90.2f)-90.2f);
			//clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(244.44f,(i*90.2f)+45.1f);
			if(child.GetComponent<ObjectController>() != null)
				child.GetComponent<ObjectController>().element = clone.GetComponent<ListObjectElement>();
			i++;
		}
		objlistcurrent = obj;
		objlistcurrent_childcount = objlistcurrent.transform.childCount;
		GameObject current = objlistcurrent;
		string path = "";
		while(true){
			if(!GameObject.ReferenceEquals(current, originalScene)){
				path = current.name + "/" + path;
				current = current.transform.parent.gameObject;
			}
			else{
				break;
			}
		}
		objectListPathText.text = "Scene/" + path;
	}

	public void loadObjListByScript(GameObject obj){
		loadObjects2(obj.GetComponent<ListObjectElement>().objct);
	}

	public void loadFiles(){
		loadFiles2("/");
	}

	public void loadFiles2(string dir){
		if(dir=="/"||dir=="//")dir="";
		Debug.Log(dir);
		foreach (Transform child in FileList.transform){
			if(currObject!=child.gameObject){
			Destroy(child.gameObject);
			}
			else{
			child.gameObject.SetActive(false);
			}
		}
		foreach (Transform child in FileList2.transform){
			if(currObject!=child.gameObject){
			Destroy(child.gameObject);
			}
			else{
			child.gameObject.SetActive(false);
			}
		}
		int i=0;

		if(dir!=""){
			Instantiate(FileListElementBack,FileList.transform);
			Instantiate(FileListElementBack,FileList2.transform);
		}

		DirectoryInfo dirinfo = new DirectoryInfo(ProjectDirectory+"/Assets"+dir);
 		FileInfo[] info = dirinfo.GetFiles();
 		DirectoryInfo[] info2 = dirinfo.GetDirectories();

 		foreach (DirectoryInfo f in info2){
			GameObject clone;
			clone = Instantiate(FileListElement,FileList.transform);
			FileAttributes attr = File.GetAttributes(ProjectDirectory+"/Assets"+dir+"/"+f.Name);
			clone.GetComponent<ListFileElement>().set(i, f.Name, dir+"/"+f.Name, attr.HasFlag(FileAttributes.Directory));
			GameObject clone2;
			clone2 = Instantiate(clone, FileList2.transform);
			clone2.GetComponent<ListFileElement>().setSelecting();
			if(selectingReason == "Move file" || selectingReason == "Copy file")
				clone2.GetComponent<ListFileElement>().selectbtn.SetActive(false);
			//FileList.GetComponent<RectTransform>().sizeDelta = new Vector2(244.44f,(i*-90.2f)-90.2f);
			//clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(244.44f,(i*90.2f)+45.1f);
			//FileList2.GetComponent<RectTransform>().sizeDelta = new Vector2(244.44f,(i*-90.2f)-90.2f);
			//clone2.GetComponent<RectTransform>().anchoredPosition = new Vector2(244.44f,(i*90.2f)+45.1f);
			i++;
		}
		foreach (FileInfo f in info){
			if(f.Extension.ToLower() != ".meta_3dgma"){
				GameObject clone;
				clone = Instantiate(FileListElement, FileList.transform);
				FileAttributes attr = File.GetAttributes(ProjectDirectory+"/Assets"+dir+"/"+f.Name);
				clone.GetComponent<ListFileElement>().set(i, f.Name, dir+"/"+f.Name, attr.HasFlag(FileAttributes.Directory));
				GameObject clone2;
				clone2 = Instantiate(clone, FileList2.transform);
				clone2.GetComponent<ListFileElement>().setSelecting();
				if(selectingReason == "Move file" || selectingReason == "Copy file")
					clone2.GetComponent<ListFileElement>().selectbtn.SetActive(false);
				//FileList.GetComponent<RectTransform>().sizeDelta = new Vector2(244.44f,(i*-90.2f)-90.2f);
				//clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(244.44f,(i*90.2f)+45.1f);
				//FileList2.GetComponent<RectTransform>().sizeDelta = new Vector2(244.44f,(i*-90.2f)-90.2f);
				//clone2.GetComponent<RectTransform>().anchoredPosition = new Vector2(244.44f,(i*90.2f)+45.1f);
				i++;
				}
		}
		fileExplorerPathText.text = "Assets"+dir+"/";
		filelistcurrent=dir;
	}

	public void loadFiles3(string dir){
		if(dir=="/"||dir=="//")dir="";
		foreach (Transform child in FileList2.transform){
			if(currObject!=child.gameObject){
			Destroy(child.gameObject);
			}
			else{
			child.gameObject.SetActive(false);
			}
		}
		int i=0;

		if(dir != ""){
			GameObject clone;
			clone = Instantiate(FileListElementBack,FileList2.transform);
			clone.GetComponent<SimpleObjListControl>().selecting = true;
		}

		DirectoryInfo dirinfo = new DirectoryInfo(ProjectDirectory+"/Assets"+dir);
 		FileInfo[] info = dirinfo.GetFiles("*.*");

		foreach (FileInfo f in info){
			if(f.Extension.ToLower() != ".meta_3dgma"){
				GameObject clone;
				clone = Instantiate(FileListElement,FileList.transform);
				FileAttributes attr = File.GetAttributes(ProjectDirectory+"/Assets"+dir+"/"+f.Name);
				clone.GetComponent<ListFileElement>().set(i, f.Name, ProjectDirectory+"/Assets"+dir+"/"+f.Name, attr.HasFlag(FileAttributes.Directory));
				//FileList.GetComponent<RectTransform>().sizeDelta = new Vector2(244.44f,(i*-90.2f)-90.2f);
				//clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(244.44f,(i*90.2f)+45.1f);
				i++;
			}
		}
		filelistcurrent=dir;
		loadFiles2(filelistcurrent);
	}

	public void loadFileListByScript(GameObject obj){
		//loadFiles2(obj.GetComponent<ListFileElement>().objct);
	}

	public void createNewFolder(InputField readfrom){
		Directory.CreateDirectory(ProjectDirectory+"/Assets/"+filelistcurrent+"/"+readfrom.text);
		loadFileListByLast();
	}

	public void createNewFileMaterial(InputField readfrom){
		StreamWriter sw = File.CreateText(ProjectDirectory+"/Assets/"+filelistcurrent+"/"+readfrom.text+".ma");
		Mat_3DGMA NewMaterial = new Mat_3DGMA();
		NewMaterial.isEmissionEnabled = false;
		NewMaterial.MainColor = defMaterialColor;
		sw.Write(JsonUtility.ToJson(NewMaterial));
		sw.Close();
		loadFileListByLast();
	}

	public void createNewFileCSharpScript(InputField readfrom){
		string className = readfrom.text.Replace(" ", "").Replace("-", "").Replace(".", "");
		float uselessVariable = 0;
        if(float.TryParse(className.Substring(0,1), out uselessVariable)){
        	className = className.Remove(0,1);
        }

		StreamWriter sw = File.CreateText(ProjectDirectory+"/Assets/"+filelistcurrent+"/"+readfrom.text+".cs");
		sw.Write("using System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\n\npublic class "+className+" : MonoBehaviour\n{\n	// Start is called before the first frame update\n	void Start()\n	{\n		\n	}\n\n	// Update is called once per frame\n	void Update()\n	{\n	\n	}\n}");
		sw.Close();
		loadFileListByLast();
	}

	public void MoveFile(){
		selectedFiles.Clear();
		foreach(Transform child in FileList.transform){
			if(child.GetComponent<ListFileElement>() != null && child.GetComponent<ListFileElement>().toggle.isOn)
				selectedFiles.Add(child.GetComponent<ListFileElement>().PathToThisFile);
		}
		OpenFileChooser(true);
		selecting = true;
		selectingReason = "Move file";
		fileChooserSelectFolderButtonText.text = "Move here";
		fileChooserSelectFolderButton.gameObject.SetActive(true);
		loadFiles2(filelistcurrent);
	}

	public void CopyFile(){
		selectedFiles.Clear();
		foreach(Transform child in FileList.transform){
			if(child.GetComponent<ListFileElement>() != null && child.GetComponent<ListFileElement>().toggle.isOn)
				selectedFiles.Add(child.GetComponent<ListFileElement>().PathToThisFile);
		}
		OpenFileChooser(true);
		selecting = true;
		selectingReason = "Copy file";
		fileChooserSelectFolderButtonText.text = "Copy here";
		fileChooserSelectFolderButton.gameObject.SetActive(true);
		loadFiles2(filelistcurrent);
	}

	public void DeleteFile(){
		selectedFiles.Clear();
		foreach(Transform child in FileList.transform){
			if(child.GetComponent<ListFileElement>() != null && child.GetComponent<ListFileElement>().toggle.isOn)
				selectedFiles.Add(child.GetComponent<ListFileElement>().PathToThisFile);
		}
		selectingReason = "Delete file";
		DeleteConfirmation();
	}

	public void OpenRenameFilePanel(){
		selectedFiles.Clear();
		foreach(Transform child in FileList.transform){
			if(child.GetComponent<ListFileElement>() != null && child.GetComponent<ListFileElement>().toggle.isOn)
				selectedFiles.Add(child.GetComponent<ListFileElement>().PathToThisFile);
		}
		renameInput.text = new FileInfo(selectedFiles[0]).Name;
		renamePanel.SetActive(true);
	}

	public void RenameFile(){
		try{
			if(new FileInfo(ProjectDirectory+"/Assets"+selectedFiles[0]).Exists)
				File.Move(ProjectDirectory+"/Assets"+selectedFiles[0], ProjectDirectory+"/Assets"+filelistcurrent+"/"+renameInput.text);
			else if(new DirectoryInfo(ProjectDirectory+"/Assets"+selectedFiles[0]).Exists)
				Directory.Move(ProjectDirectory+"/Assets"+selectedFiles[0], ProjectDirectory+"/Assets"+filelistcurrent+"/"+renameInput.text);
		}
		catch{
			renamePanel.SetActive(false);
			loadFiles2(filelistcurrent);
			showAlert("An error happened while renaming a file/folder: unknown", "Error");
			throw new Exception("An error happened while renaming a file/folder: unknown");
		}
		renamePanel.SetActive(false);
		loadFiles2(filelistcurrent);
	}

	public void loadFileListByLast(){
		loadFiles2(filelistcurrent);
	}

	public void LoadComponentList(GameObject objct){
		CloseComponentEditor();
		addComponentPanel.GetComponent<AddComponentButton>().objct = objct;
		if(!multipleSelection){
			cam.GetComponent<TransformGizmo>().ClearTargets();
		}
		CloseAddComponentMenu();
		if(objct == null){
			currObject = null;
			objname.text = "";
			objtag.text = "";
			foreach (Transform child in ObjectSList.transform){
				Destroy(child.gameObject);
			}
			addComponentButton.transform.parent.gameObject.SetActive(false);
		}
		else{
			addComponentButton.transform.parent.gameObject.SetActive(true);
			cam.GetComponent<TransformGizmo>().AddTarget(objct.transform);
			string sharedName = null;
			if(multipleSelection){
				List<Transform> objects = selectedTransforms;
				if(objects.Count >= 1){
					string sampleName = objects[0].name;
					bool areAllNamesTheSame = true;
					foreach(Transform child in objects){
						if(sampleName != child.gameObject.name){
							areAllNamesTheSame = false;
						}
					}
					sharedName = areAllNamesTheSame ? sampleName : "--";
				}
			}
			selectedObject = objct;
			foreach (Transform child in ObjectSList.transform){
				Destroy(child.gameObject);
			}
			currObject = gameObject;
			objname.text = (sharedName == null ? objct.name : sharedName);
			if(objct.GetComponent<ObjectController>() == null){
				objct.AddComponent<ObjectController>();
			}
			objtag.text = objct.GetComponent<ObjectController>().id;
			if(objct.GetComponent<MeshRenderer>() == null)
			{
				//setMaterialButton.SetActive(false);
			}
			else{
				//setMaterialButton.SetActive(true);
			}
			scriptui.SetActive(false);
			objectui.SetActive(true);
			componentsPanel.SetActive(true);

			GameObject clone3;
			clone3 = Instantiate(ObjectPropsElement, ObjectSList.transform);
			clone3.GetComponent<ObjectPropsElement>().objct = objct;
			clone3.GetComponent<ObjectPropsElement>().Name.text = objct.name;

			Component[] objs = objct.GetComponents(typeof(Component));
			List<Type> typesThatAreTheSame2 = new List<Type>();

			if(multipleSelection){
				List<Transform> objects = selectedTransforms;
				if(objects.Count >= 1){
					Component[] sampleComps = objects[0].GetComponents(typeof(Component));
					List<Type> typesThatAreTheSame = new List<Type>();
					foreach(Component comp in sampleComps){
						typesThatAreTheSame.Add(comp.GetType());
					}
					foreach(Type type in typesThatAreTheSame){
						bool allOfTheObjectsHaveThisComponent = true;
						foreach(Transform obj in objects){
							if(obj.GetComponent(type) == null){
								allOfTheObjectsHaveThisComponent = false;
							}
						}
						if(allOfTheObjectsHaveThisComponent){
							typesThatAreTheSame2.Add(type);
						}
					}
				}
			}

			bool wasAnyComponentHidden = false;
			for(int i=0; i<objs.Length; i++){
				if(!multipleSelection || typesThatAreTheSame2.Contains(objs[i].GetType())){
					int index=-1;
					if(objs[i].GetType().ToString() == "FakeComponent"){
						GameObject clone;
						clone = Instantiate(ComponentElement, ObjectSList.transform);
						clone.GetComponent<ComponentElement>().set(index, objct, gameObject, objs[i], true, ((FakeComponent)objs[i]).ComponentType2 == "C# Script");
					}
					if(objs[i].GetType().ToString().Length>=12 && objs[i].GetType().ToString().Substring(0,12) == "UnityEngine." && objs[i] != objct.GetComponent<ObjectController>().fakeCollider){
						GameObject clone;
						clone = Instantiate(ComponentElement, ObjectSList.transform);
						clone.GetComponent<ComponentElement>().set(index, objct, gameObject, objs[i]);
						//clone.transform.SetParent(ScriptList.transform);
						//ObjectSList.GetComponent<RectTransform>().sizeDelta = new Vector2(244.44f,(i*-90.2f)-90.2f);
						//clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(244.44f,(i*90.2f)+45.1f);
					}
				}
				else{
					wasAnyComponentHidden = true;
				}
			}

			if(wasAnyComponentHidden){
				Instantiate(SomeComponentsWereHiddenText, ObjectSList.transform);
			}

			GameObject clone2;
			clone2 = Instantiate(AddComponentElement,ObjectSList.transform);
			//clone2.GetComponent<AddComponentButton>().objct = objct;
			//clone2.GetComponent<AddComponentButton>().objct2 = gameObject;

			/*
			GameObject clone4;
			clone4 = Instantiate(AddComponentElement,ObjectSList.transform);
			clone4.GetComponent<AddComponentButton>().objct = objct;
			clone4.GetComponent<AddComponentButton>().objct2 = gameObject;
			clone4.GetComponent<AddComponentButton>().addComponentButton.SetActive(false);
			clone4.GetComponent<AddComponentButton>().addScriptButton.SetActive(true);
			*/
		}
	}

	public IEnumerator ScrollDownComponentsList(){
        yield return null;
        ObjectSList.transform.parent.parent.GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 0);
    }

	public void editMaterialFile(){
		StreamReader sr = new StreamReader(ProjectDirectory+"/Assets/"+fileEditingNow);
		Mat_3DGMA Mat = JsonUtility.FromJson<Mat_3DGMA>(sr.ReadToEnd());
		Mat.MainColor = editMaterialColorPicker.GetComponent<FlexibleColorPicker>().color;
		Mat.Metallic = editMaterialMetallicSlider.value;
		Mat.Smoothness = editMaterialSmoothnessSlider.value;
		Mat.MainTexId = tempMaterialTexId;
		sr.Close();
		StreamWriter sw = File.CreateText(ProjectDirectory+"/Assets/"+fileEditingNow);
		sw.Write(JsonUtility.ToJson(Mat));
		sw.Close();
		for(int i=0; i<loadedAssets.Count; i++){
			if(loadedAssets[i].path == fileEditingNow){
				//loadedAssets[i].loadedMaterial = convertToAsset(loadedAssets[i].path).loadedMaterial;
				loadedAssets[i].loadedMaterial.SetColor("_Color", editMaterialColorPicker.GetComponent<FlexibleColorPicker>().color);
				loadedAssets[i].loadedMaterial.SetFloat("_Metallic", editMaterialMetallicSlider.value);
				loadedAssets[i].loadedMaterial.SetFloat("_Glossiness", editMaterialSmoothnessSlider.value);
			}
		}
		//reloadMaterial(fileEditingNow);
	}

	public void SetMaterialTex(){
		OpenFileChooser();
		selecting = true;
		selectingReason = "Set material texture";
		loadFiles2(filelistcurrent);
	}

	public void LoadModel(string fullPath){
		var assetLoader = new AssetLoader();
		var assetLoaderOptions = AssetLoaderOptions.CreateInstance();
	    var model = assetLoader.LoadFromFile(fullPath, assetLoaderOptions, objlistcurrent);
		var gameObjects = GetAllGameObjects(model);
		gameObjects.Add(model);
		foreach(GameObject child in gameObjects){
			child.AddComponent<ObjectController>();
			if(child.GetComponent<MeshFilter>() != null){
				try{
					var fakeMeshCollider = child.AddComponent<MeshCollider>();
					child.GetComponent<ObjectController>().fakeCollider = fakeMeshCollider;
					fakeMeshCollider.sharedMesh = child.GetComponent<MeshFilter>().mesh;
				}
				catch{}
			}
		}
	}

	public void loadObjListByLast(){
		loadObjects2(objlistcurrent);
	}

	public void reloadMaterial(GameObject mat){
		foreach(GameObject child in GetAllGameObjects(scene)){
			if(child.GetComponent<ObjectController>() != null &&child.GetComponent<ObjectController>().material == mat){
				child.GetComponent<Renderer>().material.SetColor("_Color", mat.GetComponent<GameFileInfo>().material.color);
			}
		}
	}

	public void test(string url2){
		url = url2;
		var a = 0;
		var b = "";
		foreach (string file in System.IO.Directory.GetFiles(Application.persistentDataPath))
 		{
 			if(file.Split("/"[0])[file.Split("/"[0]).Count()-1].Contains("image")&&int.Parse(file.Split("/"[0])[file.Split("/"[0]).Count()-1].Split(new string[] { "image" }, StringSplitOptions.None)[file.Split(new string[] { "image" }, StringSplitOptions.None).Count()-1].Split("."[0])[0])>a){
 				a = int.Parse(file.Split("/"[0])[file.Split("/"[0]).Count()-1].Split(new string[] { "image" }, StringSplitOptions.None)[file.Split(new string[] { "image" }, StringSplitOptions.None).Count()-1].Split("."[0])[0]);
 				b = file;
 			}
 		}
 		Debug.Log(url2 + " => " + Application.persistentDataPath + "/image" + (a+1) + ".png");
 		File.Copy(url2, Application.persistentDataPath + "/image" + (a+1) + ".png");
		url = "file:///"+Application.persistentDataPath + "/image" + (a+1) + ".png";
		StartCoroutine(test2());
	}

	IEnumerator test2()
    {
        Texture2D tex;
        tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
        using (WWW www = new WWW(url))
        {
            yield return www;
            www.LoadImageIntoTexture(tex);
            testObject.GetComponent<Renderer>().material.mainTexture = tex;
        }
    }

	public void blankScript(){
		input.text=preinput.text;
		input2.text="";
		input3.text="";
		objectui.SetActive(false);
		scriptui.SetActive(true);
	}

	public void addScript(){
		bool exists=false;
		int index=0;
		for(int i=0;i<ScriptsName.Count;i++){
			if(ScriptsName[i]==input2.text){
				exists=true;
				index=i;
			}
		}
		if(!exists){
		Scripts.Add(input.text);
		ScriptsName.Add(input2.text);
		ScriptsClass.Add(input3.text);
		ScriptsUsing.Add(input4.text);
		showAlert("Script saved.");
		}
		else{
		Scripts[index]=input.text;
		ScriptsName[index]=input2.text;
		ScriptsClass[index]=input3.text;
		ScriptsUsing[index]=input4.text;
		showAlert("Script overwritten.");
		}
		loadScripts();
	}

	public void addObject(string name){
		GameObject clone = GameObject.FindWithTag("Object1");
		if(name=="cube"){
			clone = Instantiate(objectCube);
			clone.name="Cube"+objnum;
			float[] temp = new float[6];
			temp[0] = 3;
			temp[1] = 1;
			temp[2] = 1;
			clone.GetComponent<ObjectController>().Components[0] = "BoxCollider";
			clone.GetComponent<ObjectController>().Components[1] = temp;
		}
		if(name=="plane"){
			clone = Instantiate(objectPlane);
			clone.name = "Plane" + objnum;
		}
		if(name=="cam"){
			clone = Instantiate(objectCam);
			clone.name = "Camera" + objnum;
		}
		if(name=="pointlight"){
			clone = Instantiate(objectPointLight);
			clone.name = "PointLight" + objnum;
		}
		clone.transform.parent = scene.transform;
		objnum++;
		CheckSprites();
		loadObjListByLast();
	}

	public void add(GameObject obj, string clss, string script, string usng){
		var assembly = Compile(script);
		var runtimeType = assembly.GetType(clss);
        var method = runtimeType.GetMethod("AddYourselfTo");
        var del = (Func<GameObject, MonoBehaviour>)
                      Delegate.CreateDelegate(
                          typeof(Func<GameObject, MonoBehaviour>), 
                          method
                  );

        // We ask the compiled method to add its component to this.gameObject
        var addedComponent = del.Invoke(obj);
        obj.GetComponent<ObjectController>().LoadedScripts.Add(addedComponent);
	}

	public IEnumerator CompileScriptAndGetType(string script){
		SetJobText("Compiling scripts...");
		yield return null;
		var runtimeType = CompileScriptAndGetType2(script);
		ClearJobText();
        yield return runtimeType;
	}

	public Type CompileScriptAndGetType2(string script){
		var assembly = Compile(script);
		var runtimeType = assembly.GetType(script.Split(new string[] { " : MonoBehaviour" }, StringSplitOptions.None)[0].Split(new string[] { "class " }, StringSplitOptions.None).Last());
        return runtimeType;
	}

	public void objectApply(){
		currObject.GetComponent<ListObjectElement>().objct.name=objname.text;
		currObject.GetComponent<ListObjectElement>().objct.GetComponent<ObjectController>().id=objtag.text;
		currObject.GetComponent<ListObjectElement>().open();
		loadObjects();
		showAlert("Object updated.");
	}
	
	// Update is called once per frame
	void Update () {
		Application.RegisterLogCallback(HandleLog);
		if(selectedTransforms.Count >= 1){
			var objects = selectedTransforms;
			editorobjname.text = "Object name" + (objects.Count > 1 ? "s" : "") + ":  ";
			foreach(Transform child in objects){
				editorobjname.text += '\n' + "  " + child.gameObject.name;
			}
		}
		AxisThing.transform.rotation = Quaternion.identity;
		if((checkAssetsFolderChanges || checkAgain) && !busyCheckingFiles && !busy){
			checkAgain = false;
			checkAssetsFolderChanges = false;
			StartCoroutine(CheckAssetsFolderChanges2());
		}

		animDeltaTime = Time.realtimeSinceStartup - animStartTime;

		if(openAddCompAnim)
		{
			if(animDeltaTime <= 0.3f)
			{
				addComponentButtonPlus.eulerAngles = new Vector3(0, 0, Anim.Quint.InOut(0f, -135f, animDeltaTime, 0.3f));
				addComponentPanel.sizeDelta = new Vector2(addComponentPanel.sizeDelta.x, Anim.Quint.InOut(0f, 630.62f, animDeltaTime, 0.3f));
				addComponentCancelPanel.GetComponent<Image>().color = new Color(0, 0, 0, Anim.Quint.InOut(0f, 0.3f, animDeltaTime, 0.3f));
			}
			else{
				addComponentButtonPlus.eulerAngles = new Vector3(0, 0, -135f);
				addComponentPanel.sizeDelta = new Vector2(addComponentPanel.sizeDelta.x, 630.62f);
				addComponentCancelPanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.3f);
				openAddCompAnim = false;
			}
		}

		if(closeAddCompAnim)
		{
			if(animDeltaTime <= 0.3f)
			{
				addComponentButtonPlus.eulerAngles = new Vector3(0, 0, Anim.Quint.InOut(-135f, -270f, animDeltaTime, 0.3f));
				addComponentPanel.sizeDelta = new Vector2(addComponentPanel.sizeDelta.x, Anim.Quint.InOut(630.62f, 0f, animDeltaTime, 0.3f));
				addComponentCancelPanel.GetComponent<Image>().color = new Color(0, 0, 0, Anim.Quint.InOut(0.3f, 0f, animDeltaTime, 0.3f));
			}
			else{
				addComponentButtonPlus.eulerAngles = new Vector3(0, 0, 0);
				addComponentPanel.sizeDelta = new Vector2(addComponentPanel.sizeDelta.x, 0);
				addComponentCancelPanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
				addComponentPanel.gameObject.SetActive(false);
				addComponentCancelPanel.gameObject.SetActive(false);
				closeAddCompAnim = false;
			}
		}
	}

	public void OpenAddComponentMenu(){
		if(addComponentPanel.gameObject.activeSelf && !closeAddCompAnim){
			CloseAddComponentMenu();
		}
		else{
			animStartTime = Time.realtimeSinceStartup;
			addComponentPanel.gameObject.SetActive(true);
			addComponentCancelPanel.gameObject.SetActive(true);
			closeAddCompAnim = false;
			openAddCompAnim = true;
			addComponentPanel2.SetActive(true);
			addComponentPanel3.SetActive(false);
		}
	}

	public void CloseAddComponentMenu(){
		if(addComponentPanel.gameObject.activeSelf && !closeAddCompAnim){
			animStartTime = Time.realtimeSinceStartup;
			closeAddCompAnim = true;
			openAddCompAnim = false;
		}
	}

	void HandleLog (string logString, string stackTrace, LogType type) {
		if(logString == "Error found while compiling scripts, check the console."){
			showAlert("An error happened while compiling scripts, check the console.", "Error");
		}
		else{
			if(logs.Count()>1000){
				logs = new List<Log_3DGMA>();
				//fakeconsole.text = "";
			}
			Log_3DGMA tempLog = new Log_3DGMA();
			tempLog.logText = logString;
			tempLog.stackTrace = stackTrace;
			tempLog.repeated = 1;
			tempLog.type = type;
			AddLog(tempLog);
		    //fakeconsole.text+="-"+logString+"\n";
		    //fakeconsole2.normalizedPosition = new Vector2(0, 0);
		}
	}

	public void setGizmoMode(int num){
		TransformType type = TransformType.Move;
		if(num==0)type=TransformType.Move;
		if(num==1)type=TransformType.Rotate;
		if(num==2)type=TransformType.Scale;
		cam.GetComponent<TransformGizmo>().transformType=type;
	}

	public void resetRotation(){
		cam.GetComponent<TransformGizmo>().lastSelection.rotation = Quaternion.identity;
	}

	public void deleteObject(){
		Destroy(cam.GetComponent<TransformGizmo>().lastSelection.gameObject);
		loadObjListByLast();
	}

	public void camFullscreen(){
		cam.rect = new Rect(0,0,1,1);
	}

	public void camSmallscreen(){
		cam.rect = new Rect(0.6f,0,0.4f,0.4f);
	}

	public void setLandscape(){
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		//StartCoroutine(SetResolutionTo720p());
	}

	public IEnumerator SetResolutionTo720p(){
		yield return new WaitForSeconds(1f);
		//Screen.SetResolution(1280, 720, true);
	}

	public void setPortrait(){
		Screen.orientation = ScreenOrientation.Portrait;
		//Screen.SetResolution(720, 1280, true);
	}

	public void showAlert(string text = "", string title = "", UnityEvent continueButtonEvent = null, UnityEvent cancelButtonEvent = null){
		newAlertNormalButtons.SetActive(true);
		newAlertCancelContinueButtons.SetActive(false);
		newAlertContinueButton.onClick.RemoveAllListeners();
		newAlertCancelButton.onClick.RemoveAllListeners();
		if(continueButtonEvent != null || cancelButtonEvent != null){
			newAlertNormalButtons.SetActive(false);
			newAlertCancelContinueButtons.SetActive(true);
			if(continueButtonEvent != null)
				newAlertContinueButton.onClick.AddListener(() => continueButtonEvent.Invoke());
			if(cancelButtonEvent != null)
				newAlertCancelButton.onClick.AddListener(() => cancelButtonEvent.Invoke());
		}
		newAlertTitle.text = title;
		newAlertText.text = text;
		newAlert.GetComponent<FloatingPanel>().Open();
	}

	public static Assembly Compile(string source)
	{
		var provider = new CSharpCompiler.CodeCompiler();
		var param = new CompilerParameters();

		// Add ALL of the assembly references
		for(int i=0;i<assemblyLocations.Count;i++)
		{
			param.ReferencedAssemblies.Add(assemblyLocations[i]);
		}

		// Add specific assembly references
		//param.ReferencedAssemblies.Add("System.dll");
		//param.ReferencedAssemblies.Add("CSharp.dll");
		//param.ReferencedAssemblies.Add("UnityEngines.dll");

		// Generate a dll in memory
		param.GenerateExecutable = false;
		param.GenerateInMemory = true;

		// Compile the source
		var result = provider.CompileAssemblyFromSource(param, source);

		if (result.Errors.Count > 0) {
			var msg = new StringBuilder();
			foreach (CompilerError error in result.Errors) {
				msg.AppendFormat("Error ({0}): {1}\n",
					error.ErrorNumber, error.ErrorText);
			}
			Debug.Log("Error found while compiling scripts, check the console.");
			throw new Exception(msg.ToString());
		}

		// Return the assembly
		return result.CompiledAssembly;
	}

	public List<GameObject> GetAllGameObjects(GameObject obj){
		List<GameObject> list = new List<GameObject>();
		if(obj.transform.childCount>0){
			foreach(Transform child in obj.transform){
				list.Add(child.gameObject);
				var temp = GetAllGameObjects(child.gameObject);
				for(int i=0;i<temp.Count;i++){
					list.Add(temp[i]);
				}
			}
		}
		return list;
	}

    IEnumerator hideAlert(){
    	yield return new WaitForSeconds(5);
    	AlertObject.SetActive(false);
    }

    IEnumerator runwait(){
    	compilingText.text="Preparing simulation";
    	yield return new WaitForSeconds(0.5f);
    	scene = Instantiate(originalScene);
		fakescene.SetActive(false);

					List<HiddenProperties> HP = new List<HiddenProperties>();
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

		compilingText.text="Loading scene";
		//yield return new WaitForSeconds(0.5f);
    	foreach (var child in GetAllGameObjects(scene)){
    		if(!child.GetComponent<ObjectController>()){
				child.AddComponent<ObjectController>();
			}
			Component[] fakeComponents = child.GetComponents(typeof(FakeComponent));
			for(int j=0; j<fakeComponents.Length; j++){
				FakeComponent comp = (FakeComponent)fakeComponents[j];
				string path = "";
				Transform next = child.transform;
				while(!GameObject.ReferenceEquals(next.gameObject, scene) && next!=null){
					path = "/" + next.name + path;
					next = next.parent;
				}
				GameObject originalGO = GameObject.Find("/Scene"+path);
				Component[] originalFakeComponents = originalGO.GetComponents(typeof(FakeComponent));
				FakeComponent originalComp = (FakeComponent)originalFakeComponents[j];
				if(comp.ComponentType2 == "UnityEngine.Rigidbody" && child.GetComponent<Rigidbody>() == null){
					Rigidbody newComponent = child.AddComponent<Rigidbody>();
					foreach(var i in typeof(Rigidbody).GetProperties()){
						bool shouldGetHidden = false;
        				foreach(var k in HP){
        					if(k.name == comp.ComponentType2){
        						foreach(var l in k.properties){
        							if(l == i.Name){
        								shouldGetHidden = true;
        							}
        						}
        					}
        				}
						if(i.CanWrite && i.Name!="enabled" && i.Name!="name" && i.Name!="tag" && i.Name!="hideFlags" && (i.Name.Length<6 || i.Name.Substring(0,6)!="shared") && !shouldGetHidden && i.GetValue(newComponent, null)!=null && i.GetValue((Rigidbody)originalComp.fakeValue, null)!=null){
							i.SetValue(newComponent, i.GetValue((Rigidbody)originalComp.fakeValue, null), null);
						}
					}
				}
				if(comp.ComponentType2 == "C# Script"){
					var originalGO2 = GameObject.Find(GetGameObjectPath(comp.gameObject).Replace(scene.name, originalScene.name));
            		FakeComponent originalComp2 = null;
            		foreach(var tempComp in originalGO2.GetComponents(typeof(FakeComponent))){
            			if(((FakeComponent)tempComp).realFakeComponentId != null && ((FakeComponent)tempComp).realFakeComponentId == comp.realFakeComponentId){
            				originalComp2 = ((FakeComponent)tempComp);
            				break;
            			}
            		}
            		CoroutineWithData cd = new CoroutineWithData(this, loadAsset("", false, originalComp2.loadedScriptAsset.meta.id));
					yield return cd.coroutine;
					LoadedAssetFile asset = (LoadedAssetFile)cd.result;
            		var componentAdded = child.AddComponent(asset.loadedType);
            		Type componentType = componentAdded.GetType();
            		componentType.GetProperty("enabled").SetValue(componentAdded, comp.enabled);
            		if(originalComp2 != null){
	            		var values = originalComp2.fakeValues;
						List<string> keys = new List<string>();
						foreach(string key in values.Keys){
							keys.Add(key);
						}
						for(int i=0; i<keys.Count; i++){
							var fakeScriptValue = (FakeScriptValue)values[keys[i]];
							if(fakeScriptValue.isField){
								var field = componentType.GetField(keys[i]);
								if(field != null){
									if(!fakeScriptValue.isActuallyAList){
										field.SetValue(componentAdded, fakeScriptValue.val);
									}
									else{
										var listType = typeof(List<>).MakeGenericType(fakeScriptValue.type.GetElementType());
										IList tempList = (IList)Activator.CreateInstance(listType);
										foreach(var child2 in (Array)fakeScriptValue.val){
											tempList.Add(child2);
										}
										field.SetValue(componentAdded, tempList);
									}
								}
							}
							else{
								var property = componentType.GetProperty(keys[i]);
								if(property != null){
									property.SetValue(componentAdded, fakeScriptValue.val);
								}
							}
						}
					}
				}
			}
			foreach(Component comp in fakeComponents){
				Destroy(comp);
			}
			foreach(var comp in child.GetComponent<ObjectController>().Components){
				if(comp!=null&&((string[])comp).Count()>0){
				Debug.Log(((string[])comp)[0]);
				if(((string[])comp)[0]=="BoxCollider"){
					child.AddComponent<BoxCollider>();
					BoxCollider col = child.GetComponent<BoxCollider>();
					col.size = new Vector3(((float[][])comp)[1][0],((float[][])comp)[1][1],((float[][])comp)[1][2]);
				}
				}
			}
			for(int i=0;i<child.gameObject.GetComponent<ObjectController>().Scripts.Count;i++){
				for(int j=0;j<ScriptsName.Count;j++){
					if(ScriptsName[j]==child.gameObject.GetComponent<ObjectController>().Scripts[i]){
						add(child.gameObject,ScriptsClass[j],Scripts[j],ScriptsUsing[j]);
					}
				}
			}
			try{
				if(child.GetComponent<ObjectController>().fakeCollider != null){
					Destroy(child.GetComponent<ObjectController>().fakeCollider);
				}
			}
			catch{}
		}
		originalScene.SetActive(false);
		//Debug.Log(gameObject.GetComponent<ObjectSaver>().Save(scene.gameObject,false));
		compiling.SetActive(false);
		for(int i=0;i<GameObject.FindGameObjectsWithTag("cam").Length;i++){
			GameObject.FindGameObjectsWithTag("cam")[i].transform.GetChild(0).gameObject.GetComponent<Camera>().enabled=true;
		}
		for(int i=0;i<GameObject.FindGameObjectsWithTag("Player").Length;i++){
			GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).gameObject.GetComponent<Camera>().enabled=true;
			GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).gameObject.tag="MainCamera";
			GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<FirstPersonExample2>().enabled=true;
		}
		if(GameObject.FindGameObjectsWithTag("Player").Length>0){
			joystickui.enabled = true;
		}
		maincam.gameObject.transform.parent.gameObject.GetComponent<FirstPersonExample>().enabled=true;
		maincam.gameObject.transform.parent.gameObject.tag="Untagged";
		maincam.enabled=false;
		editorui.SetActive(false);
		runtimeui.SetActive(true);
		setLandscape();
		isRunning = true;
		SimulationLaunched();
    }

    void SimulationLaunched(){
    	maincam.gameObject.SetActive(false);
    	maincam.gameObject.transform.parent.GetComponent<FirstPersonExample>().enabled = false;
    }

    void SimulationStopped(){
    	maincam.gameObject.SetActive(true);
    	maincam.gameObject.transform.parent.GetComponent<FirstPersonExample>().enabled = true;
    	/*
    	if(currObject!=null && currObject.GetComponent<ListObjectElement>().objct!=null){
    		cam.GetComponent<TransformGizmo>().AddTarget(currObject.GetComponent<ListObjectElement>().objct.transform);
    	}
    	*/
    	CheckSprites();
    }

    public void TextEdiorSave(){
    	TextEditorEditingCompVar.SetValue(TextEditorEditingComponent, TextEditorInput.text, null);
    	showAlert("Saved");
    }

    public void CheckSprites(){
    	Light[] lights = (Light[])GameObject.FindObjectsOfType(typeof(Light));
        foreach(Light light in lights){
        	if(light.gameObject.GetComponent<ObjectController>()!=null && light.gameObject.GetComponent<ObjectController>().lightSprite == null){
     			GameObject clone = Instantiate(floatingIcon, fakescene.transform);
     			clone.GetComponent<InstantFollow>().target = light.gameObject;
     			clone.GetComponent<InstantFollow>().sprite.GetComponent<SpriteRenderer>().sprite = lightIcon;
     			clone.GetComponent<InstantFollow>().isLight = true;
     			light.gameObject.GetComponent<ObjectController>().lightSprite = clone;
     		}
 		}
 		Camera[] cameras = (Camera[])GameObject.FindObjectsOfType(typeof(Camera));
        foreach(var child in cameras){
        	if(child.gameObject.GetComponent<ObjectController>()!=null && child.gameObject.GetComponent<ObjectController>().lightSprite == null){
     			GameObject clone = Instantiate(floatingIcon, fakescene.transform);
     			clone.GetComponent<InstantFollow>().target = child.gameObject;
     			clone.GetComponent<InstantFollow>().sprite.GetComponent<SpriteRenderer>().sprite = cameraIcon;
     			clone.GetComponent<InstantFollow>().isCamera = true;
     			child.gameObject.GetComponent<ObjectController>().lightSprite = clone;
     		}
 		}
    }

    void AddLog(Log_3DGMA log){
    	bool found = false;
    	int foundIndex = 0;
    	for(int i=0; i<logs.Count(); i++){
    		if(logs[i].logText == log.logText && logs[i].type == log.type){
    			found = true;
    			foundIndex = i;
    		}
    	}
    	if(found){
    		logs[foundIndex].repeated++;
    		logs[foundIndex].element.counter.text = logs[foundIndex].repeated.ToString();
    	}
    	else{
    		var clone = Instantiate(LogListElement, fakeconsole.transform);
    		log.element = clone.GetComponent<LogElement>();
    		log.element.mainText.text = log.logText;
    		log.element.GetComponent<Image>().color = new Color(1, (log.type == LogType.Exception ? 0 : 1), (log.type == LogType.Exception || log.type == LogType.Warning ? 0 : 1), (realLogCount % 2 == 1 ? 0.078f : 0.048f));
    		log.element.log = log;
    		logs.Add(log);
    		realLogCount++;
    		if(scrollWithOutputToggle.On){
	    		fakeconsole2.normalizedPosition = new Vector2(0, 0);
	    	}
    	}
    	if(logs.Count() > 200){
    		Destroy(logs[0].element.gameObject);
    		logs.RemoveAt(0);
    		logConsoleLast200Text.SetActive(true);
    	}
    }

    public void ClearLogs(){
    	logs = new List<Log_3DGMA>();
    	realLogCount = 0;
    	foreach(Transform child in fakeconsole.transform){
    		if(!GameObject.ReferenceEquals(child.gameObject, logConsoleLast200Text)){
				Destroy(child.gameObject);
			}
		}
		logConsoleLast200Text.SetActive(false);
    }

    public void SetJobText(string message){
    	jobText.text = message;
    	if(!busyScreen.GetComponent<FloatingPanel>().isPanelOpen){
    		busyScreen.GetComponent<FloatingPanel>().Open(true);
    	}
    }

    public void AddJobText(string message){
    	if(busyScreen.GetComponent<FloatingPanel>().isPanelOpen)
    		jobText.text = message + "\n" + jobText.text;
    	else
    		jobText.text = message;
    	if(!busyScreen.GetComponent<FloatingPanel>().isPanelOpen){
    		busyScreen.GetComponent<FloatingPanel>().Open(true);
    	}
    }

    public void ClearAddJobText(){
    	if(jobText.text.Split('\n').Length <= 1){
    		ClearJobText();
    	}
    }

    public void ClearJobText(){
    	if(busyScreen.GetComponent<FloatingPanel>().isPanelOpen){
    		busyScreen.GetComponent<FloatingPanel>().Close(true);
    	}
    }

    public void OpenFileChooser(bool selectFolder = false){
    	selectorui.SetActive(true);
    	fileChooserSelectFolderButton.gameObject.SetActive(selectFolder);
    	loadFiles2(filelistcurrent);
    }

    public void CloseFileChooser(){
    	selectingReason = "";
		selectorui.SetActive(false);
		selecting = false;
	}

	public void SaveScene(){
		StartCoroutine(SaveSceneCoroutine());
	}

	public IEnumerator SaveSceneCoroutine(){
		StreamWriter sw = File.CreateText(ProjectDirectory+"/Assets/Scene.sc");
		sw.Write(JsonConvert.SerializeObject(GetGameObjectForSaving(scene), Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
		{
		    PreserveReferencesHandling = PreserveReferencesHandling.All
		}));
		sw.Close();
		yield return null;
	}

	public SceneFileGameObject_3DGMA GetGameObjectForSaving(GameObject GO){
		var tempSFGO = new SceneFileGameObject_3DGMA();
		tempSFGO.id = "";
		tempSFGO.name = GO.name;
		tempSFGO.children = new List<SceneFileGameObject_3DGMA>();
		var comps = GO.GetComponents<Component>();
		foreach(Component comp in comps){
			try{
				var tempComp = new SceneFileComponent_3DGMA();
				if(comp.GetType() == typeof(cakeslice.Outline))
					continue;
				if(comp.GetType() != typeof(FakeComponent)){
					tempComp.type = comp.GetType();
					var props = comp.GetType().GetProperties();
					foreach(var prop in props){
						try{
							var result = MakeFHTE(prop.GetValue(comp), prop.PropertyType, prop.Name, comp);
							if(result != null)
								tempComp.props.Add(result);
						}
						catch{}
					}
					var fields = comp.GetType().GetFields();
					foreach(var field in fields){
						try{
							if(comp.GetType() == typeof(ObjectController) && field.Name == "selected")
								continue;
							var result = MakeFHTE(field.GetValue(comp), field.FieldType, field.Name, comp);
							if(result != null)
								tempComp.fields.Add(result);
						}
						catch{}
					}
				}
				else{
					tempComp.type = Type.GetType(((FakeComponent)comp).ComponentType);
					tempComp.fakeComponentName = ((FakeComponent)comp).ComponentType2;
					tempComp.fakeComponentName2 = ((FakeComponent)comp).ComponentName;
					tempComp.isFakeComponent = true;
					if(((FakeComponent)comp).loadedScriptAsset == null){
						Component fakeComp = ((Component)((FakeComponent)comp).fakeValue);
						var props = fakeComp.GetType().GetProperties();
						foreach(var prop in props){
							try{
								var result = MakeFHTE(prop.GetValue(fakeComp), prop.PropertyType, prop.Name, fakeComp);
								if(result != null)
									tempComp.props.Add(result);
							}
							catch{}
						}
						var fields = fakeComp.GetType().GetFields();
						foreach(var field in fields){
							try{
								var result = MakeFHTE(field.GetValue(fakeComp), field.FieldType, field.Name, fakeComp);
								if(result != null)
									tempComp.fields.Add(result);
							}
							catch{}
						}
					}
					else{
						try{
							tempComp.isScript = true;
							tempComp.scriptId = ((FakeComponent)comp).loadedScriptAsset.meta.id;
							Hashtable fakeComp = ((Hashtable)((FakeComponent)comp).fakeValues);
							var keys = new List<string>();
							foreach(string key in fakeComp.Keys){
								keys.Add(key);
							}
							foreach(string key in keys){
								try{
									var prop = (FakeScriptValue)fakeComp[key];
									Debug.Log(prop.val);
									var result = MakeFHTE(prop.val, prop.type, key, null);
									if(result != null)
										tempComp.props.Add(result);
								}
								catch(Exception err){
									Debug.LogException(err);
								}
							}
						}
						catch(Exception err){
							Debug.LogException(err);
						}
					}
				}
				tempSFGO.components.Add(tempComp);
			}
			catch{}
		}
		foreach(Transform child in GO.transform){
			tempSFGO.children.Add(GetGameObjectForSaving(child.gameObject));
		}
		return tempSFGO;
	}

	FakeHashtableElement_3DGMA MakeFHTE(object val, Type type, string name, Component comp = null, bool isArrayElement = false, string arrayCompVarName = ""){
		if(type == typeof(string) || type == typeof(int) || type == typeof(float) || type == typeof(double) || type == typeof(bool) || type == typeof(Single) || type == typeof(Vector2) || type == typeof(Vector3) || type == typeof(Quaternion) || type == typeof(Mesh) || type == typeof(Material) || type.IsArray || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>)) || type == typeof(Type) || type.IsEnum){
			var tempFHTE = new FakeHashtableElement_3DGMA();
			tempFHTE.type = type;
			tempFHTE.name = name;
			if(type != typeof(Mesh))
				tempFHTE.value = val;
			if(type == typeof(Single)){
				tempFHTE.custom = true;
			}
			else if(type == typeof(Quaternion)){
				var tempHT = new Hashtable();
				tempHT.Add("x", ((Quaternion)tempFHTE.value).x);
				tempHT.Add("y", ((Quaternion)tempFHTE.value).y);
				tempHT.Add("z", ((Quaternion)tempFHTE.value).z);
				tempHT.Add("w", ((Quaternion)tempFHTE.value).w);
				tempFHTE.value = tempHT;
			}
			else if(type == typeof(Mesh)){
				tempFHTE.custom = true;
				if((Mesh)val == primitiveCube)
					tempFHTE.value = "Cube";
				else if((Mesh)val == primitivePlane)
					tempFHTE.value = "Plane";
				else if((Mesh)val == primitiveSphere)
					tempFHTE.value = "Sphere";
				else
					tempFHTE.value = "Unknown";
			}
			else if(type == typeof(Material)){
				tempFHTE.custom = true;
				tempFHTE.value = null;
				if(isArrayElement){
					foreach (var prop in ((ComponentRef_3DGMA)components[comp.GetHashCode()]).props) 
					{
						if(prop.name == arrayCompVarName)
							tempFHTE.value = GetMetadata(new FileInfo(prop.arrayPaths[Int32.Parse(name)])).id;
					}
				}
				else{
					foreach (var prop in ((ComponentRef_3DGMA)components[comp.GetHashCode()]).props) 
					{
						if(prop.name == arrayCompVarName)
							tempFHTE.value = GetMetadata(new FileInfo(prop.path)).id;
					}
				}
			}
			else if(type.IsArray){
				List<FakeHashtableElement_3DGMA> tempList = new List<FakeHashtableElement_3DGMA>();
				var castedVal = (Array)val;
				for (int i=0; i<castedVal.Length; i++) 
				{
					object child = castedVal.GetValue(i);
					tempList.Add(MakeFHTE(child, type.GetElementType(), i.ToString(), comp, true, name));
				}
				tempFHTE.custom = true;
				tempFHTE.value = tempList;
			}
			else if(type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>)){
				List<FakeHashtableElement_3DGMA> tempList = new List<FakeHashtableElement_3DGMA>();
				var castedVal = (List<object>)val;
				for (int i=0; i<castedVal.Count; i++) 
				{
					object child = castedVal[i];
					tempList.Add(MakeFHTE(child, type.GetGenericArguments()[0], i.ToString(), comp, true, name));
				}
				tempFHTE.custom = true;
				tempFHTE.value = tempList;
			}
			else if(type.IsEnum){
				tempFHTE.value = (int)val;
				tempFHTE.custom = true;
			}
			return tempFHTE;
		}
		else{
			return null;
		}
	}

	public IEnumerator LoadScene(string path, string json = null){
		SetJobText("Loading scene...");
		try{
			StreamReader sr = null;
			string readFile;
			if(json == null){
				sr = new StreamReader(ProjectDirectory+"/Assets/"+path);
				readFile = sr.ReadToEnd();
			}
			else
				readFile = json;
			var loadedScene = JsonConvert.DeserializeObject<SceneFileGameObject_3DGMA>(readFile);
			cam.GetComponent<TransformGizmo>().ClearTargets();
			foreach(Transform child in scene.transform){
				Destroy(child.gameObject);
			}
			foreach(Transform child in fakescene.transform){
				Destroy(child.gameObject);
			}
			loadObjects2(scene);
			foreach(var child in loadedScene.children){
				//int start = new DateTime(DateTime.Now).GetMilliseconds();
				yield return LoadGameObject(child, scene.transform);
				//Debug.Log(new DateTime(DateTime.Now).GetMilliseconds() - start);
			}
			if(json == null)
				sr.Close();
			CheckSprites();
			loadObjects2(scene);
		}
	    finally{
	    	//showAlert("An error has occurred while loading a scene:\n" + err.Message, "Error");
	    	ClearJobText();
	    }
	    //ClearJobText();
	    yield return null;
	}

	public IEnumerator LoadGameObject(SceneFileGameObject_3DGMA SFGO, Transform parent){
		GameObject tempGO = new GameObject();
		tempGO.name = SFGO.name;
		tempGO.transform.parent = parent;
		foreach(var comp in SFGO.components){
			Component tempComp;
			if(!comp.isFakeComponent){
				if(comp.type == typeof(Transform))
					tempComp = tempGO.GetComponent<Transform>();
				else
					tempComp = tempGO.AddComponent(comp.type);
			}
			else{
				FakeComponent tempFakeComp = tempGO.AddComponent<FakeComponent>();
				if(!comp.isScript)
					tempFakeComp.ComponentType = comp.type.AssemblyQualifiedName;
				tempFakeComp.ComponentType2 = comp.fakeComponentName;
				tempFakeComp.ComponentName = comp.fakeComponentName2;
				if(comp.isScript){
					CoroutineWithData cd = new CoroutineWithData(this, loadAsset(null, false, comp.scriptId));
					yield return cd.coroutine;
					tempFakeComp.loadedScriptAsset = (LoadedAssetFile)cd.result;
				}
				tempFakeComp.CreateFakeComponent();
				if(!comp.isScript)
					tempComp = (Component)tempFakeComp.fakeValue;
				else
					tempComp = (Component)tempFakeComp;
				if(comp.isScript){
					foreach(var prop in comp.props){
						try{
							FakeScriptValue tempFSV = new FakeScriptValue();
							CoroutineWithData cd = new CoroutineWithData(this, LoadCompVar(prop));
							yield return cd.coroutine;
							tempFSV.val = cd.result;
							tempFSV.type = prop.type;
							((Hashtable)tempFakeComp.fakeValues)[prop.name] = tempFSV;
						}
						finally{}
					}
				}
			}
			if(!comp.isScript){
				foreach(var prop in comp.props){
					CoroutineWithData cd = new CoroutineWithData(this, LoadCompVar(prop));
				    yield return cd.coroutine;
					try{
						comp.type.GetProperty(prop.name).SetValue(tempComp, cd.result);
					}
					catch{}
				}
				foreach(var field in comp.fields){
					CoroutineWithData cd = new CoroutineWithData(this, LoadCompVar(field));
					yield return cd.coroutine;
					try{
						comp.type.GetField(field.name).SetValue(tempComp, cd.result);
					}
					catch{}
				}
			}
		}
		foreach(var child in SFGO.children){
			yield return LoadGameObject(child, tempGO.transform);
		}
	}

	IEnumerator LoadCompVar(FakeHashtableElement_3DGMA prop){
		object result = new object();
		if(prop != null){
			if(!prop.custom){
				try{
					result = ((Newtonsoft.Json.Linq.JObject)prop.value).ToObject(prop.type);
				}
				catch{
					try{
						result = prop.value;
					}
					catch{}
				}
			}
			else{
				if(prop.type == typeof(Single)){
					result = Convert.ToSingle((double)prop.value);
				}
				else if(prop.type == typeof(Mesh)){
					if((string)prop.value == "Cube")
						result = primitiveCube;
					if((string)prop.value == "Plane")
						result = primitivePlane;
					if((string)prop.value == "Sphere")
						result = primitiveSphere;
				}
				else if(prop.type == typeof(Material)){
					if(prop.value != null){
						CoroutineWithData cd = new CoroutineWithData(this, loadAsset(null, false, (string)prop.value));
						yield return cd.coroutine;
						result = ((LoadedAssetFile)cd.result).loadedMaterial;
					}
					else
						result = defaultMaterial;
				}
				else if(prop.type.IsArray){
					var castedVal = ((Newtonsoft.Json.Linq.JArray)prop.value).ToObject<List<FakeHashtableElement_3DGMA>>();
					Array result2 = Array.CreateInstance(prop.type.GetElementType(), castedVal.Count);
					Type elementType = prop.type.GetElementType();
					for(int i=0; i<castedVal.Count; i++) 
					{
						try{
							var child = castedVal[i];
							if(elementType == typeof(int) || elementType == typeof(byte) || elementType == typeof(sbyte) || elementType == typeof(short) || elementType == typeof(ushort) || elementType == typeof(uint) || elementType == typeof(long) || elementType == typeof(ulong) || elementType == typeof(float) || elementType == typeof(double) || elementType == typeof(decimal) || elementType == typeof(Int32) || elementType == typeof(Int64)){
								var method = elementType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(string) }, null);
								CoroutineWithData cd = new CoroutineWithData(this, LoadCompVar(child));
								yield return cd.coroutine;
								if(cd.result != null)
									result2.SetValue(method.Invoke(null, new string[]{cd.result.ToString()}), Int32.Parse(child.name));
							}
							else{
								CoroutineWithData cd = new CoroutineWithData(this, LoadCompVar(child));
								yield return cd.coroutine;
								Debug.Log(cd.result);
								try{
									if(cd.result != null && child != null && child.name != null)
										result2.SetValue(cd.result, Int32.Parse(child.name));
								}
								catch(Exception err){
									Debug.LogException(err);
									Debug.Log(cd.result);
								}
							}
						}
						finally{}
					}
					result = result2;
				}
				else if(prop.type.IsGenericType && prop.type.GetGenericTypeDefinition() == typeof(List<>)){
					var castedVal = ((Newtonsoft.Json.Linq.JArray)prop.value).ToObject<List<FakeHashtableElement_3DGMA>>();
					IList result2 = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(prop.type.GetElementType()));
					for(int i=0; i<castedVal.Count; i++) 
					{
						var child = castedVal[i];
						CoroutineWithData cd = new CoroutineWithData(this, LoadCompVar(child));
						yield return cd.coroutine;
						result2.Add(cd.result);
					}
					result = result2;
				}
				else{
					FieldInfo[] fields = prop.type.GetFields(/*BindingFlags.NonPublic | */BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
					if(fields.Length > 1 && fields[1].FieldType == prop.type){
						result = Convert.ChangeType(Enum.Parse(prop.type, prop.value.ToString()), prop.type);
					}
				}
			}
		}
		else{
			result = null;
		}
		yield return result;
	}

	private static List<T> GetAsList<T>(object[] oldList) 
    {
        List<T> list = new List<T>();
        foreach (var item in oldList)
        {
            System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
            {
                list.Add((T)converter.ConvertFrom(item));
            }
        }
        return list;
    }

	public void CopyMoveConfirmation(){
		UnityEvent tempEvent = new UnityEvent();
		tempEvent.AddListener(() => {ChooseFolder();});
		showAlert("Any file that already exists will be overwritten.", "Warning", tempEvent);
	}

	public void DeleteConfirmation(){
		UnityEvent tempEvent = new UnityEvent();
		tempEvent.AddListener(() => {ChooseFolder();});
		showAlert("You are about to DELETE file/s" + '\n' + "This action can't be undone" + '\n' + "Are you sure you want to continue?", "Warning", tempEvent);
	}

	public void ChooseFolder(){
    	StartCoroutine(ChooseFolderCoroutine());
    }

    IEnumerator ChooseFolderCoroutine(){
    	if(selectingReason == "Copy file")
    		SetJobText("Copying files...");
    	else if(selectingReason == "Move file")
    		SetJobText("Moving files...");
    	else if(selectingReason == "Delete file")
    		SetJobText("Deleting files...");
    	yield return null;
    	if(selectingReason == "Copy file" || selectingReason == "Move file"){
	    	foreach(string file in selectedFiles){
	    		FileInfo file1 = new FileInfo(ProjectDirectory+"/Assets"+file);
	    		DirectoryInfo folder1 = new DirectoryInfo(ProjectDirectory+"/Assets"+file);
	    		if(selectingReason == "Copy file")
	    			SetJobText("Copying " + file1.Name + "...");
	    		else if(selectingReason == "Move file")
	    			SetJobText("Moving " + file1.Name + "...");
	    		yield return null;
	    		FileInfo file2 = new FileInfo(ProjectDirectory+"/Assets"+file+".meta_3dgma");
	    		if(file1.Exists){
	    			if(!IsFileLocked(file1.FullName) && file1.FullName != new FileInfo(ProjectDirectory+"/Assets"+filelistcurrent+"/"+file1.Name).FullName){
		    			if(selectingReason == "Copy file")
			    			File.Copy(file1.FullName, ProjectDirectory+"/Assets"+filelistcurrent+"/"+file1.Name, true);
			    		else if(selectingReason == "Move file"){
			    			file1.CopyTo(ProjectDirectory+"/Assets"+filelistcurrent+"/"+file1.Name, true);
			    			file1.Delete();
			    		}
			    	}
			    	else{
			    		ClearJobText();
			    		string error = "Error copying/moving a file: ";
			    		if(IsFileLocked(file1.FullName))
			    			error += "The file is in use";
			    		else if(file1.FullName == new FileInfo(ProjectDirectory+"/Assets"+filelistcurrent+"/"+file1.Name).FullName)
			    			error += "You are trying to copy/move a file to the directory it was already in";
						showAlert(error, "Error");
						throw new Exception(error);
			    	}
	    		}
	    		else if(folder1.Exists){
	    			CopyAll(folder1, new DirectoryInfo(ProjectDirectory+"/Assets"+filelistcurrent+"/"+folder1.Name));
	    		}
	    		if(file2.Exists && selectingReason == "Move file"){
	    			if(!IsFileLocked(file2.FullName)){
		    			file2.CopyTo(ProjectDirectory+"/Assets"+filelistcurrent+"/"+file2.Name, true);
		    			file2.Delete();
		    		}
		    		else{
		    			ClearJobText();
						showAlert("Error copying/moving a file: The file is in use", "Error");
						throw new Exception("Error copying/moving a file: The file is in use");
		    		}
	    		}
		    }
		}
		else if(selectingReason == "Delete file"){
			foreach(string file in selectedFiles){
	    		FileInfo file1 = new FileInfo(ProjectDirectory+"/Assets"+file);
	    		DirectoryInfo folder1 = new DirectoryInfo(ProjectDirectory+"/Assets"+file);
	    		SetJobText("Deleting " + file1.Name + "...");
	    		yield return null;
	    		FileInfo file2 = new FileInfo(ProjectDirectory+"/Assets"+file+".meta_3dgma");
	    		if(file1.Exists){
	    			if(!IsFileLocked(file1.FullName)){
	    				file1.Delete();
	    			}
	    			else{
	    				ClearJobText();
						showAlert("Error deleting a file: The file is in use", "Error");
						throw new Exception("Error deleting a file: The file is in use");
	    			}
	    		}
	    		else if(folder1.Exists){
	    			DeleteDirectory(folder1.FullName);
	    		}
	    		if(file2.Exists){
	    			if(!IsFileLocked(file2.FullName)){
	    				file2.Delete();
	    			}
	    			else{
	    				ClearJobText();
						showAlert("Error deleting a file: The file is in use", "Error");
						throw new Exception("Error deleting a file: The file is in use");
	    			}
	    		}
	    	}
		}
    	ClearJobText();
    	CloseFileChooser();
    	loadFiles2(filelistcurrent);
    }

    public void CopyAll(DirectoryInfo source, DirectoryInfo target)
    {
    	DirectoryInfo di1 = source;
		DirectoryInfo di2 = target;
		bool isParent = false;
		while (di2.Parent != null)
		{
		    if (di2.Parent.FullName == di1.FullName)
		    {
		        isParent = true;
		        break;
		    }
		    else di2 = di2.Parent;
		}

		if(isParent || source.FullName == target.FullName){
			ClearJobText();
			showAlert("Error copying/moving a folder: The target folder is inside the source folder", "Error");
			throw new Exception("Error copying/moving a folder: The target folder is inside the source folder");
		}

        Directory.CreateDirectory(target.FullName);

        // Copy each file into the new directory.
        foreach (FileInfo fi in source.GetFiles())
        {
            if(!IsFileLocked(fi.FullName)){
	            if(selectingReason == "Copy file" && fi.Extension.ToLower() != ".meta_3dgma")
	    			fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
	    		else if(selectingReason == "Move file"){
	    			fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
	    			fi.Delete();
	    		}
    		}
    		else{
    			ClearJobText();
				showAlert("Error copying/moving a folder: At least one of the files is in use", "Error");
				throw new Exception("Error copying/moving a folder: At least one of the files is in use");
    		}
        }

        // Copy each subdirectory using recursion.
        foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
        {
            DirectoryInfo nextTargetSubDir =
                target.CreateSubdirectory(diSourceSubDir.Name);
            CopyAll(diSourceSubDir, nextTargetSubDir);
        }

        try{
        	if(selectingReason == "Move file")
        		Directory.Delete(source.FullName);
        }
        catch{}
    }

    public bool IsFileLocked(string filename)
    {
        bool Locked = false;
        try
        {
            FileStream fs =
                File.Open(filename, FileMode. OpenOrCreate,
                FileAccess.ReadWrite, FileShare.None);
            fs.Close();
        }
        catch (IOException ex)
        {
            Locked = true;
        }
        return Locked;
    }

    public void DeleteDirectory(string path)
	{
	    foreach (string directory in Directory.GetDirectories(path))
	    {
	        DeleteDirectory(directory);
	    }

	    try
	    {
	        Directory.Delete(path, true);
	    }
	    catch (IOException) 
	    {
	        Directory.Delete(path, true);
	    }
	    catch (UnauthorizedAccessException)
	    {
	        Directory.Delete(path, true);
	    }
	}

	public void OpenArrayEditor(){
		hideInArrayEditor.SetActive(false);
		arrayeditorui.SetActive(true);
	}

	public void CloseArrayEditor(){
		hideInArrayEditor.SetActive(true);
		arrayeditorui.SetActive(false);
	}

	public void OpenColorEditor(){
		hideInArrayEditor.SetActive(false);
		coloreditorui.SetActive(true);
		colorEditorColorPicker.editingVar = colorEditing;
		colorEditorColorPicker.color = (Color)(colorEditing.GetVar());
	}

	public void CloseColorEditor(){
		hideInArrayEditor.SetActive(true);
		coloreditorui.SetActive(false);
		colorEditorColorPicker.editingVar = null;
	}

	public void CloseComponentEditor(){
		componentEditor.SetActive(false);
		foreach(Transform child in componentEditor.transform){
			Destroy(child.gameObject);
		}
	}

	public void UpdateArrayEditor(){
		arraySizeInput.text = arrayEditing.Length.ToString();
		foreach(Transform child in arrayEditorViewportContent.transform){
			Destroy(child.gameObject);
		}
		for(int i=0; i<arrayEditing.Length; i++){
			GameObject clone;
			clone = Instantiate(ComponentVarElement, arrayEditorViewportContent.transform);
			clone.GetComponent<ComponentVarElement>().isFakeScriptValue = arrayEditingElement.isFakeScriptValue;
			clone.GetComponent<ComponentVarElement>().fakeScriptValueName = arrayEditingElement.fakeScriptValueName;
			if(arrayEditingElement.varType.IsGenericType && arrayEditingElement.varType.GetGenericTypeDefinition() == typeof(List<>)){
				clone.GetComponent<ComponentVarElement>().varType = arrayEditingElement.varType.GetGenericArguments().FirstOrDefault();
				clone.GetComponent<ComponentVarElement>().isListElement = true;
			}
			else{
				clone.GetComponent<ComponentVarElement>().varType = arrayEditingElement.varType.GetElementType();
			}
			clone.GetComponent<ComponentVarElement>().set(0, null, null, null, null, arrayEditingElement.component, false, true, i);
		}

		arrayEditingElement.SetVar(Resize((Array)arrayEditing, arrayEditing.Length, arrayType));
	}

	public void UpdateArraySize(){
		if(!String.IsNullOrEmpty(arraySizeInput.text)){
			if(!arrayEditingElement.isFakeScriptValue)
				Array.Resize<object>(ref arrayEditing, Int32.Parse(arraySizeInput.text));
			else{
				object[] tempArray = ((IEnumerable)((Array)((FakeScriptValue)((FakeComponent)arrayEditingElement.component).fakeValues[arrayEditingElement.fakeScriptValueName]).val)).Cast<object>().ToArray();
				Array.Resize<object>(ref tempArray, Int32.Parse(arraySizeInput.text));
				((FakeScriptValue)((FakeComponent)arrayEditingElement.component).fakeValues[arrayEditingElement.fakeScriptValueName]).val = tempArray;
				Array.Resize<object>(ref arrayEditing, Int32.Parse(arraySizeInput.text));
			}
		}
		UpdateArrayEditor();
	}

	static object Resize(Array array, int newSize, Type customType) {        
    	Type elementType = customType!=null ? customType : array.GetType().GetElementType();
    	Array newArray = Array.CreateInstance(elementType, newSize);
    	Array.Copy(array, newArray, Math.Min(array.Length, newArray.Length));
    	return newArray;
	}

	public IEnumerator loadAsset(string path = "", bool forced = false, string id = null){
		if(!(path == null || path == ""))
			path = (path[0] == "/"[0] ? path.Remove(0,1) : path);
		bool alreadyLoaded = false;
		LoadedAssetFile old = new LoadedAssetFile();
		int oldIndex = -1;
		if(path != null && path != ""){
			old.path = path;
			for(int i=0; i<loadedAssets.Count(); i++){
				if(loadedAssets[i].path == path){
					alreadyLoaded = true;
					old = loadedAssets[i];
					oldIndex = i;
				}
			}
		}
		else{
			for(int i=0; i<loadedAssets.Count(); i++){
				if(loadedAssets[i].meta.id == id){
					alreadyLoaded = true;
					old.path = loadedAssets[i].path;
					old = loadedAssets[i];
					oldIndex = i;
				}
			}
			if(!alreadyLoaded){
				FileInfo[] allFiles = GetAllFiles("/");
				foreach(FileInfo file in allFiles){
					var meta = GetMetadata(file);
					if(meta.id == id){
						CoroutineWithData cd = new CoroutineWithData(this, loadAsset(file.FullName.Replace('\\', '/').Substring((ProjectDirectory+"/Assets/").Length-1)));
						yield return cd.coroutine;
						var loaded = (LoadedAssetFile)cd.result;
						alreadyLoaded = true;
						old.path = loaded.path;
						old = loaded;
						oldIndex = loadedAssets.Count()-1;
					}
				}
			}
		}
		if(!alreadyLoaded || forced){
			CoroutineWithData cd = new CoroutineWithData(this, convertToAsset(path));
			yield return cd.coroutine;
			LoadedAssetFile newAsset = (LoadedAssetFile)cd.result;
			if(!forced){
				loadedAssets.Add(newAsset);
			}
			else{
				if(oldIndex == -1){
					loadedAssets.Add(newAsset);
					Debug.Log("Doesn't exist, loading 1");
				}
				else{
					//loadedAssets[oldIndex] = newAsset;
					loadedAssets[oldIndex].path = newAsset.path;
					loadedAssets[oldIndex].type = newAsset.type;
					loadedAssets[oldIndex].meta = newAsset.meta;
					loadedAssets[oldIndex].loadedType = newAsset.loadedType;
					if(loadedAssets[oldIndex].loadedMaterial != null){
						var properties = newAsset.loadedMaterial.GetType().GetProperties();
						foreach(var prop in properties)
						{
							try{
								prop.SetValue(loadedAssets[oldIndex].loadedMaterial, prop.GetValue(newAsset.loadedMaterial));
							}
							catch{}
						}
						loadedAssets[oldIndex].loadedMaterial.SetFloat("_Metallic", newAsset.loadedMaterial.GetFloat("_Metallic"));
						loadedAssets[oldIndex].loadedMaterial.SetFloat("_Glossiness", newAsset.loadedMaterial.GetFloat("_Glossiness"));
						loadedAssets[oldIndex].loadedMaterial.SetTexture("_MainTex", newAsset.loadedMaterial.GetTexture("_MainTex"));
					}
					Debug.Log("Doesn't exist, loading 2");
					if(newAsset.type == "Type"){
						var allGO = GetAllGameObjects(scene);
						foreach(var child in allGO){
							foreach(var comp in child.GetComponents<FakeComponent>()){
								try{
									if(comp.ComponentType2 == "C# Script" && comp.loadedScriptAsset.loadedType == newAsset.loadedType){
										comp.UpdateFakeValues();
									}
								}
								catch{}
							}
						}
					}
					if(componentEditor.transform.childCount >= 1 && componentEditor.transform.GetChild(0).GetComponent<ComponentElement>() != null && componentEditor.transform.GetChild(0).GetComponent<ComponentElement>().component.GetType() == typeof(FakeComponent) && ((FakeComponent)componentEditor.transform.GetChild(0).GetComponent<ComponentElement>().component).ComponentType2 == "C# Script" && ((FakeComponent)componentEditor.transform.GetChild(0).GetComponent<ComponentElement>().component).loadedScriptAsset.loadedType == newAsset.loadedType){
						foreach(Transform child in componentEditor.transform.GetChild(0).GetComponent<ComponentElement>().VarList.transform){
							Destroy(child.gameObject);
						}
						componentEditor.transform.GetChild(0).GetComponent<ComponentElement>().Open();
					}
				}
			}
			yield return newAsset;
		}
		else{
			Debug.Log("Already exists, not reloading");
			yield return old;
		}
	}

	public void RemoveAsset(string path){
		path = (path[0] == "/"[0] ? path.Remove(0,1) : path);
		int index = -1;
		for(int i=0; i<loadedAssets.Count(); i++){
			if(loadedAssets[i].path == path){
				index = i;
			}
		}
		if(index != -1){
			loadedAssets.RemoveAt(index);
		}
	}

	public IEnumerator convertToAsset(string path){
		LoadedAssetFile newAsset = new LoadedAssetFile();
		newAsset.path = (path[0] == "/"[0] ? path.Remove(0,1) : path);

		string extension = path.Split(Convert.ToChar("."))[path.Split(Convert.ToChar(".")).Length-1].ToLower();
		bool typeFound = false;
		newAsset.meta = GetMetadata(new FileInfo(ProjectDirectory+"/Assets/"+path), null);
		StreamReader sr = null;

		if(extension == "ma"){
			sr = new StreamReader(ProjectDirectory+"/Assets/"+path);
			newAsset.type = "Material";
			typeFound = true;
			Mat_3DGMA Mat = JsonUtility.FromJson<Mat_3DGMA>(sr.ReadToEnd());
			newAsset.loadedMaterial = new Material(materialTemplate);
			newAsset.loadedMaterial.SetColor("_Color", Mat.MainColor);
			if(Mat.isEmissionEnabled){
				newAsset.loadedMaterial.EnableKeyword("_EMISSION");
			}
			else{
				newAsset.loadedMaterial.DisableKeyword("_EMISSION");
			}
			newAsset.loadedMaterial.SetColor("_EmissionColor", Mat.EmissionColor);
			newAsset.loadedMaterial.SetFloat("_Metallic", Mat.Metallic);
			newAsset.loadedMaterial.SetFloat("_Glossiness", Mat.Smoothness);
			try{
				CoroutineWithData cd = new CoroutineWithData(this, loadAsset(null, false, Mat.MainTexId));
				yield return cd.coroutine;
				newAsset.loadedMaterial.SetTexture("_MainTex", ((LoadedAssetFile)cd.result).loadedTexture);
			}
			finally{}
		}

		if(extension == "cs"){
			sr = new StreamReader(ProjectDirectory+"/Assets/"+path);
			newAsset.type = "Type";
			typeFound = true;
			try{
				newAsset.loadedType = CompileScriptAndGetType2(sr.ReadToEnd());
			}
			catch(Exception e){
				sr.Close();
				ClearJobText();
				throw e;
			}
		}

		if(extension == "jpg" || extension == "jpeg" || extension == "png" || extension == "gif"){
			newAsset.type = "Texture";
			typeFound = true;
			Texture2D tex;
	        tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
	        AddJobText("Loading textures...");
			yield return null;
	        using (WWW www = new WWW("file://"+ProjectDirectory+"/Assets/"+path))
	        {
	        	yield return www;
	            www.LoadImageIntoTexture(tex);
	            newAsset.loadedTexture = (Texture)tex;
	        }
	        ClearAddJobText();
		}

		if(sr != null)
			sr.Close();
		yield return newAsset;
	}

	public IEnumerator CheckFilesMetadata(){
		watcher.EnableRaisingEvents = false;
		busy = true;
		try{
			SetJobText("Checking files");
	        yield return null;
			FileInfo[] allFiles = GetAllFiles("/");
			DirectoryInfo[] allDirs = GetAllDirectories("/");
			string assetsFolderPath = new DirectoryInfo(ProjectDirectory+"/Assets").FullName;
			var lastFive = new List<FileInfo>();
			for(int i=0; i<allFiles.Length; i++){
				FileInfo file = allFiles[i];
				var hasMetadata = CheckMetadata(file, null);
				if(hasMetadata){
					FileMetadata_3DGMA2 metadata2 = new FileMetadata_3DGMA2();
					metadata2.meta = GetMetadata(file, null);
					string path = file.FullName.Replace(assetsFolderPath, "").Replace("\\","/");
			        path = (path[0] == "/"[0] ? path.Remove(0,1) : path);
					metadata2.path = path;
					loadedMetas.Add(metadata2);
				}
				else{
	        		lastFive.Add(file);
	        		if(lastFive.Count() >= 5){
	        			SetJobText("Creating metadata for\n\r"+lastFive[0].Name+"\n\r"+lastFive[1].Name+"\n\r"+lastFive[2].Name+"\n\r"+lastFive[3].Name+"\n\r"+lastFive[4].Name);
	        			yield return null;
	        			lastFive.Clear();
	        		}
				}
			}
			SetJobText("Checking directories...");
	        yield return null;
			for(int i=0; i<allDirs.Length; i++){
				DirectoryInfo dir = allDirs[i];
				CheckMetadata(null, dir);
			}
		}
		finally
		{
		}
		ClearJobText();
		yield return null;
		busy = false;
		watcher.EnableRaisingEvents = true;
	}

	public bool CheckMetadata(FileInfo file, DirectoryInfo dir){
		if(file != null && !file.Exists){
			FileInfo newFile = new FileInfo(ProjectDirectory + "/Assets/" + file.ToString());
			if(newFile.Exists){
				file = newFile;
			}
		}
		if(file != null && !(new FileInfo(file.FullName+".meta_3dgma").Exists)){
			CreateMetadata(file, null);
			return false;
		}
		if(dir != null && !(new FileInfo(dir.FullName.Substring(0, dir.FullName.Length-1)+".meta_3dgma").Exists)){
			CreateMetadata(null, dir);
			return false;
		}
		return true;
	}

	public FileMetadata_3DGMA GetMetadata(FileInfo file, DirectoryInfo dir = null){
		if(file != null && !file.Exists){
			FileInfo newFile = new FileInfo(ProjectDirectory + "/Assets/" + file.ToString());
			if(newFile.Exists){
				file = newFile;
			}
		}
		CheckMetadata(file, dir);
		StreamReader sr = new StreamReader((dir==null ? file.FullName : dir.FullName.Substring(0, dir.FullName.Length-1)) + ".meta_3dgma");
		FileMetadata_3DGMA metadata = JsonUtility.FromJson<FileMetadata_3DGMA>(sr.ReadToEnd());
		sr.Close();
		return metadata;
	}

	public void CreateMetadata(FileInfo file, DirectoryInfo dir){
		//Debug.Log("Creating metadata file for " + (dir==null ? file.Name : dir.Name));
		StreamWriter sw = File.CreateText((dir==null ? file.FullName : dir.FullName.Substring(0, dir.FullName.Length-1)) + ".meta_3dgma");
		var newMetadata = new FileMetadata_3DGMA();
		newMetadata.version = Application.version;
		newMetadata.id = Guid.NewGuid().ToString();
		sw.Write(JsonUtility.ToJson(newMetadata));
		sw.Close();
		string assetsFolderPath = new DirectoryInfo(ProjectDirectory+"/Assets").FullName;
		string path = (dir==null ? file.FullName : dir.FullName).Replace(assetsFolderPath, "").Replace("\\","/");
		path = (path[0] == "/"[0] ? path.Remove(0,1) : path);
		FileMetadata_3DGMA2 metadata2 = new FileMetadata_3DGMA2();
		metadata2.meta = GetMetadata(file, dir);
		metadata2.path = path;
		loadedMetas.Add(metadata2);
		if(file != null){
			for(int i=0; i<loadedAssets.Count(); i++){
				if(loadedAssets[i].path == path){
					loadedAssets[i].meta = newMetadata;
				}
			}
		}
	}

	public FileInfo[] GetAllFiles(string path, bool includeMetadata=false){
		var result = new List<FileInfo>();
		DirectoryInfo[] allDirs = GetAllDirectories(path);
		foreach(DirectoryInfo dir in allDirs){
			foreach(FileInfo file in dir.GetFiles()){
				if(includeMetadata || !file.Name.Contains(".meta_3dgma")){
					result.Add(file);
				}
			}
		}
		return result.ToArray();
	}

	public DirectoryInfo[] GetAllDirectories(string path){
		var result = new List<DirectoryInfo>();
		DirectoryInfo dirinfo = new DirectoryInfo(ProjectDirectory+"/Assets"+path);
		result.Add(dirinfo);
		DirectoryInfo[] directories = dirinfo.GetDirectories();
		foreach(DirectoryInfo dir in directories){
			DirectoryInfo[] result2 = GetAllDirectories(path+dir.Name+"/");
			result.AddRange(result2);
		}
		return result.ToArray();
	}

	public string GetGameObjectPath(GameObject obj)
	{
	    string path = "/" + obj.name;
	    while (obj.transform.parent != null)
	    {
	        obj = obj.transform.parent.gameObject;
	        path = "/" + obj.name + path;
	    }
	    return path;
	}

	public void CheckAssetsFolderChanges(object source, FileSystemEventArgs e){
		if(!busyCheckingFiles && !busy && e.ChangeType == WatcherChangeTypes.Changed){
			checkAssetsFolderChanges = true;
		}
	}

	public IEnumerator CheckAssetsFolderChanges2(){
		busyCheckingFiles = true;
		string assetsFolderPath = new DirectoryInfo(ProjectDirectory+"/Assets").FullName;
		DateTime dt1970 = new DateTime(1970, 1, 1);
        if(true){
        	SetJobText("Checking for changes in the Assets folder");
        	yield return null;
        	var modifiedFiles = new List<FileInfo>();
        	//var modifiedDirs = new List<DirectoryInfo>();
        	FileInfo[] allFiles = GetAllFiles("/", true);
        	//DirectoryInfo[] allDirs = GetAllDirectories("/");
        	foreach(FileInfo file in allFiles){
        		DateTime current2 = file.LastAccessTime;
    			TimeSpan span2 = current2 - dt1970;
    			if(file.Extension.ToLower() != ".meta_3dgma" && ((!cachedAccessDates.ContainsKey(file.FullName) || span2.TotalMilliseconds != (double)cachedAccessDates[file.FullName]) || !cachedPaths.ContainsKey(file.FullName))){
    				//Debug.Log(file.Name + " " + (Math.Abs(lastAssetsFolderChange - span2.TotalMilliseconds) < 100) + " || " + (Math.Abs(lastAssetsFolderChange - span3.TotalMilliseconds) < 100) + " || " + (lastAssetsFolderChange < span2.TotalMilliseconds) + " || " + (lastAssetsFolderChange < span3.TotalMilliseconds) + " || " + (!cachedPaths.ContainsKey(file.FullName)));
    				cachedAccessDates[file.FullName] = span2.TotalMilliseconds;
    				modifiedFiles.Add(file);
    				CheckMetadata(file, null);
    			}
        	}
        	if(modifiedFiles.Count() > 0){

	        	SetJobText("Updating file path cache");
	        	yield return null;
	        	cachedPaths.Clear();
	        	for(int i=0; i<allFiles.Count(); i++){
	        		FileInfo file = allFiles[i];
	        		cachedPaths.Add(file.FullName, true);
	        	}
	        	
	        	for(int i=0; i<loadedMetas.Count(); i++){
	        		FileMetadata_3DGMA2 meta = loadedMetas[i];
	        		if(!cachedPaths.ContainsKey(new FileInfo(ProjectDirectory+"/Assets/"+meta.path).FullName)){
	        			loadedMetas.RemoveAt(i);
	        		}
	        	}
	        	var cachedPathsList = cachedPaths.Keys.Cast<string>().ToList();
	        	var loadedMetasPathList = new List<string>();
	        	for(int i=0; i<loadedMetas.Count(); i++){
	        		loadedMetasPathList.Add(loadedMetas[i].path);
	        	}
	        	for(int i=0; i<cachedPathsList.Count(); i++){
	        		string path = cachedPathsList[i];
	        		if((new FileInfo(path)).Extension.ToLower() != ".meta_3dgma"){
		        		string path2 = path.Replace(assetsFolderPath, "").Replace("\\","/");
				        path2 = (path2[0] == "/"[0] ? path2.Remove(0,1) : path2);
		        		if(!loadedMetasPathList.Contains(path2)){
		        			FileMetadata_3DGMA2 newMetadata = new FileMetadata_3DGMA2();
		        			newMetadata.path = path2;
		        			newMetadata.meta = GetMetadata(new FileInfo(path), null);
		        			loadedMetas.Add(newMetadata);
		        		}
	        		}
	        	}

	        	SetJobText("Checking if any of the new files could be a moved/renamed file instead of an actual new file");
	        	yield return null;
	        	foreach(FileInfo file in modifiedFiles.ToArray()){
	    			CheckMetadata(file, null);
	    		}
	    		var modifiedFilesPathList = new List<string>();
	    		for(int l=0; l<modifiedFiles.Count(); l++){
	    			FileInfo file = modifiedFiles[l];
	    			string path2 = file.FullName.Replace(assetsFolderPath, "").Replace("\\","/");
			        path2 = (path2[0] == "/"[0] ? path2.Remove(0,1) : path2);
	    			modifiedFilesPathList.Add(path2);
	    		}
	    		Hashtable loadedMetas2 = new Hashtable();
	    		for(int l=0; l<loadedMetas.Count(); l++){
	    			FileMetadata_3DGMA2 meta = loadedMetas[l];
	    			if(!loadedMetas2.ContainsKey(meta.path) && !modifiedFilesPathList.Contains(meta.path)){
	    				loadedMetas2.Add(meta.path, meta);
	    			}
	    		}
	    		Hashtable loadedMetas3 = new Hashtable();
	    		for(int l=0; l<loadedMetas.Count(); l++){
	    			FileMetadata_3DGMA2 meta = loadedMetas[l];
	    			if(!modifiedFilesPathList.Contains(meta.path)){
		    			if(!loadedMetas3.ContainsKey(meta.meta.id)){
		    				loadedMetas3.Add(meta.meta.id, meta);
		    			}
		    			else{
		    				FileMetadata_3DGMA2 newMetadata = new FileMetadata_3DGMA2();
		    				CheckMetadata(new FileInfo(ProjectDirectory+"/Assets/"+meta.path), null);
		    				File.Delete(new FileInfo(ProjectDirectory+"/Assets/"+meta.path+".meta_3dgma").FullName);
		    				loadedMetas.RemoveAt(l);
		    				newMetadata.meta = GetMetadata(new FileInfo(ProjectDirectory+"/Assets/"+meta.path), null);
		    				newMetadata.path = meta.path;
		    				loadedMetas3.Add(newMetadata.meta.id, newMetadata);
		    			}
	    			}
	    		}
	    		var lastFive = new List<FileInfo>();
	        	for(int j=0; j<modifiedFiles.Count(); j++){
	        		FileInfo file = modifiedFiles[j];
	        		FileMetadata_3DGMA fileMetadata = GetMetadata(file, null);
	        		if(loadedMetas3.ContainsKey(fileMetadata.id)){
	    				File.Delete(file.FullName+".meta_3dgma");
	    				string path2 = file.FullName.Replace(assetsFolderPath, "").Replace("\\","/");
			       		path2 = (path2[0] == "/"[0] ? path2.Remove(0,1) : path2);
	    				for(int l=0; l<loadedMetas.Count(); l++){
	    					FileMetadata_3DGMA2 meta = loadedMetas[l];
	    					if(meta.path == path2){
	    						loadedMetas.RemoveAt(l);
	    					}
	    				}
	    				FileMetadata_3DGMA newMetadata = GetMetadata(file, null);
	    				if(loadedMetas2.ContainsKey(path2)){
	    					FileMetadata_3DGMA2 newMetadata2 = new FileMetadata_3DGMA2();
	    					newMetadata2.path = path2;
	    					newMetadata2.meta = newMetadata;
	    					loadedMetas2[path2] = newMetadata2;
	    				}
	        		}
	        		lastFive.Add(file);
	        		if(lastFive.Count() >= 5){
	        			SetJobText("Importing files 1/2 ("+j+" / "+modifiedFiles.Count()+"):\n\r"+lastFive[0].Name+"\n\r"+lastFive[1].Name+"\n\r"+lastFive[2].Name+"\n\r"+lastFive[3].Name+"\n\r"+lastFive[4].Name);
	        			yield return null;
	        			lastFive.Clear();
	        		}
	        	}
	        	lastFive.Clear();
	        	for(int j=0; j<modifiedFiles.Count(); j++){
	        		FileInfo file = modifiedFiles[j];
	        		FileMetadata_3DGMA metadata = GetMetadata(file, null);
	        		var matches = new List<LoadedAssetFile>();
	        		var matchIndexes = new List<int>();
	        		bool stillExists = false;
	        		for(int i=0; i<loadedAssets.Count(); i++){
	        			LoadedAssetFile asset = loadedAssets[i];
		        		if(metadata.id == asset.meta.id){
		        			matches.Add(asset);
		        			matchIndexes.Add(i);
		        			if(new FileInfo(ProjectDirectory+"/Assets/"+asset.path).Exists){
		        				stillExists = true;
		        			}
		        		}
		        	}
		        	var resetMetadata = new List<LoadedAssetFile>();
		        	if(matches.Count() > 0){
		        		if(matches.Count() > 1){
		        			bool realFileFound = false;
		        			for(int i=1; i<matches.Count(); i++){
		        				if((new FileInfo(ProjectDirectory+"/Assets/"+matches[i].path)).FullName == file.FullName){
		        					StartCoroutine(loadAsset(matches[i].path, false, GetMetadata(new FileInfo(ProjectDirectory + "/Assets/" + matches[i].path)).id));
		        					realFileFound = true;
		        				}
		        				else{
		        					resetMetadata.Add(matches[i]);
		        				}
		        			}
		        			if(realFileFound){
		        				resetMetadata.Add(matches[0]);
		        			}
		        			else{
		        				if(new FileInfo(ProjectDirectory+"/Assets/"+matches[0].path).Exists){
			        				if((new FileInfo(ProjectDirectory+"/Assets/"+matches[0].path)).FullName == file.FullName){
			        					StartCoroutine(loadAsset(matches[0].path, false, GetMetadata(new FileInfo(ProjectDirectory + "/Assets/" + matches[0].path)).id));
			        				}
			        				else{
			        					resetMetadata.Add(matches[0]);
			        				}
			        			}
			        			else{
			        				loadedAssets[matchIndexes[0]].path = file.FullName.Replace(assetsFolderPath, "").Replace("\\","/");
			        				loadedAssets[matchIndexes[0]].path = (loadedAssets[matchIndexes[0]].path[0] == "/"[0] ? loadedAssets[matchIndexes[0]].path.Remove(0,1) : loadedAssets[matchIndexes[0]].path);
			        				StartCoroutine(loadAsset(loadedAssets[matchIndexes[0]].path, false, GetMetadata(new FileInfo(ProjectDirectory + "/Assets/" + loadedAssets[matchIndexes[0]].path)).id));
			        			}
		        			}
		        		}
		        		else{
		        			if(stillExists){
		        				if((new FileInfo(ProjectDirectory+"/Assets/"+matches[0].path)).FullName == file.FullName){
		        					//loadAsset(matches[0].path, true, GetMetadata(new FileInfo(ProjectDirectory + "/Assets/" + matches[0].path)).id);
		        				}
		        				else{
		        					resetMetadata.Add(matches[0]);
		        				}
		        			}
		        			else{
		        				loadedAssets[matchIndexes[0]].path = file.FullName.Replace(assetsFolderPath, "").Replace("\\","/");
		        				loadedAssets[matchIndexes[0]].path = (loadedAssets[matchIndexes[0]].path[0] == "/"[0] ? loadedAssets[matchIndexes[0]].path.Remove(0,1) : loadedAssets[matchIndexes[0]].path);
		        				StartCoroutine(loadAsset(loadedAssets[matchIndexes[0]].path, false, GetMetadata(new FileInfo(ProjectDirectory + "/Assets/" + loadedAssets[matchIndexes[0]].path)).id));
		        			}
		        		}
		        	}
		        	foreach(LoadedAssetFile asset2 in resetMetadata){
		        		FileInfo file2 = new FileInfo(ProjectDirectory+"/Assets/"+asset2.path);
		        		FileMetadata_3DGMA metadata2 = asset2.meta;
		        		string path = file2.FullName.Replace(assetsFolderPath, "").Replace("\\","/");
		        		path = (path[0] == "/"[0] ? path.Remove(0,1) : path);
		        		int index = -1;
		        		int index2 = -1;
		        		for(int i=0; i<loadedMetas.Count(); i++){
		        			if(loadedMetas[i].meta.id == metadata2.id && loadedMetas[i].path == path){
		        				index = i;
		        			}
		        			if(loadedMetas[i].meta.id == metadata2.id && loadedMetas[i].path != path){
		        				index2 = i;
		        			}
		        		}
		        		if(index2 != -1){
		        			loadedMetas.RemoveAt(index2);
		        		}
		        		if(index != -1){
			        		File.Delete(file2.FullName + ".meta_3dgma");
	        				CreateMetadata(file2, null);
	    				}
		        	}
		        	lastFive.Add(file);
	        		if(lastFive.Count() >= 5){
	        			SetJobText("Importing files 2/2 ("+j+" / "+modifiedFiles.Count()+"):\n\r"+lastFive[0].Name+"\n\r"+lastFive[1].Name+"\n\r"+lastFive[2].Name+"\n\r"+lastFive[3].Name+"\n\r"+lastFive[4].Name);
	        			yield return null;
	        			lastFive.Clear();
	        		}
	        	}
	        	StartCoroutine(CheckAssetsFolderChanges2());
	        	yield break;
	        }
        	ClearJobText();
        }
        busyCheckingFiles = false;
	}

	public IEnumerator CheckIfObjectListNeedsUpdate(){
		yield return new WaitForSeconds(0.5f);
		if(objlistcurrent.transform.childCount != objlistcurrent_childcount){
			loadObjects2(objlistcurrent);
		}
		StartCoroutine(CheckIfObjectListNeedsUpdate());
	}

	public void startTimer(){
		DateTime dt1970 = new DateTime(1970, 1, 1);
        DateTime current = DateTime.Now;
    	TimeSpan span = current - dt1970;
    	timerStartTime = span.TotalMilliseconds;
	}

	public void stopTimer(string text){
		DateTime dt1970 = new DateTime(1970, 1, 1);
        DateTime current = DateTime.Now;
    	TimeSpan span = current - dt1970;
    	Debug.Log(text + ": " + (span.TotalMilliseconds - timerStartTime) + " ms");
	}

	public static void Mute( UnityEngine.Events.UnityEventBase ev )
	{
	    int count = ev.GetPersistentEventCount();
	    for ( int i = 0 ; i < count ; i++ )
	    {
	    	if(i != 0)
	        	ev.SetPersistentListenerState( i, UnityEngine.Events.UnityEventCallState.Off );
	    }
	}
	 
	public static void Unmute( UnityEngine.Events.UnityEventBase ev )
	{
	    int count = ev.GetPersistentEventCount();
	    for ( int i = 0 ; i < count ; i++ )
	    {
	    	if(i != 0)
	        	ev.SetPersistentListenerState( i, UnityEngine.Events.UnityEventCallState.RuntimeOnly );
	    }
	}

}

}

 public class CoroutineWithData {
     public Coroutine coroutine { get; private set; }
     public object result;
     public IEnumerator target;
     public CoroutineWithData(MonoBehaviour owner, IEnumerator target) {
         this.target = target;
         this.coroutine = owner.StartCoroutine(Run());
     }
 
     private IEnumerator Run() {
         while(target.MoveNext()) {
             result = target.Current;
             yield return result;
         }
     }
 }