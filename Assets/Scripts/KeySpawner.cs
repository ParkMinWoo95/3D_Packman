using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject keyPrefab;

    public int keyCount = 10;

    Transform[] keySlots;

    protected virtual void Spawn(Vector3 V)
    {
        Factory.Instance.GetKey(V);
    }
    private void Awake()
    {
        keySlots = new Transform[transform.childCount];

        for (int k = 0; k < keySlots.Length; k++)
        {
            keySlots[k] = transform.GetChild(k);
        }
    }

    private void Start()
    {
        Mix();

        for(int i = 0; i < keyCount; i++)
        {
            Spawn(keySlots[i].transform.position);
        }
    }

    void Mix()
    {
        for(int j = keySlots.Length - 1; j >-1; j--)
        {
            int index = Random.Range(0, j);

            (keySlots[index], keySlots[j]) = (keySlots[j], keySlots[index]);
        }
    }
}
