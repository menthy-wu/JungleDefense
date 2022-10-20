using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour
{
    bool selected = false;
    private GameObject ClickUI;
    // Start is called before the first frame update
    void Start()
    {
        ClickUI = GameObject.Find("ClickUI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseUp()
    {
        if(selected)
        {
            selected = false;
            ClickUI.GetComponent<ClickUI>().hideChooseTowerUI();
            return;
        }
        // if(chooseTowerUI.activeSelf == false)
        if (!ClickUI.GetComponent<ClickUI>().isOverUI())
        {
            selected = true;
            ClickUI.GetComponent<ClickUI>().showChooseTowerUI();
            ClickUI.transform.position = new Vector3(transform.position.x, transform.position.y, ClickUI.transform.position.z);
            if (ClickUI.GetComponent<ClickUI>().getTowerUI().transform.GetComponentsInChildren<towerButton>()[0].getPlace() != null)
            {
                ClickUI.GetComponent<ClickUI>().getTowerUI().transform.GetComponentsInChildren<towerButton>()[0].getPlace().GetComponent<Tower>().deSelect();
            }
            foreach (towerButton TowerButton in ClickUI.GetComponent<ClickUI>().getChooseUI().transform.GetComponentsInChildren<towerButton>())
            {
                TowerButton.setPlace(gameObject);
            }
        }
    }
    public void FilledWithTower()
    {
        ClickUI.GetComponent<ClickUI>().hideChooseTowerUI();
        Destroy(gameObject);
    }
}
