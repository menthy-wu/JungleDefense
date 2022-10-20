using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        normal,
        snow,
        fire
    }
    [SerializeField] private BulletType bulletType;
    [SerializeField] private float speed = 2f;   //the speed of the bullet flies
    [SerializeField] private int damage = 1;   //the damage made by the bullet
    [SerializeField] float freezetime = 5f; // only for snow bullet, the time ice stay on the mob
    [SerializeField] float freezeSpeed = 0.7f; // only for snow bullet, the rate of the speed of the 
    [SerializeField] GameObject particle;
    float firetime = 5f; // only for snow bullet, the time ice stay on the mob
    int fireDamage = 1; // only for snow bullet, the rate of the speed of the 
    private GameObject target;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x<= -26 || transform.position.x >= 26 || transform.position.y >= 12 || transform.position.x <= -12)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            if (collision.tag == "Obstacle")
            {
                collision.gameObject.GetComponent<Obstacle>().hurt(damage);
                Instantiate(particle, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            if (collision.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Mob1>().hurt(damage);
                if (bulletType == BulletType.snow)
                {
                    collision.gameObject.GetComponent<Mob1>().freeze(freezeSpeed, freezetime);
                }
                else if (bulletType == BulletType.fire)
                {
                    collision.gameObject.GetComponent<Mob1>().setFire(fireDamage, firetime);
                }
                Destroy(gameObject);
                Instantiate(particle, transform.position, Quaternion.identity);
            }
        }
    }
    public void setTarget(GameObject Target)
    {
        target = Target;
    }
    public void setDamage(int dam)
    {
        damage = dam;
    }
    public void setFrozonSpeed(float amount)
    {
        freezeSpeed = amount;
    }
}
