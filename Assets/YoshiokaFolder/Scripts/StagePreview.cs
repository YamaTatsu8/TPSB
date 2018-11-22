using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePreview : MonoBehaviour
{
    [SerializeField]
    float _speed = 1.0f;

    GameObject _stage;   //　ステージObj


    // Use this for initialization
    void Start()
    {
        Initialize();
	}

    public void Initialize()
    {
        SetStage("Stage1");
    }
	
	// Update is called once per frame
	void Update ()
    {
        StageUpdate();
    }

    public void StageUpdate()
    {
        Quaternion rot = _stage.transform.rotation;
        rot.y += _speed;
        _stage.transform.localRotation = rot;
    }

    public void SetStage(string stageName)
    {
        GameObject obj = (GameObject)Instantiate(Resources.Load("Prefabs/Stages/" + stageName));
        _stage = obj;
    }
}
