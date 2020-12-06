using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System;
using System.IO;
using System.Linq;
using CSharpCompiler;
using RuntimeGizmos;

public class ForTesting : MonoBehaviour
{
    public Material materialTemplate;

    // Start is called before the first frame update
    void Start()
    {
        Material newMat = new Material(materialTemplate);
        GetComponent<MeshRenderer>().material = newMat;
        newMat.SetFloat("_Glossiness", 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
