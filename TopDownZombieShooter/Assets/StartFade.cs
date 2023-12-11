using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup fade;

    private void Start()
    {
        fade.alpha = 1f;
        fade.DOFade(0f, 1f).SetEase(Ease.InOutBack);
    }
}
