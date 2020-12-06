using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using MaterialUI;
using System.IO;
using ThreeDGMA;
using System;
using System.Linq;
using System.Threading;
using System.Globalization;

public class MainMenu : MonoBehaviour
{
	public RectTransform settingsPanel;
	public MenuArrowAnim spinnyArrow;
	public RectTransform newProjectPanel;
	public InputField newProjectName;
	public ToggleGroup newProjectType;
	public RectTransform projectsScrollContent;
	public GameObject projectElement;
	public GameObject languagesPanel;
	public GameObject deletingPanel;
	public Text deletingPanelSumText;
	public InputField deletingPanelSumResult;
	public Text deletingPanelReminder;
	public GameObject errorPanel;
	public Text errorText;
	public GameObject bigErrorPanel;
	public GameObject comingSoonPanel;
	public GameObject loadingPanel;
	public string AppDirectory = "/sdcard/3DGMA";
	List<ThreeDGMA.Project_3DGMA> projects = new List<ThreeDGMA.Project_3DGMA>();
	bool settingsOpen = false;
	bool newProjectPanelOpen = false;
	public string lang = "en";
	DirectoryInfo projectDeleting;
	int sumResult = 0;
	bool anim1 = false;
	bool anim2 = false;
	float animStartTime;
	float animDeltaTime;

	void Awake()
	{
        LanguageModForTextComponent[] components = Resources.FindObjectsOfTypeAll<LanguageModForTextComponent>();
    	foreach(var comp in components)
    	{
    		comp.englishText = comp.gameObject.GetComponent<Text>().text;
    		comp.UpdateText();
    	}
		if(!PlayerPrefs.HasKey("lang"))
    	{
    		UpdateLang("en");
    		languagesPanel.SetActive(true);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
    	#if PLATFORM_ANDROID
        if(!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead)){
            AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.READ_EXTERNAL_STORAGE");
        }
        if(!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite)){
            AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.WRITE_EXTERNAL_STORAGE");
        }
        #endif
        if(Application.isEditor){
			AppDirectory = "C:/Users/mcleo/Desktop/3DGMA";
			if(!(new DirectoryInfo(AppDirectory).Exists)){
				AppDirectory = "/home/mcleocito/Desktop/3DGMA";
			}
		}
		if(PlayerPrefs.HasKey("lang"))
    	{
    		UpdateLang(PlayerPrefs.GetString("lang"));
    	}
		RefreshProjects();
    }

    // Update is called once per frame
    void Update()
    {
        animDeltaTime = Time.realtimeSinceStartup - animStartTime;

        if(anim1){
	        if(animDeltaTime <= 0.3f)
				settingsPanel.anchoredPosition = new Vector2(Anim.Quint.InOut(720f, 0f, animDeltaTime, 0.3f), 0);
			else{
				settingsPanel.anchoredPosition = new Vector2(0, 0);
				anim1 = false;
			}
		}
		if(anim2){
	        if(animDeltaTime <= 0.3f)
				settingsPanel.anchoredPosition = new Vector2(Anim.Quint.InOut(0f, 720f, animDeltaTime, 0.3f), 0);
			else{
				settingsPanel.anchoredPosition = new Vector2(720, 0);
				anim2 = false;
			}
		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(settingsOpen)
				OpenCloseSettings();
			else if(errorPanel.GetComponent<FloatingPanel>().isPanelOpen)
				errorPanel.GetComponent<FloatingPanel>().OpenClose();
			else if(comingSoonPanel.GetComponent<FloatingPanel>().isPanelOpen)
				comingSoonPanel.GetComponent<FloatingPanel>().OpenClose();
			else if(newProjectPanel.GetComponent<FloatingPanel>().isPanelOpen)
				newProjectPanel.GetComponent<FloatingPanel>().OpenClose();
			else if(deletingPanel.GetComponent<FloatingPanel>().isPanelOpen)
				deletingPanel.GetComponent<FloatingPanel>().OpenClose();
			else if(languagesPanel.activeSelf)
				languagesPanel.SetActive(false);
			else
				CloseApp();
		}
    }

    public void UpdateLang(string newLang)
    {
    	PlayerPrefs.SetString("lang", newLang);
    	if(newLang == "es")
			Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES", true);
		else if(newLang == "cat")
			Thread.CurrentThread.CurrentCulture = new CultureInfo("ca-ES-valencia", true);
		else if(newLang == "jp")
			Thread.CurrentThread.CurrentCulture = new CultureInfo("ja", true);
		else if(newLang == "br")
			Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR", true);
		else if(newLang == "ru")
			Thread.CurrentThread.CurrentCulture = new CultureInfo("ru", true);
		else
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB", true);
		lang = newLang;
    }

    public void UpdateAllText2()
    {
    	UpdateAllText();
    }

    public static void UpdateAllText()
    {
    	LanguageModForTextComponent[] components = Resources.FindObjectsOfTypeAll<LanguageModForTextComponent>();
    	foreach(var comp in components)
    	{
    		comp.UpdateText();
    	}
    }

    public void OpenCloseSettings()
    {
    	if(!anim1 && !anim2){
    		spinnyArrow.Toggle();
	    	if(settingsOpen){
	    		animStartTime = Time.realtimeSinceStartup;
	    		anim2 = true;
	    	}
	    	else{
	    		animStartTime = Time.realtimeSinceStartup;
	    		anim1 = true;
	    	}
	    	settingsOpen = !settingsOpen;
	    }
    }

    public void OpenCloseNewProjectPanel()
    {
    	newProjectPanel.GetComponent<FloatingPanel>().OpenClose();
    }

    public void RefreshProjects()
    {
    	projects.Clear();
    	DirectoryInfo[] dirs = new DirectoryInfo[0];
    	try{
	    	if(!Directory.Exists(AppDirectory))  
	    		Directory.CreateDirectory(AppDirectory);
	    	if(!Directory.Exists(AppDirectory + "/Projects"))  
	    		Directory.CreateDirectory(AppDirectory + "/Projects");
	    	dirs = new DirectoryInfo(AppDirectory + "/Projects").GetDirectories();
	    }
	    catch(Exception err)
	    {
	    	bigErrorPanel.SetActive(true);
	    	Debug.LogException(err);
	    }
		foreach(Transform child in projectsScrollContent){
			Destroy(child.gameObject);
		}
		foreach(DirectoryInfo dir in dirs){
			Project_3DGMA project = new Project_3DGMA();
			project.path = dir.FullName;
			project.lastTimeOpened = dir.LastWriteTime;
			try{
				StreamReader sr = new StreamReader(dir.FullName + "/info.json");
				ProjectInfo_3DGMA info = new ProjectInfo_3DGMA();
				info = JsonUtility.FromJson<ProjectInfo_3DGMA>(sr.ReadToEnd());
				if(info.lastTimeOpened != "")
					project.lastTimeOpened = DateTime.Parse(info.lastTimeOpened);
				else
					project.lastTimeOpened = new DateTime(2000, 1, 1);
				sr.Close();
			}
			catch{}
			projects.Add(project);
		}
		projects = projects.OrderByDescending(o => (o.lastTimeOpened != new DateTime(2000, 1, 1) ? o.lastTimeOpened : DateTime.Now)).ToList();
		for(var i = 0; i < projects.Count; i++){
			Project_3DGMA project = projects[i];
			var dir = new DirectoryInfo(project.path);
			var clone = Instantiate(projectElement, projectsScrollContent);
			clone.GetComponent<ProjectElement>().title.text = dir.Name;
			string tmpText = "Last time opened: " + (project.lastTimeOpened != new DateTime(2000, 1, 1) ? project.lastTimeOpened.ToString() : "Never");
			if(lang == "es")
				tmpText = "Última vez abierto: " + (project.lastTimeOpened != new DateTime(2000, 1, 1) ? project.lastTimeOpened.ToString() : "Nunca");
			else if(lang == "cat")
				tmpText = "Última vegada obert: " + (project.lastTimeOpened != new DateTime(2000, 1, 1) ? project.lastTimeOpened.ToString() : "Mai");
			else if(lang == "jp")
				tmpText = "前回オープン： " + (project.lastTimeOpened != new DateTime(2000, 1, 1) ? project.lastTimeOpened.ToString() : "決して");
			else if(lang == "br")
				tmpText = "Aberto pela última vez: " + (project.lastTimeOpened != new DateTime(2000, 1, 1) ? project.lastTimeOpened.ToString() : "Nunca");
			else if(lang == "ru")
				tmpText = "Последний раз открывали: " + (project.lastTimeOpened != new DateTime(2000, 1, 1) ? project.lastTimeOpened.ToString() : "Никогда");
			clone.GetComponent<ProjectElement>().lastSave.text = tmpText;
			clone.GetComponent<ProjectElement>().path = dir.FullName;
			clone.GetComponent<ProjectElement>().deleteButton.onClick.AddListener(() => OpenDeletingPanel(dir));
			clone.GetComponent<ProjectElement>().loadingPanel = loadingPanel;
		}
    }

    public void CreateNewProject()
    {
    	try{
	    	if(newProjectName.text.IndexOfAny(Path.GetInvalidFileNameChars()) < 0 && newProjectName.text.IndexOf('\\') < 0 && newProjectName.text != ".." && newProjectName.text != "." && newProjectName.text.Length > 0)
	    	{
	    		var dir = new DirectoryInfo(AppDirectory + "/Projects/" + newProjectName.text);
	    		if(dir.Exists)
	    		{
	    			errorText.text = "A project with that name already exists";
	    			if(lang == "es")
						errorText.text = "Ya existe un proyecto con ese nombre";
					else if(lang == "cat")
						errorText.text = "Ja existeix un projecte amb aqueix nom";
					else if(lang == "jp")
						errorText.text = "その名前のプロジェクトはすでに存在します";
					else if(lang == "br")
						errorText.text = "Já existe um projeto com esse nome";
					else if(lang == "ru")
						errorText.text = "Проект с таким названием уже существует";
	    			errorPanel.GetComponent<FloatingPanel>().OpenClose();
	    		}
	    		else{
	    			Directory.CreateDirectory(dir.FullName);
	    			ProjectInfo_3DGMA info = new ProjectInfo_3DGMA();
			        info.lastTimeOpened = null;
			        StreamWriter sw = File.CreateText(dir.FullName + "/info.json");
					sw.Write(JsonUtility.ToJson(info));
					sw.Close();
					newProjectPanel.GetComponent<FloatingPanel>().OpenClose();
					RefreshProjects();
					newProjectName.text = "";
	    		}
	    	}
	    	else
	    	{
	    		errorText.text = "Invalid name, try to remove special characters that might not be valid; if the error still appears, try shortening the name";
	    		if(lang == "es")
					errorText.text = "Nombre no válido, intente eliminar los caracteres especiales que podrían no ser válidos; si el error sigue apareciendo, intente acortar el nombre";
				else if(lang == "cat")
					errorText.text = "Nom no vàlid, intente eliminar els caràcters especials que podrien no ser vàlids; si l'error continua apareixent, intente escurçar el nom";
				else if(lang == "jp")
					errorText.text = "名前が無効です。無効である可能性のある特殊文字を削除してみてください。 それでもエラーが表示される場合は、名前を短くしてみてください";
				else if(lang == "br")
					errorText.text = "Nome inválido, tente remover caracteres especiais que podem não ser válidos; se o erro ainda aparecer, tente encurtar o nome";
				else if(lang == "ru")
					errorText.text = "Недействительное имя, попробуйте удалить специальные символы, которые могут быть недопустимыми; если ошибка все еще появляется, попробуйте сократить имя";
	    		errorPanel.GetComponent<FloatingPanel>().OpenClose();
	    	}
	    }
	    catch
	    {
	    	errorText.text = "An unknown error occurred";
	    	if(lang == "es")
				errorText.text = "Ha ocurrido un error desconocido";
			else if(lang == "cat")
				errorText.text = "Ha ocorregut un error desconegut";
			else if(lang == "jp")
				errorText.text = "不明なエラーが発生しました";
			else if(lang == "br")
				errorText.text = "Ocorreu um erro desconhecido";
			else if(lang == "ru")
				errorText.text = "Произошла неизвестная ошибка";
	    	errorPanel.GetComponent<FloatingPanel>().OpenClose();
	    }
    }

    public void OpenDeletingPanel(DirectoryInfo dir)
    {
    	int num1 = UnityEngine.Random.Range(1, 10);
    	int num2 = UnityEngine.Random.Range(1, 10);
    	sumResult = num1 + num2;
    	projectDeleting = dir;
    	deletingPanelSumText.text = num1 + " + " + num2 + " =";
    	deletingPanelReminder.text = "You are about to delete <b>" + dir.Name + "</b>";
    	if(lang == "es")
				deletingPanelReminder.text = "Está a punto de eliminar <b>" + dir.Name + "</b>";
			else if(lang == "cat")
				deletingPanelReminder.text = "Està a punt d'eliminar <b>" + dir.Name + "</b>";
			else if(lang == "jp")
				deletingPanelReminder.text = "あなたは<b>" + dir.Name + "</b>を削除しようとしています";
			else if(lang == "br")
				deletingPanelReminder.text = "Você está prestes a excluir <b>" + dir.Name + "</b>";
			else if(lang == "ru")
				deletingPanelReminder.text = "Вы собираетесь удалить <b>" + dir.Name + "</b>";
    	deletingPanel.GetComponent<FloatingPanel>().OpenClose();
    }

    public void DeleteProject()
    {
    	try{
	    	int answer = -1;
	    	int.TryParse(deletingPanelSumResult.text, out answer);
	    	if(answer == sumResult)
	    	{
	    		deletingPanel.GetComponent<FloatingPanel>().OpenClose();
	    		projectDeleting.Delete(true);
	    		RefreshProjects();
	    		deletingPanelSumResult.text = "";
	    	}
	    }
	    catch
	    {
	    	errorText.text = "An unknown error occurred";
	    	if(lang == "es")
				errorText.text = "Ha ocurrido un error desconocido";
			else if(lang == "cat")
				errorText.text = "Ha ocorregut un error desconegut";
			else if(lang == "jp")
				errorText.text = "不明なエラーが発生しました";
			else if(lang == "br")
				errorText.text = "Ocorreu um erro desconhecido";
			else if(lang == "ru")
				errorText.text = "Произошла неизвестная ошибка";
	    	errorPanel.GetComponent<FloatingPanel>().OpenClose();
	    }
    }

    public void OpenURL(string url)
    {
    	Application.OpenURL(url);
    }

    public void CloseApp()
    {
    	Application.Quit();
    }
}
