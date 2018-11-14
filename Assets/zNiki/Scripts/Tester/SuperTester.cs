using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperTester : MonoBehaviour
{
    GameController con;

    private bool _isEquippedMain1 = true;

    [SerializeField]
    private GameObject _main1;

    [SerializeField]
    private GameObject _main2;

    [SerializeField]
    private GameObject _sub;

    private void Awake()
    {
        SoundManager.LoadBgm("bgm", "02_Trailer2");
    }

    // Use this for initialization
    void Start ()
    {
        con = GameController.Instance;

        Debug.Log(SoundManager.PlayBgm("bgm"));
    }

    // Update is called once per frame
    void Update()
    {
        //// このUpdateは必須
        //con.ControllerUpdate();

        //// このifでコントローラが刺さってるか判定する
        //if (con.GetConnectFlag())
        //{
        //    if (con.TriggerDown(Trigger.LEFT))
        //    {
        //        if (_isEquippedMain1)
        //        {
        //            _main1.GetComponent<WeaponManager>().Attack();
        //        }
        //        else
        //        {
        //            _main2.GetComponent<WeaponManager>().Attack();
        //        }
        //    }
        //    if (con.TriggerDown(Trigger.RIGHT))
        //    {
        //        _sub.GetComponent<WeaponManager>().Attack();
        //    }
        //    if (con.ButtonDown(Button.X))
        //    {
        //        _isEquippedMain1 = !_isEquippedMain1;
        //    }
        //}
        //else
        //{
        //    Debug.Log(con.GetConnectFlag());
        //}

        //if (_isEquippedMain1)
        //{
        //    _main1.SetActive(true);
        //    _main2.SetActive(false);
        //}
        //else
        //{
        //    _main1.SetActive(false);
        //    _main2.SetActive(true);
        //}

        //if (Input.GetMouseButton(0))
        //{
        //    this.transform.GetChild(0).GetComponent<WeaponManager>().Shot();
        //}
    }
}
