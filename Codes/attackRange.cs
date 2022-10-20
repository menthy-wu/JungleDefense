using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackRange : MonoBehaviour
{
    private Tower tower; 
    // Start is called before the first frame update
    void Start()
    {
        tower = gameObject.GetComponentInParent<Tower>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (tower.isThunder())
            {
                collision.gameObject.GetComponent<Mob1>().turnOnThunder();
            }
            tower.addMob(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {

            tower.remvoeMob(collision.gameObject);
            if (tower.isThunder())
            {
                collision.gameObject.GetComponent<Mob1>().turnOffThunder();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            tower.addObs(collision.gameObject);
            if (tower.isThunder())
                collision.gameObject.GetComponent<Obstacle>().turnOnThunder();
        }
    }
}
