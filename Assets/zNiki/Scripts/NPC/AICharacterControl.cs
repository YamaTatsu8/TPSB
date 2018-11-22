using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacterControl : MonoBehaviour
{
    private float _rotationSmooth = 2.5f;

    // 追跡するターゲットの場所
    private Transform _target = null;

    [SerializeField]
    private float _range = 5.0f;

    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private GameObject _weapon = null;

    [SerializeField]
    private Transform _view = null;

    // NavMeshAgent
    [SerializeField]
    private NavMeshAgent _agent = null;

    // Use this for initialization
    void Start()
    {
        _target = GameObject.Find("Player").transform;

        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.enabled == true)
        {
            if (_target != null)
            {
                _agent.SetDestination(_target.position);
            }
            if (_agent.remainingDistance > _agent.stoppingDistance)
            {
                this.gameObject.GetComponent<Rigidbody>().velocity = _agent.desiredVelocity;

                _animator.SetBool("Walk", true);
            }
            else
            {
                _animator.SetBool("Walk", false);
            }
        }

        // プレイヤーの方向を向く
        Quaternion targetRotation = Quaternion.LookRotation(_target.position - transform.position);
        targetRotation.x = 0.0f;
        targetRotation.z = 0.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSmooth);

        // 壁が前にある際に、NavMeshAgentを切ってから壁を越えれるようにジャンプする
        Ray ray = new Ray(_view.transform.position, _view.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _range))
        {
            if (hit.collider.tag == "Ground" && _agent.enabled == true)
            {
                _agent.enabled = false;

                float targetHeight = hit.collider.transform.localScale.y;
                Vector3 dis = (hit.point - this.transform.position);

                Vector3 jumpForce = new Vector3(dis.x, targetHeight * 12.5f, dis.z);

                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                this.GetComponent<Rigidbody>().AddForce(jumpForce * 500);

                _animator.SetBool("Jump", true);

                this.Delay(targetHeight * 1.5f, () =>
                {
                    _animator.SetBool("Jump", false);
                    _animator.SetBool("Fall", true);
                });

                this.Delay(targetHeight * 3.0f, () =>
                {
                    _animator.SetBool("Fall", false);
                    _agent.enabled = true;
                });
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            _weapon.GetComponent<EnemyWeaponManager>().Attack();

            _animator.SetBool("Attack", true);
        }
        else
        {
            _animator.SetBool("Attack", false);
        }
    }

    public Vector3 GetTargetPosition()
    {
        return _target.position;
    }
}
