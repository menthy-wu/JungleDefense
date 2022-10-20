using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ClickUI : MonoBehaviour
{
    private GameObject ChooseTowerUI;
    private GameObject TowerUI;
    // Start is called before the first frame update
    void Start()
    {
        ChooseTowerUI = transform.GetChild(0).gameObject;
        TowerUI = transform.GetChild(1).gameObject;
        ChooseTowerUI.SetActive(false);
        TowerUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool isOverUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }
    public void hideChooseTowerUI()
    {
        ChooseTowerUI.SetActive(false);
    }
    public void hideTowerUI()
    {
        TowerUI.SetActive(false);
    }
    public void showChooseTowerUI()
    {
        TowerUI.SetActive(false);
        ChooseTowerUI.SetActive(true);
    }
    public void showTowerUI()
    {
        ChooseTowerUI.SetActive(false);
        TowerUI.SetActive(true);
    }
    public GameObject getTowerUI()
    {
        return TowerUI;
    }
    public GameObject getChooseUI()
    {
        return ChooseTowerUI;
    }
}
