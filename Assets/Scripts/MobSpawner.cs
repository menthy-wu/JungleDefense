using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private float gapTime;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] Wave[] waves;
    GameObject inGameUI;
    public int mobAmount = 0;
    bool allMobOut = false;
    // Start is called before the first frame update
    void Start()
    {
        inGameUI = GameObject.Find("InGameUI");
        inGameUI.GetComponent<InGameUI>().setTotalWave(waves.Length);
        spawnPoint.gameObject.SetActive(false);
    } 

    // Update is called once per frame
    void Update()
    {
        if (allMobOut && mobAmount <= 0)
        {
            allMobOut = false;
            inGameUI.GetComponent<InGameUI>().win();
        }

    }
    IEnumerator SpawerEnemy()
    {
        for (int waveIn = 0; waveIn < waves.Length; waveIn++)
        {
            inGameUI.GetComponent<InGameUI>().setCurrentWaves(waveIn+1);
            inGameUI.GetComponent<InGameUI>().changeWaveText();
            Wave wave = waves[waveIn];
            for (int i = 0; i < wave.amount; i++)
            {
                GameObject.Instantiate(wave.mob, spawnPoint);
                mobAmount++;
                /*
                if (i < waves.Length - 1)
                {
                    yield return new WaitForSeconds(gapTime);
                }*/
                yield return new WaitForSeconds(gapTime);
            }
            while (mobAmount > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(3f);
        }
        allMobOut = true;
    }
    public void startSpawn()
    {
        spawnPoint.gameObject.SetActive(true);
        StartCoroutine("SpawerEnemy");
    }
}
