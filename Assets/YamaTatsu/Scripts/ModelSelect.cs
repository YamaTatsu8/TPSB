using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSelect : MonoBehaviour {

    //PlayerSystemオブジェクト
    private GameObject _playerSystem;

    //モデルリスト
    [SerializeField]
    private GameObject[] _model;

    //モデル名前
    private string _modelName;

	// Use this for initialization
	void Start () {

        //PlayerSystemを探す
        _playerSystem = GameObject.Find("PlayerSystem");

        _modelName = _playerSystem.GetComponent<PlayerSystem>().getChar();

        for(int i = 0; i < _model.Length; i++)
        {
            if(_model[i].name == _modelName)
            {
                _model[i].SetActive(true);
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //名前をセットする
    public void SetModel(string name)
    {
        for (int i = 0; i < _model.Length; i++)
        {
            if (_model[i].name == _modelName)
            {
                _model[i].SetActive(false);
            }
        }

        _modelName = name;
        //名前と一致するモデルを探しtrueにする
        for (int i = 0; i < _model.Length; i++)
        {
            if (_model[i].name == _modelName)
            {
                _model[i].SetActive(true);
            }
        }
    }

}
