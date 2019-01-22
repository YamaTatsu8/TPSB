using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class WeaponImage : MonoBehaviour {

    private Image _weapon1Image;

    private Image _weapon2Image;

    //武器リスト
    private List<string[]> _weaponList = new List<string[]>();

    //武器数
    private int _weaponNum = 0;

    //
    private GameObject canvas;

    // Use this for initialization
    void Start () {

        //メイン武器のリスト作成
        WeaponAdd("WeaponList", _weaponList);

        canvas = GameObject.Find("Customize");

        //
        _weapon1Image = GameObject.Find("Wepoan1").GetComponent<Image>();

        _weapon2Image = GameObject.Find("Weapon2").GetComponent<Image>();

    }
	
	// Update is called once per frame
	void Update () {

	}

    //武器画像の差し替え
    public void setWeaponName(string name)
    {
       for(int i = 0; i < _weaponNum; i++)
        {
            if(name == _weaponList[i][0])
            {
                _weapon1Image = (Image)Instantiate(Resources.Load("WeaponImages/" + _weaponList[i][0].ToString()));
            }
            else if (name == _weaponList[i][0])
            {
                _weapon1Image = (Image)Instantiate(Resources.Load("WeaponImages/" + _weaponList[i][0].ToString()));
            }
            else if (name == _weaponList[i][0])
            {
                _weapon1Image = (Image)Instantiate(Resources.Load("WeaponImages/" + _weaponList[i][0].ToString()));
            }
            else if (name == _weaponList[i][0])
            {
                _weapon1Image = (Image)Instantiate(Resources.Load("WeaponImages/" + _weaponList[i][0].ToString()));
            }
        }
    }

    //csvファイルから読み込んだ武器名をリストに入れる関数
    private void WeaponAdd(string name, List<string[]> list)
    {
        string filename = name;

        var csvFile = Resources.Load("CSV/" + filename) as TextAsset;

        //csvの内容をStringReaderに変換
        var reader = new StringReader(csvFile.text);

        //csvの内容を最後まで取得
        while (reader.Peek() > -1)
        {
            //1行読む
            var lineData = reader.ReadLine();

            //カンマ区切りのデータを文字列の配列に変換
            var weaponName = lineData.Split(',');

            //リストに追加
            list.Add(weaponName);

            _weaponNum++;
        }

    }
}
