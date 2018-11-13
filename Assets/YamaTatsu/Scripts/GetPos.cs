using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPos : MonoBehaviour {

    [SerializeField]
    private GameObject obj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(obj.GetComponent<Attack>().getPosition());

	}
}
