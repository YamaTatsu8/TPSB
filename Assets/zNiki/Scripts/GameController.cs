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
    Left,
    Right,
}

public class GameController : Util.SingletonMonoBehaviour<GameController>
{
    private bool _connected = false;
    private bool _isUseAxis = false;

    private Direction _prevDir;

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
        string controllerNames = string.Empty;
        if (names.Any())
        {
            controllerNames = names[0];
        }

        if (controllerNames.Length > 0)
        {
            _connected = true;
        }
        else
        {
            _connected = false;
        }
    }

    /// <summary>
    /// スティックと十字キーが入力されているかの確認
    /// </summary>
    private void CheckUseAxis()
    {
        if (!Move(_prevDir))
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
        string trigger = t.ToString();

        try
        {
            if (Input.GetAxis(trigger) == 1)
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
    /// 指定した向きにスティック/十字キーが入力されているか
    /// </summary>
    /// <param name="d">向き</param>
    /// <returns>true:入力されている false:入力されていない</returns>
    public bool Move(Direction d)
    {
        var limit = 0.01f;
        switch (d)
        {
            case Direction.Front:
                if (Input.GetAxis("L-StickVertical") >= limit || Input.GetAxis("C-ButtonVertical") >= limit)
                {
                    return true;
                }
                break;

            case Direction.Back:
                if (Input.GetAxis("L-StickVertical") <= -limit || Input.GetAxis("C-ButtonVertical") <= -limit)
                {
                    return true;
                }
                break;

            case Direction.Left:
                if (Input.GetAxis("L-StickHorizontal") <= -limit || Input.GetAxis("C-ButtonHorizontal") <= -limit)
                {
                    return true;
                }
                break;

            case Direction.Right:
                if (Input.GetAxis("L-StickHorizontal") >= limit || Input.GetAxis("C-ButtonHorizontal") >= limit)
                {
                    return true;
                }
                break;

            default:
                break;
        }
        return false;
    }

    /// <summary>
    /// Moveの単発型
    /// </summary>
    /// <param name="d">向き</param>
    /// <returns>true:入力されている false:入力されていない</returns>
    public bool OneShotMove(Direction d)
    {
        if (!_isUseAxis && Move(d))
        {
            _isUseAxis = true;
            _prevDir = d;
            return true;
        }

        return false;
    }

    public bool ViewpoinMove(Direction d)
    {
        var limit = 0.01f;
        switch (d)
        {
            case Direction.Front:
                if (Input.GetAxis("R-StickVertical") >= limit)
                {
                    return true;
                }
                break;

            case Direction.Back:
                if (Input.GetAxis("R-StickVertical") <= -limit)
                {
                    return true;
                }
                break;

            case Direction.Left:
                if (Input.GetAxis("R-StickHorizontal") <= -limit)
                {
                    return true;
                }
                break;

            case Direction.Right:
                if (Input.GetAxis("R-StickHorizontal") >= limit)
                {
                    return true;
                }
                break;

            default:
                break;
        }
        return false;
    }
}