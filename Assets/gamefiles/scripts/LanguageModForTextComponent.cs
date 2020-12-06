using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ThreeDGMA;

namespace ThreeDGMA
{
	[System.Serializable]
	public class LangTranslation
	{
		public string language;
		public string translation;
	}
}

public class LanguageModForTextComponent : MonoBehaviour
{
	public List<LangTranslation> translations = new List<LangTranslation>();
    public string englishText = "";
	string lang = "en";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText()
    {
    	if(PlayerPrefs.HasKey("lang"))
    	{
    		lang = PlayerPrefs.GetString("lang");
            if(lang == "en")
                GetComponent<Text>().text = englishText;
            else
            {
        		foreach(var child in translations)
        		{
        			if(child.language == lang)
        			{
        				GetComponent<Text>().text = child.translation;
        			}
        		}
            }
    	}
    }
}
