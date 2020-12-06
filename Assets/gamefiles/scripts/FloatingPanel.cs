using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MaterialUI;

public class FloatingPanel : MonoBehaviour
{
	bool anim1 = false;
	bool anim2 = false;
	RectTransform panel;
	public Image background;
	float animStartTime;
	float animDeltaTime;
	public bool isPanelOpen = false;
	public float speed = 0.2f;
	public float baseScale = 1f;

    // Start is called before the first frame update
    void Start()
    {
        panel = GetComponent<RectTransform>();
        background.color = new Color(0, 0, 0, 0);
        panel.GetComponent<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        animDeltaTime = Time.realtimeSinceStartup - animStartTime;
        if(anim1){
	        if(animDeltaTime <= speed){
	        	var scale = Anim.Quint.SoftOut(baseScale * 0.8f, baseScale, animDeltaTime, speed);
				panel.localScale = new Vector2(scale, scale);
				panel.GetComponent<CanvasGroup>().alpha = Anim.Quint.SoftOut(0f, 1f, animDeltaTime, speed);
			}
			else{
				panel.localScale = new Vector2(baseScale, baseScale);
				panel.GetComponent<CanvasGroup>().alpha = 1f;
				anim1 = false;
			}
			background.color = new Color(0, 0, 0, panel.GetComponent<CanvasGroup>().alpha * 0.572f);
		}
		if(anim2){
	        if(animDeltaTime <= speed){
	        	var scale = Anim.Quint.SoftOut(baseScale, baseScale * 0.8f, animDeltaTime, speed);
				panel.localScale = new Vector2(scale, scale);
				panel.GetComponent<CanvasGroup>().alpha = Anim.Quint.SoftOut(1f, 0f, animDeltaTime, speed);
			}
			else{
				panel.localScale = new Vector2(baseScale * 0.8f, 0);
				panel.GetComponent<CanvasGroup>().alpha = 0f;
				anim2 = false;
				panel.parent.gameObject.SetActive(false);
			}
			background.color = new Color(0, 0, 0, panel.GetComponent<CanvasGroup>().alpha * 0.572f);
		}
    }

    public void OpenClose(){
    	panel = GetComponent<RectTransform>();
    	if(!anim1 && !anim2){
	    	if(isPanelOpen){
	    		animStartTime = Time.realtimeSinceStartup;
	    		anim2 = true;
	    	}
	    	else{
	    		animStartTime = Time.realtimeSinceStartup;
	    		anim1 = true;
	    		panel.parent.gameObject.SetActive(true);
	    	}
	    	isPanelOpen = !isPanelOpen;
	    }
    }

    public void Open(bool forced = false){
    	panel = GetComponent<RectTransform>();
    	if((!anim1 && !anim2) || forced){
	    	if(!isPanelOpen || forced){
	    		animStartTime = Time.realtimeSinceStartup;
	    		anim1 = true;
	    		anim2 = false;
	    		panel.parent.gameObject.SetActive(true);
	    		isPanelOpen = true;
	    	}
	    }
    }

    public void Close(bool forced = false){
    	panel = GetComponent<RectTransform>();
    	if((!anim1 && !anim2) || forced){
	    	if(isPanelOpen || forced){
	    		animStartTime = Time.realtimeSinceStartup;
	    		anim2 = true;
	    		anim1 = false;
	    		isPanelOpen = false;
	    	}
	    }
    }
}
