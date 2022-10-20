using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class towerButton : MonoBehaviour
{
    private GameObject place;
    private GameObject inGameUI;
    private GameObject ClickUI;
    private GameObject priceText;
    [SerializeField] public int price;
    soundEffects SFX;
    // Start is called before the first frame update
    void Start()
    {
        inGameUI = GameObject.Find("InGameUI");
        ClickUI = GameObject.Find("ClickUI");
        priceText = transform.Find("PriceTag").Find("Price").gameObject;
        SFX = GameObject.Find("SoundEffects").GetComponent<soundEffects>();
    }

    // Update is called once per frame
    void Update()
    {

        if (inGameUI.GetComponent<InGameUI>().Money < price && gameObject.name != "remove")
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
        else
            gameObject.GetComponent<Button>().interactable = true;
    }
    public void chooseTower(GameObject towerPrefab)
    {
        SFX.playMusic("switch6");
        GameObject tower = Instantiate(towerPrefab);
        inGameUI.GetComponent<InGameUI>().pay(price);
        tower.transform.position = new Vector3(place.transform.position.x, place.transform.position.y, 1);
        place.GetComponent<Place>().FilledWithTower();
        return;
    }
    public void upgrade()
    {
        SFX.playMusic("switch6");
        inGameUI.GetComponent<InGameUI>().pay(price);
        place.GetComponent<Tower>().leveUp();
    }
    public void remove(GameObject placePrefab)
    {
        SFX.playMusic("switch6");
        GameObject newPlace = Instantiate(placePrefab);
        newPlace.transform.position = new Vector3(place.transform.position.x, place.transform.position.y, 1);
        inGameUI.GetComponent<InGameUI>().addMoney(price);
        this.place.GetComponent<Tower>().remove();
        ClickUI.GetComponent<ClickUI>().hideTowerUI();
    }
    public void setPlace(GameObject Place)
    {
        gameObject.GetComponent<Button>().interactable = true;
        if (Place.tag == "Tower")
        {
            if (gameObject.name == "Upgrade")
            {
                if (Place.GetComponent<Tower>().getMax())
                {
                    gameObject.GetComponent<Button>().interactable = false;
                    priceText.SetActive(false);
                }
                else
                {

                    priceText.SetActive(true);
                }
            }
            if (gameObject.name == "remove")
            {
                changePrice(Place.GetComponent<Tower>().returnSellPrice());
            }
        }
        place = Place;
    }
    public void changePrice(int amount)
    {
        if (priceText == null)
        priceText = transform.Find("PriceTag").Find("Price").gameObject;
        price = amount;
        priceText.GetComponent<TMP_Text>().text = price.ToString();
    }
    public GameObject getPlace()
    {
        return place;
    }
}
