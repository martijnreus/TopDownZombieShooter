using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodyScreen : MonoBehaviour
{
    private Player player;

    private float alphaAmount = 0f;
    private float timeDamaged;

    [SerializeField] private Image bloodyScreen;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float fadeDelay;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        player.GetHealthSystem().OnDamaged += BloodyScreen_OnDamaged;
    }

    private void BloodyScreen_OnDamaged(object sender, System.EventArgs e)
    {
        alphaAmount = 1f;
        timeDamaged = Time.time;
    }

    private void Update()
    {
        FadeBloodEffect();
        UpdatePictureAlpha();
    }

    private void FadeBloodEffect()
    {
        if (Time.time - timeDamaged > fadeDelay)
        {
            alphaAmount -= fadeSpeed * Time.deltaTime;
        }
    }

    private void UpdatePictureAlpha()
    {
        bloodyScreen.color = new Color(1, 1, 1, alphaAmount);
    }
}
