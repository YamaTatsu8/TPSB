using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    //武器のステート
    private enum STATE_WEAPON
    {
        WEAPON1,        //武器1
        WEAPON2,        //武器2
        SUBWEAPON       //サブ武器
    }

    //Player 
    private GameObject _player;

    //武器1
    [SerializeField]
    private GameObject _weapon1;

    //武器2
    [SerializeField]
    private GameObject _weapon2;

    //サブ武器
    [SerializeField]
    private GameObject _subWeapon;

	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {
		
	}



}
