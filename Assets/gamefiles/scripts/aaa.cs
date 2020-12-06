using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaa : MonoBehaviour
{
    void Start(){
Debug.Log("Working");
}

void OnCollisionEnter(Collision col){
Debug.Log("Collided");
if(col.gameObject.tag=="Object1"){
Destroy(col.gameObject);
}
}
}
