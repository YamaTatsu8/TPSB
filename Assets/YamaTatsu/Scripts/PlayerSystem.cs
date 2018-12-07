using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSystem : MonoBehaviour {

    //キャラ名
    [SerializeField]
    private string _char = "Unity-Chan";

    //メイン武器1
    [SerializeField]
    private string _mainWeapon1 = "Main1";
    
    //メイン武器2
    [SerializeField]
    private string _mainWeapon2 = "Main2";

    //
    private GameObject _player;

    //サブ武器
    [SerializeField]
    private string _subWeapon;

    private static PlayerSystem _playerSystem;

    //
    void Awake()
    {
        
    }

    private void Start()
    {
        if (_playerSystem == null)
        {
            _playerSystem = FindObjectOfType<PlayerSystem>() as PlayerSystem;
            DontDestroyOnLoad(gameObject);
        }

        _char = "Unity-Chan";

        _mainWeapon1 = "Main1";

        _mainWeapon2 = "Main2";

        Debug.Log(_char);
    }


    // Update is called once per frame
    void Update () {
		
	}

    //ゲッター
    public string GetChar()
    {
        return _char;
    }


    public string GetMain1()
    {
        return _mainWeapon1;
    }

    public string GetMain2()
    {
        return _mainWeapon2;
    }

    public string GetSub()
    {
        return _subWeapon;
    }

    //セッター
    public void SetChar(string name)
    {
        _char = name;
    }

    public void SetMain1(string weapon)
    {
        _mainWeapon1 = weapon;
    }

    public void SetMain2(string weapon)
    {
        _mainWeapon2 = weapon;
    }

    public void SetSub(string weapon)
    {
        //
        _subWeapon = "Missile";
    }

    public void Init()
    {
        _mainWeapon1 = "Main1";

        _mainWeapon2 = "Main2";

        _char = "Unity-Chan";
    }



}
