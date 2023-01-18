using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FlowManager : MonoBehaviour
{

    public GameManager gameManager;
    public MoneyCount moneyCount;
    public SystemManager systemManager;
    public DayManager dayManager;

    //UI 
    public TMP_Text clock;

    private bool _isPlayTime;

    private float _playTime;

    private int clockTime = 720;

    private void Start()
    {
        moneyCount.money = gameManager.money;
        _playTime = gameManager.time;

        ClockTime();
        StartCoroutine(TimerCor());
    }

    private IEnumerator TimerCor()
    {
        for (int i = 0; i < 36; i++)
        {
            yield return new WaitForSeconds(5f);

            clockTime += 15;
            ClockTime();
            _playTime += 3;
        }

        print("하루끝남");
        moneyCount.Calculate();
        gameManager.time = 0;
    }

    public void ClockTime()
    {
        int hour = clockTime / 60;
        int minute = clockTime % 60;

        clock.text = hour + ":" + minute;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void Open()
    {
        gameManager.saveData.days++;
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void MenuSuccess()
    {
        dayManager.cells += 30;
        //gameManager.money = moneyCount.money;
        //gameManager.time = _playTime;
        //moneyCount.SalesRamen();
        systemManager.Reset();
        DialogueData.instance.OrderEnd();
        Debug.Log("라멘 팔았다!");
        //SceneManager.LoadScene("GameScene");
    }

    public void SceneReset()
    {
        SceneManager.LoadScene("GameScene");
    }
}
