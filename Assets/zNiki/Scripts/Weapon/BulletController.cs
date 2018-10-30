using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // 消滅までの時間
    [SerializeField]
    private float _destroyTime = 3.0f;

    public void DeleteBullet(GameObject bulletClone)
    {
        this.Delay(_destroyTime, () =>
        {
            Destroy(bulletClone);
        });
    }
}
