using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSystem : MonoBehaviour {

    //メイン武器1
    [SerializeField]
    private string _mainWeapon1;
    
    //メイン武器2
    [SerializeField]
    private string _mainWeapon2;

    //サブ武器
    [SerializeField]
    private string _subWeapon;

    //
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}

    //セッター
    public string getMain1()
    {
        return _mainWeapon1;
    }

    public string getMain2()
    {
        return _mainWeapon2;
    }

    public string getSub()
    {
        return _subWeapon;
    }

    //ゲッター
    public void setMain1(string weapon)
    {
        _mainWeapon1 = weapon;
    }

    public void setMain2(string weapon)
    {
        _mainWeapon2 = weapon;
    }

    public void setSub(string weapon)
    {
        _subWeapon = weapon;
    }

}
