using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookOnTarget : MonoBehaviour {

    //ターゲット
    [SerializeField]
    private GameObject _target;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    protected void OnTriggerEnter(Collider target)
    {
        //タグがプレイヤーだった場合
        if(target.gameObject.tag == "Player")
        {
            _target = target.gameObject;
        }
    }

    protected void OnTriggerExit(Collider target)
    {
        if(target.gameObject.tag=="Player")
        {
            _target = null;
        }
    }

    public GameObject getTarget()
    {
        return this._target;
    }

}
