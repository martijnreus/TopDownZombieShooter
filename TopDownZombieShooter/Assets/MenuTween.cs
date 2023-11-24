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

    private float menuTweenSpeed = 0.75f;

    Vector2 mainMenuStartPosition;
    Vector2 mainMenuTextPosition;

    private void Start()
    {
        mainMenuStartPosition = mainMenuPanel.transform.localPosition;
        mainMenuTextPosition = mainMenuTitle.transform.localPosition;
    }

    public void OpenStatistics()
    {
        float openDelay = 0.1f;

        statisticsMenuCanvasGroup.alpha = 1f;

        statisticsMenu.transform.localPosition = new Vector3(0f, -1000f, 0f);
        statisticsMenu.transform.DOLocalMoveY(0f, menuTweenSpeed).SetEase(Ease.InOutBack).SetDelay(openDelay);

        statisticsMenuCanvasGroup.DOFade(1f, menuTweenSpeed).SetEase(Ease.InOutBack);
    }

    public void CloseStatistics()
    {
        statisticsMenuCanvasGroup.alpha = 1f;

        statisticsMenu.transform.localPosition = new Vector3(0f, 0f, 0f);
        statisticsMenu.transform.DOLocalMoveY(-1000f, menuTweenSpeed).SetEase(Ease.InOutBack);

        statisticsMenuCanvasGroup.DOFade(0f, menuTweenSpeed * 2).SetEase(Ease.InOutBack);
    }

    public void OpenSettings()
    {

    }

    public void CloseSettings()
    {

    }

    public void OpenMainMenu()
    {
        float openDelay = 0.1f;

        mainMenuCanvasGroup.alpha = 0f;

        mainMenuTitle.transform.localPosition = new Vector3(mainMenuTextPosition.x, 1000f, 0f);
        mainMenuTitle.transform.DOLocalMoveY(mainMenuTextPosition.y, menuTweenSpeed).SetEase(Ease.InOutBack).SetDelay(openDelay);

        mainMenuPanel.transform.localPosition = new Vector3(2000f, mainMenuStartPosition.y, 0f);
        mainMenuPanel.transform.DOLocalMoveX(mainMenuStartPosition.x, menuTweenSpeed).SetEase(Ease.InOutBack).SetDelay(openDelay);

        mainMenuCanvasGroup.DOFade(1f, menuTweenSpeed).SetEase(Ease.InOutBack);
    }

    public void CloseMainMenu()
    {
        mainMenuCanvasGroup.alpha = 1f;

        mainMenuTitle.transform.DOLocalMoveY(1000f, menuTweenSpeed).SetEase(Ease.InOutBack);
        mainMenuPanel.transform.DOLocalMoveX(2000f, menuTweenSpeed).SetEase(Ease.InOutBack);

        mainMenuCanvasGroup.DOFade(0f, menuTweenSpeed).SetEase(Ease.InOutBack);
    }
}
