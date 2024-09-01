using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnInterval = 1f;
    private float _chrono = 0f;
    private bool canSpawn = true;
    public GameObject _particlePrefab;
    [SerializeField] Transform _transformParent;
    BoxCollider _boxCollider;

    void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {

        if (canSpawn)
        {
            var spawnBox = _boxCollider.size;
            Vector3 spawnPosition = /*transform.position + */new Vector3(Random.value * spawnBox.x, Random.value * spawnBox.y, Random.value * spawnBox.z);
            spawnPosition = transform.TransformPoint(spawnPosition - spawnBox / 2);
            GameObject ball = ObjectPool.Get();
            ball.transform.parent = _transformParent;
            ball.SetActive(true);
            ball.transform.position = spawnPosition;
            canSpawn = false;
        }

        _chrono += Time.deltaTime;

        if (_chrono >= spawnInterval)
        {
            canSpawn = true;
            _chrono = 0f;
        }
    }
}
