using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    GameObject chooseLevel;
    private soundEffects SFX;
    // Start is called before the first frame update
    void Start()
    {
        chooseLevel = GameObject.Find("ChooseLevel");
        SFX = GameObject.Find("SoundEffects").GetComponent<soundEffects>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {

        SceneManager.LoadScene(chooseLevel.GetComponent<chooseLevelScene>().getCurrentSelected()+2);
    }
    public void playMusic(string name)
    {
        SFX.playMusic(name);
    }
}
