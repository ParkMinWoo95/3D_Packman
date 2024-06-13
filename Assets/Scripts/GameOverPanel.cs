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
            // ������ Ŭ����Ǹ�
            canvasGroup.alpha = 1;              // ���İ� �÷��� ���̰� �����
            canvasGroup.blocksRaycasts = true;  // �����ɽ�Ʈ�� �ڱⰡ �ǰ� �ϱ�
        };
    }
}
