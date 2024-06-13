using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyCount : MonoBehaviour
{
    TextMeshProUGUI keycount;

    int goalCount = 0;

    int currentCount = 0;

    private void Awake()
    {
        keycount = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Player player = FindAnyObjectByType<Player>();
        player.onKeyCountChange += RefreshCount;

        goalCount = 0;
        currentCount = 0;
        keycount.text = "Key : 10 / 10";
    }

    private void Update()
    {
        currentCount = goalCount;
        int temp = currentCount;
        keycount.text = $"Key : {temp} / 10";
    }

    private void RefreshCount(int newCount)
    {
        goalCount = newCount;
    }
}
