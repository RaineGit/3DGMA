using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSharpCompiler;

namespace game
{

public class gm : MonoBehaviour
{

	public static GameObject getObjectById(string id){
		List<GameObject> all = GameObject.FindWithTag("Controller").GetComponent<Controller>().GetAllGameObjects(GameObject.FindWithTag("Controller").GetComponent<Controller>().scene);
		foreach(GameObject child in all){
			if(child.GetComponent<ObjectController>()!=null && child.GetComponent<ObjectController>().id == id){
				return child;
			}
		}
		return null;
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

}
