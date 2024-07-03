using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAMovements : MonoBehaviour
{
    [Header("Reference :")]
    [SerializeField] Transform _player;
    [SerializeField] Animator _animator;
    [SerializeField] GameObject _eyes;
    [SerializeField] SkinnedMeshRenderer skinnedMesh;
    NavMeshAgent _agent;

    [Header("Stats :")]
    [SerializeField] float _rayonDetection;
    [SerializeField] float _runSpeed;
    [SerializeField] float dissolveRate = 0.125f;
    [SerializeField] float refreshRate = 0.025f;

    [Header("Ballades :")]
    [SerializeField] float _tempBalladesMin;
    [SerializeField] float _tempBalladesMax;
    [SerializeField] float _distanceBalladesMin;
    [SerializeField] float _distanceBalladesMax;

    bool hasDestination;
    Material _material;

    private void Start()
    {
        if (skinnedMesh != null)
            _material = skinnedMesh.material;
    }

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(_animator.GetBool("IsDead") == false)
        {
            if (_agent.remainingDistance < 0.75f && !hasDestination)
            {
                StartCoroutine(GetNewDestination());
            }
        }
        else
        {
            _eyes.SetActive(false);
            StartCoroutine(DissolveCo());
        }
        
        
        _animator.SetFloat("Speed", _agent.velocity.magnitude);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _rayonDetection);
    }

    IEnumerator GetNewDestination()
    {
        hasDestination = true;
        yield return new WaitForSeconds(Random.Range(_tempBalladesMin, _tempBalladesMax));

        Vector3 nextDestination = transform.position;
        nextDestination += Random.Range(_distanceBalladesMin, _distanceBalladesMax) * new Vector3(Random.Range(-1f, 1), 0f, Random.Range(-1f, 1f)).normalized;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(nextDestination, out hit, _distanceBalladesMax, NavMesh.AllAreas))
        {
            _agent.SetDestination(hit.position);
        }
        hasDestination = false;
    }

    IEnumerator DissolveCo()
    {
        float counter = 0;
        while (_material.GetFloat("_Dissolving") < 1)
        {
            counter += dissolveRate;
            _material.SetFloat("_Dissolving", counter);
            
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
