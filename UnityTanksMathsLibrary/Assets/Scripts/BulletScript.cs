using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Tank")
        {
            GameObject tank = collision.gameObject;
            float health = tank.GetComponent<TankStats>().health;
            health = health - 5f;
            tank.GetComponent<TankStats>().health = health;
            Debug.Log(health);
            Destroy(gameObject);
        }
    }
}
