using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponName : MonoBehaviour {

    //武器名
    [SerializeField]
    private string _weaponName;

    //テキスト
    [SerializeField]
    private Text _text;

    private void Start()
    {
        GameObject go = transform.Find("Text").gameObject;
        _text = go.GetComponent<Text>();
    }

    public void setName(string name)
    {
        _text.text = name;
    }

    public string getName()
    {
        return _weaponName;
    }

}
