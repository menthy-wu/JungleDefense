using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public enum TowerType
    {
        normal, 
        snow,
        fire,
        thunder
    }

    [SerializeField] private float shootSpeed = 1.5f;      //the time betwee each shot
    [SerializeField] private GameObject projectile;  //projectile prefab
    [SerializeField] private int damage;     //the first level damage
    [SerializeField] private int damageIncrease;  //the amont of damage increase each time being upgraded
    [SerializeField] private int price;    //the first price take to buy the tower
    [SerializeField] private int sellPrice; // the money you get if you remove the level I tower
    [SerializeField] private int sellPrice_II;// the money you get if you remove the level II tower
    [SerializeField] private int sellPrice_III;// the money you get if you remove the level III tower
    [SerializeField] private int currentUpgrade;//the price take to upgrade the tower to level II
    [SerializeField] private int nextUpgradePrice;//the price take to upgrade the tower to level III
    [SerializeField] private TowerType towerType;
    private GameObject tower;//the shooting part of the tower
    private GameObject inGameUI;
    private GameObject ClickUI;
    private GameObject attackRange;
    private GameObject target = null;    //the mob or obstacle under attach currently
    private GameObject end;
    private soundEffects SFX;
    private List<GameObject> mobList; //all the mobs in the attach range
    private List<GameObject> obsList; //all the obstacles in the attach range
    private Animator animator;     //animator of the shooting part of the tower
    private Animator baseAnimator;//animator of the base of the tower
    private float elaspseTime;    //a timer for the time between each shot
    private bool selected = false;//if the Click UI is showing on this tower
    private bool Max = false;//if the tower is at the highest level
    private float FrozeTime = 0.7f;
   // private bool ThunderStarted = false;
    // Start is called before the first frame update

    void Start()
    {
        ClickUI = GameObject.Find("ClickUI");
        SFX = GameObject.Find("SoundEffects").GetComponent<soundEffects>();
        inGameUI = GameObject.Find("InGameUI");
        end = GameObject.Find("end");
        mobList = new List<GameObject>();
        obsList = new List<GameObject>();
        tower = transform.GetChild(0).gameObject;
        animator = tower.GetComponent<Animator>();
        baseAnimator = transform.GetChild(1).gameObject.GetComponent<Animator>();
        attackRange = transform.Find("AttackRange").gameObject;
        attackRange.transform.Find("img").gameObject.SetActive(false);
        if(towerType == TowerType.thunder)
            StartCoroutine("thunderShoot");
    }

    // Update is called once per frame
    void Update()
    {
        elaspseTime += Time.deltaTime;
        if (towerType != TowerType.thunder)
        {
            setTarget();
            if (target != null)
            {
                faceTowards(target);
                if (elaspseTime > shootSpeed)
                {
                    shoot();
                    elaspseTime = 0;
                }
            }
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && animator.GetBool("shoot") && animator.GetCurrentAnimatorStateInfo(0).IsTag("shoot"))
            {
                animator.SetBool("shoot", false);
            }
        }
        if (!selected)
        {
            attackRange.transform.Find("img").gameObject.SetActive(false);
        }
    }
    private void faceTowards(GameObject Target) 
    {
        Vector2 direction = Target.transform.position - transform.position;
        tower.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }

    private void shoot()
    {
        SFX.playMusic("shoot");
        animator.SetBool("shoot", true);
        GameObject projectilePrefab = Instantiate(projectile);
        projectilePrefab.transform.position = transform.GetChild(0).GetChild(0).position;
        projectilePrefab.transform.rotation = transform.GetChild(0).GetChild(0).rotation;
        projectilePrefab.GetComponent<Bullet>().setTarget(target);
        projectilePrefab.GetComponent<Bullet>().setDamage(damage);
        projectilePrefab.GetComponent<Bullet>().setFrozonSpeed(FrozeTime);
    }
    IEnumerator thunderShoot()
    {
        while (true)
        {
            foreach (GameObject mob in mobList)
            {
                if (mob != null)
                mob.GetComponent<Mob1>().hurt(damage);
            }
            foreach (GameObject obs in obsList)
            {
                if (obs != null)
                {
                    obs.GetComponent<Obstacle>().hurt(damage);
                }
            }
            yield return new WaitForSeconds(2f);
        }
    }

    public void leveUp()
    {
        if (animator.GetInteger("level") < 3)
        {
            animator.SetInteger("level", animator.GetInteger("level") + 1);
            baseAnimator.SetInteger("level", baseAnimator.GetInteger("level") + 1);
            if (towerType == TowerType.thunder)
            {

                attackRange.transform.localScale *= 1.8f;
            }
            else
                attackRange.transform.localScale *= 1.1f;
            shootSpeed *= 0.7f;
            damage+= damageIncrease;
            if (towerType == TowerType.snow)
            {
                FrozeTime -= 0.1f;
            }
            if (animator.GetInteger("level") == 2)
            {
                currentUpgrade = nextUpgradePrice;
                sellPrice = sellPrice_II;
                ClickUI.GetComponent<ClickUI>().getTowerUI().transform.GetComponentsInChildren<towerButton>()[0].changePrice(currentUpgrade);
                ClickUI.GetComponent<ClickUI>().getTowerUI().transform.GetComponentsInChildren<towerButton>()[1].changePrice(sellPrice);
            }
            else if (animator.GetInteger("level") >= 3)
            {
                sellPrice = sellPrice_III;
                Max = true;
                ClickUI.GetComponent<ClickUI>().getTowerUI().transform.GetComponentsInChildren<towerButton>()[1].changePrice(sellPrice);
                ClickUI.GetComponent<ClickUI>().getTowerUI().transform.GetComponentsInChildren<towerButton>()[0].gameObject.GetComponent<Button>().interactable = false;
            }
        }
    }
    private void OnMouseUp()
    {
        if (!ClickUI.GetComponent<ClickUI>().isOverUI())
        {
            if (selected)
            {
                selected = false;
                ClickUI.GetComponent<ClickUI>().hideTowerUI();
                return;
            }
            selected = true;
            attackRange.transform.Find("img").gameObject.SetActive(true);
            ClickUI.GetComponent<ClickUI>().showTowerUI();
            ClickUI.transform.position = new Vector3(transform.position.x, transform.position.y, ClickUI.transform.position.z);
            ClickUI.GetComponent<ClickUI>().getTowerUI().transform.GetComponentsInChildren<towerButton>()[0].changePrice(currentUpgrade);
            if (ClickUI.GetComponent<ClickUI>().getTowerUI().transform.GetComponentsInChildren<towerButton>()[0].getPlace() != null)
            {
                ClickUI.GetComponent<ClickUI>().getTowerUI().transform.GetComponentsInChildren<towerButton>()[0].getPlace().GetComponent<Tower>().deSelect();
            }
            foreach (towerButton TowerButton in ClickUI.GetComponent<ClickUI>().getTowerUI().transform.GetComponentsInChildren<towerButton>())
            {
                TowerButton.setPlace(gameObject);
            }
        }
    }
    private void setTarget()
    {
        target = null;
        if (inGameUI.GetComponent<InGameUI>().tagging!=null && (mobList.Contains(inGameUI.GetComponent<InGameUI>().tagging) || obsList.Contains(inGameUI.GetComponent<InGameUI>().tagging))) //if there's any thing under tagging which is in the attact range of the tower
            target = inGameUI.GetComponent<InGameUI>().tagging;
        else if (mobList.Count > 0)
        {
            target = findTheClosest();
        }
    }
    private GameObject findTheClosest()
    {
        GameObject closest = mobList[0];
        foreach (GameObject obj in mobList)
        {
            if ((obj.transform.position - end.transform.position).magnitude < (closest.transform.position - end.transform.position).magnitude)
            {
                closest = obj;
            }
        }
        return closest;
    }
    public void remove()
    {
        if (towerType == TowerType.thunder)
        {
            foreach(GameObject obs in obsList)
                obs.GetComponent<Obstacle>().turnOffThunder();
        }
        Destroy(gameObject);
    }
    public void addMob(GameObject mob)
    {
        mobList.Add(mob);
    }
    public void remvoeMob(GameObject mob)
    {
        mobList.Remove(mob);
    }
    public GameObject getTarget()
    {
        return  target;
    }
    public void addObs(GameObject obs)
    {
        if(!obsList.Contains(obs))
        obsList.Add(obs);
    }
    public int returnUpgradePrice()
    {
        return currentUpgrade;
    }
    public int returnSellPrice()
    {
        return sellPrice;
    }
    public void deSelect()
    {
        selected = false;
    }
    public bool getMax()
    {
        return Max;
    }
    public bool  isThunder()
    {
        return towerType == TowerType.thunder;
    }
}
