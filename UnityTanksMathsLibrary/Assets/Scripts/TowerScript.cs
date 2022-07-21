using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public float awarenessRadius, fireRate, rotateSpeed, accuracy, projectileForce;
    float cooldown = 0f;
    public GameObject projectile;
    Transform nozzle;
    public List<GameObject> tanks = new List<GameObject>();
    GameObject target;
    void Start()
    {
        nozzle = transform.Find("Nozzle");
        UpdateTankList();
        StartCoroutine("UpdateTower");
    }

    private void OnDrawGizmos()
    {  //show if tank is in range
        bool isInside = target;
        Gizmos.color = isInside ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, awarenessRadius);
    }

    //At the start and when new tanks spawn, they will call this method to be added to the list
    public void UpdateTankList()
    {
        tanks.Clear();
        foreach (GameObject tank in GameObject.FindGameObjectsWithTag("Tank"))
        {
            tanks.Add(tank);
        }
    }
    IEnumerator UpdateTower()
    { //update what tank to target
        while (true)
        {
            //find the closest tank
            target = FindClosestTank();
            if (target != null)
            {
                Debug.Log("Targetting: " + target.name);
            }
            else
            {
                Debug.Log("No Target");
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Update()
    {
        if (target != null)
        {
            //aim towards target
            MathsCustomLibrary.Vector3 targetDirection = target.transform.position - transform.position + Vector3.up;
            float step = rotateSpeed * Time.deltaTime;
            MathsCustomLibrary.Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0f);
            Debug.DrawRay(transform.position, newDirection, Color.red);
            transform.rotation = Quaternion.LookRotation(newDirection);
            //if you're looking at it,
            if (MathsCustomLibrary.Vector3.Dot(transform.forward, targetDirection.normalized()) > accuracy)
            {
                if (cooldown >= 1f)
                {
                    //Shoot
                    GameObject clone = Instantiate(projectile, nozzle.position, transform.rotation);
                    Destroy(clone, 3f);
                    clone.GetComponent<Rigidbody>().AddForce(transform.forward * projectileForce);
                    cooldown = 0f;
                }
            }
        }
        cooldown += fireRate * Time.deltaTime;
    }
    public GameObject FindClosestTank()
    {
        GameObject closestTank = null;
        if (tanks.Count > 0)
        {
            float closest = 100f;
            foreach (GameObject tank in tanks)
            {
                if (tank != null)
                {
                    float distance = MathsCustomLibrary.Vector3.Distance(transform.position, tank.transform.position);
                    if (distance < awarenessRadius && distance < closest)
                    {
                        closest = distance;
                        closestTank = tank;
                    }
                }
            }
        }
        return closestTank;
    }
}
