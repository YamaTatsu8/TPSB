using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    public float _spawnRate = 5.0f;                          // スポーンする間隔
    public float _spawnRange = 5.0f;
    public int _spawnEnemyNum = 1;                           // スポーンする数
    public GameObject _enemyPrefab;

    public bool _isOnce;

    private bool _isSpawned = false;

    private float _nextSpawn;                                // スポーンする時間
    private GameObject _target;                              // 敵の追跡する対象                              

    // Use this for initialization
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _nextSpawn && !_isSpawned)
        {
            // 次弾発射までの時間更新
            _nextSpawn = Time.time + _spawnRate;

            // エネミーの生成
            for (int i = 0; i < _spawnEnemyNum; i++)
            {
                // 敵の生成
                GameObject enemyClone = Instantiate<GameObject>(_enemyPrefab);

                // スポーン地点に配置
                Vector3 pos = new Vector3(Random.Range(-_spawnRange, _spawnRange), 1.5f, Random.Range(-_spawnRange, _spawnRange));
                enemyClone.transform.position = gameObject.transform.position + pos;

                // ターゲットをセット
                enemyClone.GetComponent<EnemyController>().SetTarget(_target);

                if (_isOnce)
                {
                    _isSpawned = true;
                }
            }
        }
    }
}