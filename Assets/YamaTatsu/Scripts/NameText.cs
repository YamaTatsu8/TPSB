using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameText : MonoBehaviour {

    Text myText;

	// Use this for initialization
	void Start () {

        myText = GetComponentInChildren<Text>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setText(string text)
    {
        myText.text = text.ToString();
    }

}
