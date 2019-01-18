using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    InputField _inputField;
    
	void Start () {
        _inputField = GetComponent<InputField>();

        // 値をリセット
        _inputField.text = "";
    }

    /// <summary>
    /// Log出力用メソッド
    /// 入力値を取得してLogに出力し、初期化
    /// </summary>
    public string GatInput()
    {
        return _inputField.text;

    }

    /// <summary>
    /// InputFieldの初期化メソッド
    /// 入力値をリセットして、フィールドにフォーカスする
    /// </summary>
    public void InitInputField()
    {
        // フォーカス
        _inputField.ActivateInputField();
    }

    public void FinInputField()
    {
        _inputField.DeactivateInputField();
    }
}
