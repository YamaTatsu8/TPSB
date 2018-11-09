using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour {

    //flag
    private bool _flag = false;

    public bool WallHit()
    {
        return _flag;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            _flag = true;
        }
        else
        {
            _flag = false;
        }
    }

}
