using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    GameObject levels;
    GameObject SFX;
    // Start is called before the first frame update
    void Start()
    {
        levels = GameObject.Find("levels");
        SFX = GameObject.Find("SoundEffects");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void back()
    {
        SceneManager.LoadScene(0);
        Destroy(levels);
        Destroy(SFX);
    }
}
