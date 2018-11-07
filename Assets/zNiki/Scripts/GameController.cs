using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Button
{
    A,
    B,
    X,
    Y,
    L1,
    R1,
    BACK,
    START,
    L3,
    R3,
}

public enum Trigger
{
    LEFT,
    RIGHT,
}

public enum StickType
{
    LEFTSTICK,
    RIGHTSTICK,
    CLOSS,
}

public class GameController : Util.SingletonMonoBehaviour<GameController>
{
    private const float ZERO = 0.0f;
    private const float LIMIT = 0.2f;

    private bool _connected = false;
    private bool _isUseAxis = false;

    private Button _prevButton;
    private Trigger _prevTrigger;
    //private string _currentButton = null;

    private Direction _prevDir;
    private StickType _prevStickType;

    // Use this for initialization
    void Start()
    {
        CheckConnect();
    }

    public void ControllerUpdate()
    {
        CheckUseAxis();
    }

    public bool GetConnectFlag()
    {
        CheckConnect();

        return _connected;
    }

    /// <summary>
    /// コントローラの名前取得を利用した接続確認
    /// </summary>
    private void CheckConnect()
    {
        var names = Input.GetJoystickNames();
        string controllerName = string.Empty;
        if (names.Any())
        {
            foreach (string name in names)
            {
                if (name != "")
                {
                    controllerName = name;
                    break;
                }
            }
        }

        if (controllerName.Length > 0)
        {
            _connected = true;
        }
        else
        {
            _connected = false;
        }
    }

    /// <summary>
    /// ボタンが入力されているかの確認
    /// </summary>
    public bool CheckUseButton()
    {
        if (ButtonDown(_prevButton))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// トリガーが入力されているかの確認
    /// </summary>
    public bool CheckUseTrigger()
    {
        if (TriggerDown(_prevTrigger))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// スティックと十字キーが入力されているかの確認
    /// </summary>
    private void CheckUseAxis()
    {
        if (CheckDirection(_prevDir, _prevStickType) == 0.0f)
        {
            _isUseAxis = false;
        }
    }

    /// <summary>
    /// 指定されたボタンが押されているか
    /// </summary>
    /// <param name="b">判定したいボタン</param>
    /// <returns>true:押されている false:押されていない</returns>
    public bool ButtonDown(Button b)
    {
        _prevButton = b;

        string button = b.ToString();

        try
        {
            if (Input.GetButtonDown(button))
            {
                return true;
            }
        }
        catch
        {
            return false;
        }
        return false;
    }

    /// <summary>
    /// 指定されたトリガーが押されているか
    /// </summary>
    /// <param name="t">判定したいトリガー</param>
    /// <returns>true:押されている false:押されていない</returns>
    public bool TriggerDown(Trigger t)
    {
        _prevTrigger = t;

        string trigger = t.ToString();

        try
        {
            if (Input.GetAxis(trigger) >= 0.75f)
            {
                return true;
            }
        }
        catch
        {
            return false;
        }
        return false;
    }

    /// <summary>
    /// 指定した向きに指定されたキーかスティックが入力されているか
    /// </summary>
    /// <param name="d">向き</param>
    /// <param name="t">キーの種類</param>
    /// <returns>true:入力されている false:入力されていない</returns>
    public float CheckDirection(Direction d, StickType t)
    {
        switch (t)
        {
            case StickType.LEFTSTICK:
                return CheckAxis(d, "L-Stick");
            case StickType.RIGHTSTICK:
                return CheckAxis(d, "R-Stick");
            case StickType.CLOSS:
                return CheckAxis(d, "C-Button");
            default:
                break;
        }
        return ZERO;
    }
    
    /// <summary>
    /// Moveの単発型
    /// </summary>
    /// <param name="d">向き</param>
    /// <returns>true:入力されている false:入力されていない</returns>
    public bool CheckDirectionOnce(Direction d, StickType t)
    {
        if (!_isUseAxis && CheckDirection(d, t) != 0)
        {
            _isUseAxis = true;
            _prevDir = d;
            _prevStickType = t;
            return true;
        }
        return false;
    }

    private float CheckAxis(Direction d, string n)
    {
        switch (d)
        {
            case Direction.Front:
                if (Input.GetAxis(n + "Vertical") >= LIMIT)
                {
                    return Input.GetAxis(n + "Vertical");
                }
                break;

            case Direction.Back:
                if (Input.GetAxis(n + "Vertical") <= -LIMIT)
                {
                    return Input.GetAxis(n + "Vertical");
                }
                break;

            case Direction.Left:
                if (Input.GetAxis(n + "Horizontal") <= -LIMIT)
                {
                    return Input.GetAxis(n + "Horizontal");
                }
                break;

            case Direction.Right:
                if (Input.GetAxis(n + "Horizontal") >= LIMIT)
                {
                    return Input.GetAxis(n + "Horizontal");
                }
                break;
            default:
                break;
        }
        return ZERO;
    }
}