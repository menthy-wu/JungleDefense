using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] int time;
    GameObject mobSpawner;
    TMP_Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        timerText = gameObject.GetComponent<TMP_Text>();
        StartCoroutine("Time");
        mobSpawner = GameObject.Find("MobSpawner");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Time()
    {
        for (int i = 0; i <= time; i++)
        {
            timerText.text = "Mobs will come in " + (time-i).ToString()+ " seconds";
            yield return new WaitForSeconds(1f);
        }
        mobSpawner.GetComponent<MobSpawner>().startSpawn();
        gameObject.SetActive(false);
    }
}
