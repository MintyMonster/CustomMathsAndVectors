using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTank : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
            if (obj.transform.tag == "Tank")
                if (MathsCustomLibrary.Vector3.Distance(obj.transform.position, this.gameObject.transform.position) <= 1f)
                    Destroy(obj);
    
    }
}
