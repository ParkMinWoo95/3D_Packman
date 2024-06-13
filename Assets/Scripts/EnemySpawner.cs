using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Enemy enemy;

    Player player;

    public GameObject enemyPrefab;

    public int enemyCount = 4;

    Transform[] enemySlots;

    public Transform target;

    protected virtual void Spawn(Vector3 V)
    {
        Enemy enemy = Factory.Instance.GetEnmey(V);
        enemy.target = GameManager.Instance.Player.transform;
    }

    private void Awake()
    {
        //enemy = GameManager.Instance.Enemy; 
        //player = GameManager.Instance.Player; 
        enemySlots = new Transform[transform.childCount];

        for(int i = 0; i < enemySlots.Length; i++)
        {
            enemySlots[i] = transform.GetChild(i);
        }
    }

    private void Start()
    {
        for(int j = 0; j < enemyCount; j++)
        {
            Spawn(enemySlots[j].transform.position);
        }
        //enemy.target = player.transform;
    }
}
