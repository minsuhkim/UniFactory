using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProductSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnRate = 2f;
    [SerializeField] private GameObject _product;

    private void Start()
    {
        StartCoroutine(C_SpawnProduct());
    }

    private IEnumerator C_SpawnProduct()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnRate);
            Instantiate(_product, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity);
        }
    }
}
