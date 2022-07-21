using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TowerRotationAndShooting : MonoBehaviour
{

    public List<GameObject> tanks = new List<GameObject>();
    private GameObject target;
    private float timer;
    private float waitTime = 2f;

    public GameObject projectile;
    public GameObject tower;
    public GameObject shotPoint;
    [SerializeField] private float distance = 45f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float shotSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
            if (obj.transform.tag == "Tank")
                tanks.Add(obj);

        if (tanks.Count > 0)
        {
            target = tanks[0];
            tanks.ForEach(t => 
            {
                if (t != null)
                {
                    if (MathsCustomLibrary.Vector3.Distance(t.gameObject.transform.position, this.gameObject.transform.position) <= distance)
                        target = t.gameObject;
                }
            });
            RotateTowardsTarget(target);
            
            if(timer > waitTime)
            {
                timer = 0;
                Shoot(target);
            }

        }
        
    }

    private void RotateTowardsTarget(GameObject newTarget)
    {
        Transform newTower = tower.transform;
        MathsCustomLibrary.Vector3 targetDirection = newTarget.transform.position - newTower.position;
        targetDirection.y = 0;
        float step = speed * Time.deltaTime;
        MathsCustomLibrary.Vector3 newDirection = Vector3.RotateTowards(newTower.forward, targetDirection, step, 1.0f);
        newTower.rotation = Quaternion.LookRotation(newDirection);
    }

    private void Shoot(GameObject newTarget)
    {
        GameObject clone = Instantiate(projectile, shotPoint.transform.position, transform.rotation);
        clone.GetComponent<Rigidbody>().AddForce(transform.forward * 1500f);
        Destroy(clone, 2f);
    }
}
