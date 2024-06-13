using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType
{
    Key,
    Enemy
}

public class Factory : Singleton<Factory>
{
    KeyPool key;
    EnemyPool enemy;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        key = GetComponentInChildren<KeyPool>();
        if (key != null) key.Initialize();

        enemy = GetComponentInChildren<EnemyPool>();
        if(enemy != null) enemy.Initialize();
    }
 
    /// <summary>
    /// 풀에 있는 게임 오브젝트 하나 가져오기
    /// </summary>
    /// <param name="type">가져올 오브젝트의 종류</param>
    /// <param name="position">오브젝트가 배치될 위치</param>
    /// <param name="angle">오브젝트의 초기 각도</param>
    /// <returns>활성화된 오브젝트</returns>
    public GameObject GetObject(PoolObjectType type, Vector3? position = null, Vector3? euler = null)
    {
        GameObject result = null;
        switch (type)
        {
            case PoolObjectType.Key:
                result = key.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.Enemy:
                result = enemy.GetObject(position, euler).gameObject;
                break;
        }

        return result;
    }

    public Key GetKey()
    {
        return key.GetObject();
    }

    public Key GetKey(Vector3 position, float angle = 0.0f)
    {
        return key.GetObject(position, angle * Vector3.forward);
    }

    public Enemy GetEnmey()
    {
        return enemy.GetObject();
    }

    public Enemy GetEnmey(Vector3 position, float angle = 0.0f)
    {
        return enemy.GetObject(position, angle * Vector3.forward);
    }
}