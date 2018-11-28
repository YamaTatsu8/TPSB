using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{ 
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        GameObject obj = GameObject.Find("SceneManagerObject");
        StageSelectManager test = obj.GetComponent<SceneObserver>().GetStageSelectSceneData();
        Debug.Log(test.GetSelectStageName());
	}
}
