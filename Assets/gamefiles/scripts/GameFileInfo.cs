using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFileInfo : MonoBehaviour
{

	public string type;

	[System.Serializable]
	public class material_props
	{
		public Color color;
	}

	public material_props material = new material_props();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
