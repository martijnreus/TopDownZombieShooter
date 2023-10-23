using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodyScreen : MonoBehaviour
{
    private Player player;

    [SerializeField] private Image bloodyScreen;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        float alphaAmount = CalculateAlpha();
        SetPictureAlpha(alphaAmount);
    }

    private float CalculateAlpha()
    {
        return 1f - Mathf.Pow(player.GetHealthSystem().GetHealthNormalized(), 2);
    }

    private void SetPictureAlpha(float alphaAmount)
    {
        bloodyScreen.color = new Color(1, 1, 1, alphaAmount);
    }
}
