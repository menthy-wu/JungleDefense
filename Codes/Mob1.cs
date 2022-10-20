using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob1 : MonoBehaviour
{
    [SerializeField] private int health = 10;    //the total health of this mob
    [SerializeField] private int money = 50; // how many money does the player gain after kill this mob
    [SerializeField] private float speed = 1; // the speed of this mob move
    [SerializeField] GameObject particle;
    private GameObject inGameUI;
    private GameObject mobSpawner;
    private GameObject taging;
    private GameObject healthbarFill;
    private GameObject ice;
    private GameObject fire;
    private GameObject thunder;
    private GameObject ClickUI;
    private GameObject mainCamera;
    private Rigidbody2D rb;
    private soundEffects SFX;
    private int currenthealth;   // the remaining helth of this mob
    private float healthX; // the length of the total health bar
    private float iceTimer = 0;
    private float fireTimer = 0;
    private float stopFreeze;  //how long does the frezzing effect stay on the mob
    private float stopFire;  //how long does the frezzing effect stay on the mob
    private Vector2 OriginalSpeed;  //the original speed vector of the mob without being effect by tower effect
    private bool isFreeze = false;
    private bool isFire = false;



    // Start is called before the first frame update
    void Start()
    {
        healthbarFill = transform.Find("HealthBar").Find("Fill").gameObject;
        healthX = healthbarFill.transform.localScale.x;
        currenthealth = health;
        ice = transform.Find("ice").gameObject;
        fire = transform.Find("fire").gameObject;
        thunder = transform.Find("thunder").gameObject;
        ice.SetActive(false);
        fire.SetActive(false);
        thunder.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * speed;
        OriginalSpeed = rb.velocity;
        taging = transform.Find("taging").gameObject;
        taging.SetActive(false);
        ClickUI = GameObject.Find("ClickUI");
        mainCamera = GameObject.Find("Main Camera");
        inGameUI = GameObject.Find("InGameUI");
        mobSpawner = GameObject.Find("MobSpawner");
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

        iceTimer += Time.deltaTime;
        fireTimer += Time.deltaTime;
        if (isFreeze)
        {
            if (iceTimer >= stopFreeze)
            {
                isFreeze = false;
                rb.velocity = OriginalSpeed;
                ice.SetActive(false);
            }
        }
        if (isFire)
        {
            if (fireTimer >= stopFire)
            {
                isFire = false;
                fire.SetActive(false);
            }
        }
    }
    public void hurt(int damage)
    {
        currenthealth -= damage;
        healthbarFill.transform.localScale = new Vector3(healthX / health * currenthealth, healthbarFill.transform.localScale.y);
        if (currenthealth <= 0)
        {
            inGameUI.GetComponent<InGameUI>().addMoney(money);
            mobSpawner.GetComponent<MobSpawner>().mobAmount--;
            Destroy(gameObject);
            Instantiate(particle, transform.position, Quaternion.identity);
            mainCamera.GetComponent<mainCamera>().Pop();
            int num = Random.Range(1, 3);
            SFX.playMusic("mobDie"+num.ToString());
        }
    }
    public void freeze(float rate, float time)
    {
        isFreeze = true;
        iceTimer = 0;
        stopFreeze = time;
        rb.velocity  = OriginalSpeed*rate;
        ice.SetActive(true);
    }
    public void setFire(int damage, float time)
    {
        isFire = true;
        fireTimer = 0;
        stopFire = time;
        this.OnFire();
        fire.SetActive(true);
    }
    private void OnFire()
    {
        if (isFire)
        {
            this.hurt(1);
            Invoke("OnFire", 1f);
        }
        else
        {
            return;
        }
    }
    private void changeDirection(string Direction)
    {
        if (Direction == "right")
        {
            OriginalSpeed = new Vector2(OriginalSpeed.y, -OriginalSpeed.x);
            rb.velocity = new Vector2(rb.velocity.y, -rb.velocity.x);
        }
        else if (Direction == "left")
        {
            OriginalSpeed = new Vector2(-OriginalSpeed.y, OriginalSpeed.x);
            rb.velocity = new Vector2(-rb.velocity.y, rb.velocity.x);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "RoutePoints")
        {
            if(collision.gameObject.GetComponent<RoutePoints>().direction == RoutePoints.Drection.left)
                changeDirection("left");
            else if(collision.gameObject.GetComponent<RoutePoints>().direction == RoutePoints.Drection.right)
                changeDirection("right");
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
