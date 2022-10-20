using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    [SerializeField] private int health = 10;    //the total health
    private GameObject healthbarFill;
    private GameObject mobSpawner;
    private GameObject healthbar;
    private GameObject mainCamera;
    private GameObject inGameUI;
    private soundEffects SFX;
    private int currenthealth;   // the remaining helth
    private float healthX; // the length of the total health bar
    // Start is called before the first frame update
    void Start()
    {

        healthbar = transform.Find("HealthBar").gameObject;
        healthbar.SetActive(false);
        healthbarFill = transform.Find("HealthBar").Find("Fill").gameObject;
        healthX = healthbarFill.transform.localScale.x;
        currenthealth = health;
        mobSpawner = GameObject.Find("MobSpawner");
        mainCamera = GameObject.Find("Main Camera");
        inGameUI = GameObject.Find("InGameUI");
        SFX = GameObject.Find("SoundEffects").GetComponent<soundEffects>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void hurt(int damage)
    {
        SFX.playMusic("endHurt");
        if (!healthbar.activeSelf)
        {
            healthbar.SetActive(true);
        }
        currenthealth -= damage;
        mainCamera.GetComponent<mainCamera>().shake();
        healthbarFill.transform.localScale = new Vector3(healthX / health * currenthealth, healthbarFill.transform.localScale.y);
        if (currenthealth <= 0)
        {
            Destroy(gameObject);
            inGameUI.GetComponent<InGameUI>().loss();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            this.hurt(1);
            Destroy(collision.gameObject);
            mobSpawner.GetComponent<MobSpawner>().mobAmount--;
        }
    }
    public int getHealth()
    {
        return currenthealth;
    }
}
