using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ThreeDGMA;
using CSharpCompiler;

public class LogElement : MonoBehaviour
{
	public Controller controller;
	public Text mainText;
	public Text counter;
	public Log_3DGMA log;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindWithTag("Controller").GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(){
    	controller.logViewerText.text = log.logText + '\n' + '\n' + "Stack Trace:" + '\n' + log.stackTrace;
    	controller.logViewerType.text = "Type: " + log.type.ToString();
    	controller.logViewer.GetComponent<Image>().enabled = true;
    	if(log.type == LogType.Exception){
    		controller.logViewer.GetComponent<Image>().color = new Color(1, 0, 0, 0.0784f);
    	}
    	else if(log.type == LogType.Warning){
    		controller.logViewer.GetComponent<Image>().color = new Color(1, 1, 0, 0.0784f);
    	}
    	else{
    		controller.logViewer.GetComponent<Image>().enabled = false;
    	}
    	controller.logViewerText.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    	controller.logViewer.SetActive(true);
    	controller.logConsole.SetActive(false);
    }
}
