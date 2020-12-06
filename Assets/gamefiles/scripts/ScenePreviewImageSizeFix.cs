using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenePreviewImageSizeFix : MonoBehaviour
{
	RectTransform thisTransform;
	RawImage imagePreview;
	float lastAspect = -50f;

    // Start is called before the first frame update
    void Start()
    {
    	thisTransform = GetComponent<RectTransform>();
    	imagePreview = GetComponent<RawImage>();
        StartCoroutine(Check());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Check(){
    	float aspect = thisTransform.rect.width / thisTransform.rect.height;
    	if(aspect != lastAspect){
	    	float aspect2 = thisTransform.rect.height / thisTransform.rect.width;
	    	Rect newRect = new Rect();
	    	if(aspect < 1){
	    		newRect.width = aspect;
	    		newRect.height = 1;
	    		newRect.x = (1 - aspect) / 2;
	    		newRect.y = 0;
	    	}
	    	else if(aspect > 1){
	    		newRect.width = 1;
	    		newRect.height = aspect2;
	    		newRect.x = 0;
	    		newRect.y = (1 - aspect2) / 2;
	    	}
	    	else{
	    		newRect.width = 1;
	    		newRect.height = 1;
	    		newRect.x = 0;
	    		newRect.y = 0;
	    	}
	    	imagePreview.uvRect = newRect;
	    	lastAspect = aspect;
    	}
    	yield return new WaitForSeconds(1f);
    	StartCoroutine(Check());
    }
}
