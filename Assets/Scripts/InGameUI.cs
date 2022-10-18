using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    public int Money = 100;
    int CurrentWave;
    int TotalWaves;
    int starNum = 1;
    public GameObject tagging = null;
    GameObject MoneyText;
    GameObject MoneyText1;
    GameObject WaveText;
    GameObject GameEndPage;
    GameObject GameOver;
    GameObject WinPage;
    GameObject End;
    GameObject levels;
    GameObject level;
    GameObject TowerIntro;
    GameObject Timer;
    GameObject LevelTitle;
    GameObject Star1;
    GameObject Star2;
    [SerializeField] int levelNum;
    soundEffects SFX;
    // Start is called before the first frame update
    void Start()
    {
        levels = GameObject.Find("levels");
        level = levels.transform.Find("Level" + levelNum.ToString()).gameObject;
        levels.SetActive(false);
        levels.GetComponent<Levels>().loading.SetActive(false);
        MoneyText1 = transform.Find("Money").Find("MoneyText1").gameObject;
        MoneyText = transform.Find("Money").Find("MoneyText").gameObject;
        MoneyText.GetComponent<TMP_Text>().text = Money.ToString();
        MoneyText1.GetComponent<TMP_Text>().text = Money.ToString();
        WaveText = transform.Find("Waves").gameObject;
        GameEndPage = GameObject.Find("GameEndPage");
        GameOver = GameEndPage.transform.Find("GameOver").gameObject;
        WinPage = GameEndPage.transform.Find("Win").gameObject;
        GameEndPage.SetActive(false);
        End = GameObject.Find("end");
        TowerIntro = GameObject.Find("TowerIntro");
        Timer = transform.Find("timer").gameObject;
        LevelTitle = transform.Find("LevelTitle").gameObject;
        SFX = GameObject.Find("SoundEffects").GetComponent<soundEffects>();
        Star1 = GameEndPage.transform.Find("Win").Find("Star2").Find("StarYellow").gameObject;
        Star2 = GameEndPage.transform.Find("Win").Find("Star3").Find("StarYellow").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void addMoney(int amount)
    {
        Money += amount;
        changeMoneyText();
    }
    public bool pay(int amount)
    {
        if (amount > Money)
            return false;
        Money -= amount;
        changeMoneyText();
        return true;
    }
    private void changeMoneyText()
    {
        MoneyText.GetComponent<TMP_Text>().text = Money.ToString();
        MoneyText1.GetComponent<TMP_Text>().text = Money.ToString();
    }
    public void changeWaveText()
    {
        WaveText.GetComponent<TMP_Text>().text = " WAVES " + CurrentWave.ToString() + "/" + TotalWaves.ToString();
    }
    public void setCurrentWaves(int wave)
    {
        CurrentWave = wave;
    }
    public void setTotalWave(int wave)
    {
        TotalWaves = wave;
    }
    public void win()
    {
        SFX.playMusic("win");
        if (End.GetComponent<End>().getHealth() > 5)
        {
            Star1.SetActive(true);
            starNum = 2;
            if (End.GetComponent<End>().getHealth() == 10)
            {
                Star2.SetActive(true);
                starNum = 3;
            }
        }
        GameEndPage.SetActive(true);
        WinPage.SetActive(true);
        level.GetComponent<LevelButton>().setStar(starNum);
        level.GetComponent<LevelButton>().setPlayed(true);
        if (levelNum != 8)
            levels.transform.Find("Level" + (levelNum + 1).ToString()).GetComponent<Button>().interactable = true;
        Invoke("pause", 1f);
    }
    public void loss()
    {
        SFX.playMusic("loss");
        GameEndPage.SetActive(true);
        GameOver.SetActive(true);
        Invoke("pause", 1f);
    }
    public GameObject returnLevels()
    {
        return levels;
    }
    public void gameStart()
    {
        TowerIntro.SetActive(false);
        Timer.SetActive(true);
        LevelTitle.SetActive(true);
    }
    public void pause()
    {
        Time.timeScale = 0;
    }
    public void resume()
    {
        Time.timeScale = 1;
    }
}
