using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int health = 10; //total health of this obstacle
    [SerializeField] private int money = 50; //the amount of money you get after destory this obstacle
    [SerializeField] private GameObject placePrefab; //the "place" object will created after this obstacle being destoried
    private GameObject healthbarFill; 
    private GameObject healthbar;
    private GameObject inGameUI;
    private GameObject taging;
    private GameObject ClickUI;
    private soundEffects SFX;
    private GameObject thunder;
    private int currenthealth;   // the remaining helth
    private float healthX; // the length of the total health bar
    // Start is called before the first frame update
    void Start()
    {
        healthbar = transform.Find("HealthBar").gameObject;
        taging = transform.Find("taging").gameObject;
        healthbar.SetActive(false);
        taging.SetActive(false);
        thunder = transform.Find("thunder").gameObject;
        thunder.SetActive(false);
        healthbarFill = transform.Find("HealthBar").Find("Fill").gameObject;
        healthX = healthbarFill.transform.localScale.x;
        currenthealth = health;
        inGameUI = GameObject.Find("InGameUI");
        ClickUI = GameObject.Find("ClickUI");
        SFX = GameObject.Find("SoundEffects").GetComponent<soundEffects>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inGameUI.GetComponent<InGameUI>().tagging == gameObject)
        {
            taging.SetActive(true);
        }
        else
            taging.SetActive(false);
    }
    public void hurt(int damage)
    {
        if (!healthbar.activeSelf)
        {
            healthbar.SetActive(true);   
        }
        currenthealth -= damage;
        healthbarFill.transform.localScale = new Vector3(healthX / health * currenthealth, healthbarFill.transform.localScale.y);
        if (currenthealth <= 0)
        {
            GameObject newPlace = Instantiate(placePrefab);
            newPlace.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 1);
            inGameUI.GetComponent<InGameUI>().addMoney(money);
            SFX.playMusic("ObsDestory");
            Destroy(gameObject);
        }
    }
    private void OnMouseUp()
    {
        if (ClickUI.GetComponent<ClickUI>().isOverUI())
            return;
        if (inGameUI.GetComponent<InGameUI>().tagging == gameObject)
        {
            inGameUI.GetComponent<InGameUI>().tagging = null;
        }
        else
        {
            inGameUI.GetComponent<InGameUI>().tagging = gameObject;
            SFX.playMusic("tag");
        }
    }
    public void turnOnThunder()
    {
        thunder.SetActive(true);
    }
    public void turnOffThunder()
    {
        thunder.SetActive(false);
    }
}
