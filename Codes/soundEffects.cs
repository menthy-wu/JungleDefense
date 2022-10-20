using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffects : MonoBehaviour
{
    AudioSource buttonClick;
    AudioSource buttonTrigger;
    AudioSource loss;
    AudioSource mobDie2;
    AudioSource mobDie1;
    AudioSource shoot;
    AudioSource switch6;
    AudioSource win;
    AudioSource tagSFX;
    AudioSource ObsDestory;
    AudioSource endHurt;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        buttonClick = transform.Find("buttonClick").gameObject.GetComponent<AudioSource>();
        buttonTrigger = transform.Find("buttonTrigger").gameObject.GetComponent<AudioSource>();
        loss = transform.Find("loss").gameObject.GetComponent<AudioSource>();
        mobDie2 = transform.Find("mobDie2").gameObject.GetComponent<AudioSource>();
        mobDie1 = transform.Find("mobDie1").gameObject.GetComponent<AudioSource>();
        shoot = transform.Find("shoot").gameObject.GetComponent<AudioSource>();
        switch6 = transform.Find("switch6").gameObject.GetComponent<AudioSource>();
        win = transform.Find("win").gameObject.GetComponent<AudioSource>();
        tagSFX = transform.Find("tagSFX").gameObject.GetComponent<AudioSource>();
        ObsDestory = transform.Find("ObsDestory").gameObject.GetComponent<AudioSource>();
        endHurt = transform.Find("endHurt").gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playMusic(string audioName)
    {
        if (audioName == "buttonClick")
            buttonClick.Play();
        else if (audioName == "buttonTrigger")
            buttonTrigger.Play();
        else if (audioName == "loss")
            loss.Play();
        else if (audioName == "mobDie2")
            mobDie2.Play();
        else if (audioName == "mobDie1")
            mobDie1.Play();
        else if (audioName == "shoot")
            shoot.Play();
        else if (audioName == "switch6")
            switch6.Play();
        else if (audioName == "win")
            win.Play();
        else if (audioName == "tagSFX")
            tagSFX.Play();
        else if (audioName == "ObsDestory")
            ObsDestory.Play();
        else if (audioName == "endHurt")
            endHurt.Play();
    }
}
