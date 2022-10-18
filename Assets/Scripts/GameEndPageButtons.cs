using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndPageButtons : MonoBehaviour
{
    GameObject ingameUI;
    private soundEffects SFX;

    // Start is called before the first frame update
    void Start()
    {
        ingameUI = GameObject.Find("InGameUI");
        SFX = GameObject.Find("SoundEffects").GetComponent<soundEffects>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void home()
    {
        Time.timeScale = 1;
        SFX.playMusic("switch6");
        ingameUI.GetComponent<InGameUI>().returnLevels().GetComponent<Levels>().loading.SetActive(true);
        ingameUI.GetComponent<InGameUI>().returnLevels().SetActive(true);
        SceneManager.LoadScene(2);

    }
    public void next()
    {
        Time.timeScale = 1;
        SFX.playMusic("switch6");
        ingameUI.GetComponent<InGameUI>().returnLevels().GetComponent<Levels>().loading.SetActive(true);
        ingameUI.GetComponent<InGameUI>().returnLevels().SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void restart()
    {
        Time.timeScale = 1;
        SFX.playMusic("switch6");
        ingameUI.GetComponent<InGameUI>().returnLevels().SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
