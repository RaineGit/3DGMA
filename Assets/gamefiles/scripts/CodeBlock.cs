using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeBlock : MonoBehaviour
{

	public string type = "";
    public bool input = true;
    public bool output = true;
    public bool deletable = true;
    public Image inputImage;
    public Image outputImage;
	public RectTransform mainContainer;
	public CodeBlock parent;
	public Transform content;
	public RectTransform arm;
	public RectTransform head;
	public RectTransform bottom;
    public RectTransform trashRect;

    // Start is called before the first frame update
    void Start()
    {
        if(!input){
            inputImage.sprite = null;
        }
        if(!output){
            outputImage.gameObject.SetActive(false);
        }
    	try{
    		parent = transform.parent.parent.gameObject.GetComponent<CodeBlock>();
    	}
    	catch{}
    	if(type == "keys"){
        	UpdateSize();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartDragging(){
    	Vector3 oldPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    	transform.parent = mainContainer;
    	transform.position = oldPosition;
    	transform.SetAsLastSibling();
    }

    public void DetachOrAttach(){
        if(deletable && rectOverlaps(trashRect, Input.mousePosition)){
            Destroy(gameObject);
        }
        if(input){
        	CodeBlock collidingBlock = GetCollidingBlock(GetComponent<RectTransform>(), mainContainer.GetComponent<RectTransform>());
        	if(collidingBlock != null && (collidingBlock.type == "keys" || collidingBlock.parent != null)){
        		CodeBlock newParent = (collidingBlock.type == "keys" ? collidingBlock : collidingBlock.parent);
        		transform.parent = newParent.content;
        		if(parent != null){
        			parent.UpdateSize();
        		}
        		parent = newParent;
        		transform.SetSiblingIndex(collidingBlock.type != "keys" ? collidingBlock.transform.GetSiblingIndex()+1 : 0);
        		parent.UpdateSize();
        	}
        	else{
        		if(parent != null){
    	    		transform.parent = mainContainer;
    	    		parent.UpdateSize();
    	    		parent = null;
    	    	}
        	}
        }
    }

    public CodeBlock GetCollidingBlock(RectTransform block, RectTransform container){
    	foreach(RectTransform child in container){
    		if(child.GetComponent<CodeBlock>() != null && rectOverlaps(child, Input.mousePosition) && block != child){
    			if(child.GetComponent<CodeBlock>().type == "keys"){
    				var result = GetCollidingBlock(block, child.GetComponent<CodeBlock>().content.GetComponent<RectTransform>());
    				return result == null ? child.GetComponent<CodeBlock>() : result;
    			}
    			else{
    				return child.GetComponent<CodeBlock>();
    			}
    		}
    	}
    	return null;
    }

    bool rectOverlaps(RectTransform rectTrans1, Vector2 position)
	{
		Vector2 blockSize = new Vector2(rectTrans1.rect.width * (Screen.width / mainContainer.rect.width), rectTrans1.rect.height * (Screen.height / mainContainer.rect.height));
	    return Input.mousePosition.x > rectTrans1.gameObject.transform.position.x && Input.mousePosition.x < (rectTrans1.gameObject.transform.position.x + blockSize.x) && Input.mousePosition.y < rectTrans1.gameObject.transform.position.y && Input.mousePosition.y > (rectTrans1.gameObject.transform.position.y - blockSize.y);
	}

    public void UpdateSize(){
    	float totalHeight = 0f;
    	float totalWidth = 0f;
    	Transform biggestChild;
    	float biggestChildWidth = -99999f;
    	foreach(Transform child in content){
    		child.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -totalHeight);
    		totalHeight += child.GetComponent<RectTransform>().rect.height + 5f;
    		Vector2 blockSize = new Vector2(child.GetComponent<RectTransform>().rect.width, child.GetComponent<RectTransform>().rect.height);
    		if(biggestChildWidth == -99999f || blockSize.x + content.localPosition.x > biggestChildWidth){
    			biggestChild = child;
    			biggestChildWidth = blockSize.x + content.localPosition.x;
    		}
    	}
    	if(totalHeight == 0f){
    		totalHeight = 10f;
    	}
    	if(biggestChildWidth == -99999f){
    		biggestChildWidth = content.GetComponent<RectTransform>().rect.width;
    	}
    	totalWidth = biggestChildWidth;
    	arm.offsetMin = new Vector2(arm.offsetMin.x, -totalHeight - 55);
    	gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(biggestChildWidth, head.sizeDelta.y + arm.sizeDelta.y + bottom.sizeDelta.y);
    	if(parent){
    		parent.UpdateSize();
    	}
    }
}
