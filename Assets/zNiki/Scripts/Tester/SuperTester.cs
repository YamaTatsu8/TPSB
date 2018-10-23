using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperTester : MonoBehaviour
{
    GameController con;
    
    // Use this for initialization
    void Start ()
    {
        con = GameController.Instance;        
    }
	
	// Update is called once per frame
	void Update ()
    {
        // このUpdateは必須
        con.ControllerUpdate();

        // このifでコントローラが刺さってるか判定する
        if (con.GetConnectFlag())
        {
            if (con.TriggerDown(Trigger.LEFT))
            {
                this.transform.GetChild(0).GetComponent<WeaponManager>().Attack();
            }
        }
        else
        {
            Debug.Log(con.GetConnectFlag());
        }

        //if (Input.GetMouseButton(0))
        //{
        //    this.transform.GetChild(0).GetComponent<WeaponManager>().Shot();
        //}
    }
}
