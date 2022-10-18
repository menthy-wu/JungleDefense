using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] int level;
    GameObject chooseLevel;
    GameObject stars;
    GameObject star1;
    GameObject star2;
    GameObject star3;
    private soundEffects SFX;
    int star = 0;
    bool played = false;
    // Start is called before the first frame update
    void Start()
    {
        stars = transform.Find("stars").gameObject;
        if (level != 1)
            gameObject.GetComponent<Button>().interactable = false;
        if (!played)
        {
            stars.SetActive(false);
        }
        star1 = stars.transform.Find("Star1").Find("StarYellow").gameObject;
        star2 = stars.transform.Find("Star2").Find("StarYellow").gameObject;
        star3 = stars.transform.Find("Star3").Find("StarYellow").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void LevelButtonPress()
    {
        chooseLevel = GameObject.Find("ChooseLevel");
        chooseLevel.GetComponent<chooseLevelScene>().setCurrentSelected(level);
        SFX = GameObject.Find("SoundEffects").GetComponent<soundEffects>();
        SFX.playMusic("buttonClick");
    }
    public void setStar(int num)
    {
        star = num;
        star2.SetActive(true);
        if (num >= 2)
        {
            star1.SetActive(true);
            if (num == 3)
            {
                star3.SetActive(true);
            }
        }
    }
    public void setPlayed(bool state)
    {
        played = state;
        stars.SetActive(true);
    }
    public int getStar()
    {
        return star;
    }
}
