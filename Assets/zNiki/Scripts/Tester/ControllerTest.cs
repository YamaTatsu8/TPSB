﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    GameController con;

    // Use this for initialization
    void Start()
    {
        con = GameController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // このUpdateは必須
        con.ControllerUpdate();

        // このifでコントローラが刺さってるか判定する
        if (con.GetConnectFlag())
        {
            // ボタン入力を取る
            if (con.ButtonDown(Button.A))
            {
                Debug.Log("A");
            }
            if (con.ButtonDown(Button.B))
            {
                Debug.Log("B");
            }
            if (con.ButtonDown(Button.X))
            {
                Debug.Log("X");
            }
            if (con.ButtonDown(Button.Y))
            {
                Debug.Log("Y");
            }
            if (con.ButtonDown(Button.L1))
            {
                Debug.Log("L1");
            }
            if (con.ButtonDown(Button.R1))
            {
                Debug.Log("R1");
            }
            if (con.ButtonDown(Button.BACK))
            {
                Debug.Log("BACK");
            }
            if (con.ButtonDown(Button.START))
            {
                Debug.Log("START");
            }
            if (con.ButtonDown(Button.L3))
            {
                Debug.Log("L3");
            }
            if (con.ButtonDown(Button.R3))
            {
                Debug.Log("R3");
            }

            // やってもいいけど意味ないよ
            if (con.ButtonDown(Button.R3 + 1))
            {
                Debug.Log("R3");
            }

            // 左スティックと十字キーの入力を取る
            //if (con.Move(Direction.Front))
            //{
            //    Debug.Log("UP");
            //}
            //if (con.Move(Direction.Back))
            //{
            //    Debug.Log("DOWN");
            //}
            //if (con.Move(Direction.Left))
            //{
            //    Debug.Log("LEFT");
            //}
            //if (con.Move(Direction.Right))
            //{
            //    Debug.Log("RIGHT");
            //}

            if (con.OneShotMove(Direction.Front))
            {
                Debug.Log("Move Front");
            }
            if (con.OneShotMove(Direction.Back))
            {
                Debug.Log("Move Back");
            }
            if (con.OneShotMove(Direction.Left))
            {
                Debug.Log("Move Left");
            }
            if (con.OneShotMove(Direction.Right))
            {
                Debug.Log("Move Right");
            }

            // トリガーの入力を取る
            if (con.TriggerDown(Trigger.Left))
            {
                Debug.Log("Left Trigger");
            }
            if (con.TriggerDown(Trigger.Right))
            {
                Debug.Log("Right Trigger");
            }

            // 右スティックの入力を取る
            if (con.ViewpointMove(Direction.Front))
            {
                Debug.Log("ViewPoint Front");
            }
            if (con.ViewpointMove(Direction.Back))
            {
                Debug.Log("ViewPoint Back");
            }
            if (con.ViewpointMove(Direction.Left))
            {
                Debug.Log("ViewPoint left");
            }
            if (con.ViewpointMove(Direction.Right))
            {
                Debug.Log("ViewPoint Right");
            }
        }
        else
        {
            Debug.Log("つっかえ！");
        }
    }
}
