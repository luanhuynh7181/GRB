using System;
using GameConstants;
using UnityEditor.PackageManager;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public GameObject nodeWave;
    public GameObject PopupEndGame;
    public GameObject nodePlay;
    public GameObject fog;

    public CanvasGroup textContainer;
    public TMPro.TextMeshProUGUI textWave;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject UIInfo;

    private static bool isFirstTime = true;
    private Vector3 posNodeWave;

    void Awake()
    {
        if (DevMode.IsDev)
            isFirstTime = false;
        nodeWave.SetActive(false);
        PopupEndGame.SetActive(false);
        nodePlay.SetActive(false);
        UIInfo.SetActive(false);
        posNodeWave = nodeWave.transform.position;
    }

    void Start()
    {
        if (!isFirstTime)
            return;
        nodePlay.SetActive(true);
        RunFog(true);
    }

    private void OnEnable()
    {
        GlobalEventManager.ShowPopupEndGame += ShowUIPlayAgain;
    }

    private void OnDisable()
    {
        GlobalEventManager.ShowPopupEndGame -= ShowUIPlayAgain;
    }

    private void RunFog(bool isIn)
    {
        Time.timeScale = isIn ? 0 : 1;
        var image = fog.GetComponent<UnityEngine.UI.Image>();
        LeanTween.alpha(image.rectTransform, isIn ? 0.93f : 0, 0.2f).setIgnoreTimeScale(true);
    }

    public void OnClickPlayNow()
    {
        nodePlay.SetActive(false);
        ShowUIInfo();
    }

    public void OnClickSetting()
    {
        ShowUIInfo();
    }

    public void ShowUIInfo()
    {
        RunFog(true);
        UIInfo.SetActive(true);
        UIInfo.transform.localScale = new Vector3(0, 1, 1);
        textContainer.alpha = 0;
        LeanTween
            .scaleX(UIInfo, 1, 0.5f)
            .setEase(LeanTweenType.easeInOutQuad)
            .setIgnoreTimeScale(true)
            .setOnComplete(() =>
            {
                LeanTween
                    .alphaCanvas(textContainer, 1, 0.5f)
                    .setEase(LeanTweenType.easeInOutQuad)
                    .setIgnoreTimeScale(true)
                    .setOnComplete(() => { });
            });
    }

    public void OnClickCloseInfo()
    {
        UIInfo.SetActive(false);
        RunFog(false);
        if (isFirstTime)
            ShowWave(1);
        isFirstTime = false;
    }

    public void OnClickPlayAgain()
    {
        PopupEndGame.SetActive(false);
        RunFog(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ShowUIPlayAgain(bool isWin)
    {
        PopupEndGame.SetActive(true);
        RunFog(true);
        PopupEndGame.transform.Find("TextWin").GetComponent<TMPro.TextMeshProUGUI>().text = isWin
            ? "Play Again"
            : "Retry";
        if (isWin)
        {
            Saver.pointRespawn = 0;
        }
    }

    public void ShowWave(int wave)
    {
        nodeWave.transform.position = posNodeWave + new Vector3(0, 400, 0);
        textWave.text = $"Wave {wave}";
        nodeWave.SetActive(true);
        LeanTween
            .move(nodeWave, posNodeWave, 0.5f)
            .setEase(LeanTweenType.easeInBack)
            .setIgnoreTimeScale(true)
            .setOnComplete(() =>
            {
                LeanTween
                    .move(nodeWave, posNodeWave + new Vector3(0, 200, 0), 0.1f)
                    .setEase(LeanTweenType.easeOutBack)
                    .setIgnoreTimeScale(true)
                    .setDelay(3f)
                    .setOnComplete(() =>
                    {
                        nodeWave.SetActive(false);
                    });
            });
    }

    public void CheatHP()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
            return;
        player.GetComponent<Player_Controller>().CheatHP();
    }
}
