using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("-----Player-----")]
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private Vector3 _posP;
    [SerializeField]
    private Quaternion _rotP;

    [Header("-----Enemy-----")]
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private Vector3 _posE;
    [SerializeField]
    private Quaternion _rotE;

    [Header("-----Stage-----")]
    [SerializeField]
    private GameObject _stage;

	// Use this for initialization
	void Start ()
    {
        Initialize();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void Initialize()
    {
        RandomSetting();
        //　地形、プレイヤー、敵を生成する
        Instantiate(_stage);
        Instantiate(_player, _posP, _rotP);
        Instantiate(_enemy, _posE, _rotE);
    }

    void RandomSetting()
    {
        //　横幅
        float width = _stage.GetComponent<Renderer>().bounds.size.x;
        //　縦幅
        float depth = _stage.GetComponent<Renderer>().bounds.size.z;
        float minDepth = depth / 10;

        _posP = new Vector3(Random.Range(-width / 2, width / 2), 1, Random.Range(0, minDepth) - depth / 2);
        _posE = new Vector3(Random.Range(-width / 2, width / 2), 1, Random.Range(depth - minDepth, depth) - depth / 2);
    }
}
