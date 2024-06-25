using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAMovements : MonoBehaviour
{
    [Header("Reference :")]
    [SerializeField] Transform _player;
    NavMeshAgent _agent;

    [Header("Zone detection :")]
    [SerializeField] float _rayonDetection;

    [Header("Ballades :")]
    [SerializeField] float _tempBalladesMin;
    [SerializeField] float _tempBalladesMax;
    [SerializeField] float _distanceBalladesMin;
    [SerializeField] float _distanceBalladesMax;

    bool hasDestination,
         isAttacking;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_agent.remainingDistance < 0.75f && !hasDestination)
        {
            StartCoroutine(GetNewDestination());
        }
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
}
