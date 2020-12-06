using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using RuntimeGizmos;
using CSharpCompiler;
using ThreeDGMA;
using System.Linq;

public class FakeScriptValue
{
    public bool isField = true;
    public Type type;
    public object val;
    public bool isActuallyAList = false;

    public FakeScriptValue Clone(){
        return (FakeScriptValue)this.MemberwiseClone();
    }
}

public class FakeComponent : MonoBehaviour
{

	public string ComponentName = "";
	public string ComponentType = "";
	public string ComponentType2 = "";
    public Hashtable fakeValues = new Hashtable();
	public object fakeValue;
	public AddComponentButton returnTo;
    public string scriptPath;
    public LoadedAssetFile loadedScriptAsset;
	Controller controller;
	Rigidbody emptyRigidbody;
	GameObject samplesObject;
    public string realFakeComponentId;

    // Start is called before the first frame update
    void Start()
    {
    	
    }

    public void CreateFakeComponent(){
        controller = GameObject.FindWithTag("Controller").GetComponent<Controller>();
        if(ComponentType2 == "UnityEngine.Rigidbody"){
        	emptyRigidbody = controller.emptyRigidbody;
        	samplesObject = controller.samplesObject;
    		GameObject tempGO = new GameObject("FakeRigidbody");
    		tempGO.transform.SetParent(samplesObject.transform);
    		tempGO.SetActive(false);
            fakeValue = tempGO.AddComponent<Rigidbody>();
        }
        if(ComponentType2 == "C# Script"){
            UpdateFakeValues();
        }
        realFakeComponentId = this.GetInstanceID().ToString();
    }

    public void UpdateFakeValues(){
        var tempInstance = Activator.CreateInstance(loadedScriptAsset.loadedType);
        var fields = loadedScriptAsset.loadedType.GetFields();
        foreach(var field in fields){
            var tempScriptValue = new FakeScriptValue();
                tempScriptValue.isField = true;
                if(!(field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))){
                    tempScriptValue.type = field.FieldType;
                    tempScriptValue.val = field.GetValue(tempInstance);
                }
                else{
                    tempScriptValue.type = field.FieldType.GetGenericArguments().FirstOrDefault().MakeArrayType();
                    tempScriptValue.val = ((IEnumerable)field.GetValue(tempInstance)).Cast<object>().ToArray();
                    tempScriptValue.isActuallyAList = true;
                }

            if(!fakeValues.ContainsKey(field.Name)){
                fakeValues.Add(field.Name, tempScriptValue);
            }
            else{
                if(((FakeScriptValue)fakeValues[field.Name]).type != field.FieldType){
                    fakeValues[field.Name] = tempScriptValue;
                }
            }
        }
        var values = fakeValues;
        List<string> keys = new List<string>();
        foreach(string key in values.Keys){
            keys.Add(key);
        }
        foreach(string key in keys){
            if(loadedScriptAsset.loadedType.GetField(key) == null){
                fakeValues.Remove(key);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy(){
        if(ComponentType2 != "C# Script" && fakeValue != null){
            Destroy(((Component)fakeValue).gameObject);
        }
    }

}

public static class ExtensionMethods
{
    // Deep clone
    public static T DeepClone<T>(this T a)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, a);
            stream.Position = 0;
            return (T) formatter.Deserialize(stream);
        }
    }
}
