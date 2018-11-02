using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquipment : MonoBehaviour {


    //メイン武器1
    private GameObject _weapon1;

    //メイン武器2
    private GameObject _weapon2;

    //武器の名前1
    private string _weaponName1;

    //武器の名前2
    private string _weaponName2;

    //右手
    [SerializeField]
    private GameObject _rightHand;

    //PlayerSystemのスクリプト
    private PlayerSystem _playerSystem;


	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //武器1にセットする
    public void setWeapon1(string name)
    {
        _weaponName1 = name;

        Debug.Log(_weaponName1);

        _weapon1 = (GameObject)Instantiate(Resources.Load("Prefabs/Model/" + _weaponName1));

        //子供にする
        _weapon1.transform.parent = _rightHand.transform;
        _weapon1.transform.position = _rightHand.transform.position;
    }

    //武器2にセットする
    public void setWeapon2(string name)
    {
        _weaponName2 = name;

        _weapon2 = (GameObject)Instantiate(Resources.Load("Prefabs/Model/" + _weaponName2));

        //子供にする
        _weapon2.transform.parent = _rightHand.transform;
        _weapon2.transform.position = _rightHand.transform.position;
    }

}
