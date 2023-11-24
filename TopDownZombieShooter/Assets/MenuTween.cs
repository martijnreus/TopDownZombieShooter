using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuTween : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject mainMenuTitle;
    [SerializeField] private CanvasGroup mainMenuCanvasGroup;

    [Header("Statistics Menu")]
    [SerializeField] private GameObject statisticsMenu;
    [SerializeField] private CanvasGroup statisticsMenuCanvasGroup;

    [Header("Settings Menu")]
    [SerializeField] private GameObject settingsMrnu;

    private float menuTweenSpeed = 1f;

    public void OpenStatistics()
    {
        float openDelay = 0.2f;

        statisticsMenuCanvasGroup.alpha = 1f;

        statisticsMenu.transform.localPosition = new Vector3(0f, -1000f, 0f);
        statisticsMenu.transform.DOLocalMoveY(0f, menuTweenSpeed).SetEase(Ease.InOutBack).SetDelay(openDelay);
    }

    public void CloseStatistics()
    {

    }

    public void OpenSettings()
    {

    }

    public void CloseSettings()
    {

    }

    public void OpenMainMenu()
    {

    }

    public void CloseMainMenu()
    {
        mainMenuCanvasGroup.alpha = 1f;

        mainMenuTitle.transform.DOLocalMoveY(1000f, menuTweenSpeed).SetEase(Ease.InOutBack);
        mainMenuPanel.transform.DOLocalMoveX(2000f, menuTweenSpeed).SetEase(Ease.InOutBack);

        mainMenuCanvasGroup.DOFade(0f, menuTweenSpeed).SetEase(Ease.InOutBack);
    }
}
