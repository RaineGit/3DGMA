using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Gui;

public class VP_Controller : MonoBehaviour
{
	public Transform container;
	public RectTransform canvas;
	public RectTransform trashRect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate(GameObject prefab)
    {
    	var clone = Instantiate(prefab, container);
    	clone.GetComponent<CodeBlock>().mainContainer = container.GetComponent<RectTransform>();
    	clone.GetComponent<CodeBlock>().trashRect = trashRect;
    	clone.GetComponent<LeanDrag>().Canvas = canvas;
    }
}
