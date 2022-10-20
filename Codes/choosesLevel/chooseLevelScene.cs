using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class chooseLevelScene : MonoBehaviour
{
    private int currentSelected = 1;
    private GameObject levels;
    [SerializeField]    TMP_Text levelText;
    [SerializeField] Animator levelimg;
    // Start is called before the first frame update
    void Start()
    {
        levels = GameObject.Find("levels");
        DontDestroyOnLoad(levels);
        levels.GetComponent<Levels>().loading.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setCurrentSelected(int num)
    {
        currentSelected = num;
        levelText.text = "level " + currentSelected.ToString();
        levelimg.SetInteger("level", currentSelected);
    }
    public int  getCurrentSelected()
    {
        return currentSelected;
    }
}
