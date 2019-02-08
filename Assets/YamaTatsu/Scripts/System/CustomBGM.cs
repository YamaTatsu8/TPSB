﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBGM : MonoBehaviour {

    //流したい曲をセット
    [SerializeField]
    private string _bgmName;

    private AudioManager _audio;

	// Use this for initialization
	void Start () {

        _audio = AudioManager.Instance;

        _audio.PlayBGM(_bgmName);

    }
	
	// Update is called once per frame
	void Update () {

        
    }
}
