using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using MaterialUI;

[System.Serializable]
public class CustomDropdownItem
{
	public string text = "";
	public UnityEvent onClick;
}

public class CustomDropdown : MonoBehaviour
{
	public Text openCloseButtonText;
	public string openMenuText = "Open";
	public string closeMenuText = "Close";
    public bool closeOnElementSelected = false;
    public bool showSelectedElementAsDropdownText = false;
	public GameObject menu;
	public Transform listContent;
	public GameObject templateItem;
	public Text templateItemText;
	public Button templateItemButton;
    public float finalHeight = 170f;
    float animStartTime;
    float animDeltaTime;
    bool openMenuAnim = false;
    bool closeMenuAnim = false;
	[SerializeField]
	public CustomDropdownItem[] items = new CustomDropdownItem[1];

	bool isMenuOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        openCloseButtonText.text = openMenuText;
        foreach(var item in items)
        {
        	templateItemText.text = item.text;
        	var clone = Instantiate(templateItem, listContent);
        	clone.GetComponent<Button>().onClick.AddListener(() => item.onClick.Invoke());
            if(closeOnElementSelected)
                clone.GetComponent<Button>().onClick.AddListener(() => OpenCloseMenu());
            if(showSelectedElementAsDropdownText)
                clone.GetComponent<Button>().onClick.AddListener(() => openCloseButtonText.text = item.text);
        }
        templateItem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        animDeltaTime = Time.realtimeSinceStartup - animStartTime;

        if(openMenuAnim)
        {
            if(animDeltaTime <= 0.3f)
            {
                menu.GetComponent<RectTransform>().sizeDelta = new Vector2(menu.GetComponent<RectTransform>().sizeDelta.x, Anim.Quint.InOut(0f, finalHeight, animDeltaTime, 0.3f));
                
            }
            else{
                menu.GetComponent<RectTransform>().sizeDelta = new Vector2(menu.GetComponent<RectTransform>().sizeDelta.x, finalHeight);
                openMenuAnim = false;
            }
        }

        if(closeMenuAnim)
        {
            if(animDeltaTime <= 0.1f)
            {
                menu.GetComponent<RectTransform>().sizeDelta = new Vector2(menu.GetComponent<RectTransform>().sizeDelta.x, Anim.Quint.InOut(finalHeight, 0f, animDeltaTime, 0.1f));
                
            }
            else{
                menu.GetComponent<RectTransform>().sizeDelta = new Vector2(menu.GetComponent<RectTransform>().sizeDelta.x, 0f);
                menu.SetActive(false);
                closeMenuAnim = false;
            }
        }
    }

    public void OpenCloseMenu()
    {
        animStartTime = Time.realtimeSinceStartup;
    	if(isMenuOpen){
    		isMenuOpen = false;
            if(!showSelectedElementAsDropdownText)
    		  openCloseButtonText.text = openMenuText;
            openMenuAnim = false;
            closeMenuAnim = true;
    	}
    	else{
    		menu.SetActive(true);
    		isMenuOpen = true;
            if(!showSelectedElementAsDropdownText)
    		  openCloseButtonText.text = closeMenuText;
            openMenuAnim = true;
            closeMenuAnim = false;
    	}
    }

}
