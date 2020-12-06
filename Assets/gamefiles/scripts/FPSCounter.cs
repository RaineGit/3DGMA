using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
	public Text textComponent;
	int frameCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplayFPS());
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
    }

    IEnumerator DisplayFPS(){
    	yield return new WaitForSeconds(0.5f);
    	textComponent.text = (frameCount*2).ToString() + " FPS";
    	frameCount = 0;
    	StartCoroutine(DisplayFPS());
    }
}
