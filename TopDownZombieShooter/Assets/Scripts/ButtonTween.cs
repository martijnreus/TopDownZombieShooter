using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonTween : MonoBehaviour
{
    void Start()
    {
        transform.DOMoveX(transform.position.x - 1f, 2f).SetEase(Ease.OutElastic);
    }

    private void MoveLeft()
    {
        //move left when game starts
        transform.DOMoveX(transform.position.x - 1f, 2f).SetEase(Ease.OutElastic);
    }

}
