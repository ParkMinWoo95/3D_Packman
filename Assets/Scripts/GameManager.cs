using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    Player player;

    Enemy enemy;
    public Player Player
    {
        get
        {
            if(player == null)
                player = FindAnyObjectByType<Player>();
            return player;
        }
    }

    public Enemy Enemy
    {
        get
        {
            if (enemy == null)
                enemy = FindAnyObjectByType<Enemy>();
            return enemy;
        }
    }

    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
        enemy = FindAnyObjectByType<Enemy>();
    }

    public Action onDie;

    public void Die()
    {
        onDie?.Invoke();
    }

    public Action onClear;

    public void Clear()
    {
        onClear?.Invoke();
    }
}
