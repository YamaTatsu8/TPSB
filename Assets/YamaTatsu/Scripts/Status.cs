using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {

    //　HP
    [SerializeField]
    private float _HP = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
        // HPが0以下の場合破壊
        if(_HP < 0)
        {
            Destroy(this.gameObject);
        }
	}

    public bool getDestroy()
    {
        return true;
    }

    //ダメージを与える処理
    public void hitDamage(int damage)
    {
        Debug.Log("ダメージ");
        _HP -= damage;
    }

    public float getHP()
    {
        return (float)_HP;
    }
  
}
