using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkSubWeaponCanvas : MonoBehaviour {

    //弾数確認
    [SerializeField]
    private Text _magazin;

    //
    [SerializeField]
    private int _max = 0;

    [SerializeField]
    private GameObject _weapon;

    // Use this for initialization
    void Start () {

        _magazin.text = _max.ToString();

	}
	
	// Update is called once per frame
	void Update () {

        _magazin.text = _weapon.GetComponent<MissileShot>().getCount().ToString();

	}
}
