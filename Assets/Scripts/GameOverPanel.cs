using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        GameManager.Instance.onDie += () =>
        {
            // 게임이 클리어되면
            canvasGroup.alpha = 1;              // 알파값 올려서 보이게 만들기
            canvasGroup.blocksRaycasts = true;  // 레이케스트를 자기가 되게 하기
        };
    }
}
