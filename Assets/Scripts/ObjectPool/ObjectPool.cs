using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static GameObject[] pool;
    public GameObject prefabToCreate;
    public int qtyToCreate = 10;

    void Start()
    {
        pool = new GameObject[qtyToCreate];

        for (int i = 0; i < qtyToCreate; i++)
        {
            pool[i] = Instantiate(prefabToCreate, transform);
            pool[i].SetActive(false);
        }
    }

    public static GameObject Get()
    {
        foreach (GameObject item in pool)
        {
            if (!item.activeSelf)
            {
                return item;
            }
        }
        return null;
    }
}
