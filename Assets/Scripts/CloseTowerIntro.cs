using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTowerIntro : MonoBehaviour
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
    public void closeUI()
    {
        ingameUI.GetComponent<InGameUI>().gameStart();
    }
    public void playMusic(string name)
    {
        SFX.playMusic(name);
    }
}
