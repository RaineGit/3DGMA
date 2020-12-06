using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ThreeDGMA;
using System.IO;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ProjectElement : MonoBehaviour, IPointerClickHandler
{
	public Text title;
	public Text lastSave;
    public Button deleteButton;
	public string path;
    public GameObject loadingPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    	Open();
    }

    public void Open()
    {
    	ProjectInfo_3DGMA info = new ProjectInfo_3DGMA();
    	try{
	        StreamReader sr = new StreamReader(path + "/info.json");
	    	info = JsonUtility.FromJson<ProjectInfo_3DGMA>(sr.ReadToEnd());
	        sr.Close();
	    }
	    catch{}
        info.lastTimeOpened = DateTime.Now.ToString();
        StreamWriter sw = File.CreateText(path + "/info.json");
		sw.Write(JsonUtility.ToJson(info));
		sw.Close();
        PlayerPrefs.SetString("currentProjectPath", path);
        StartCoroutine(LoadEditor());
    }

    IEnumerator LoadEditor()
    {
        loadingPanel.SetActive(true);
        yield return null;
        //yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync("Test");
    }
}
