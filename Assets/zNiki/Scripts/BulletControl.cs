using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {

    public int damage = 1;         // ダメ―ジ
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Untagged")
        {
            if (col.gameObject.tag != "Player")
            {
                if (col.gameObject.tag == "Enemy")
                {
                    //col.gameObject.GetComponent<EnemyControl>().Damage(damage);
                }
                Destroy(gameObject);
            }
        }
    }
}
