using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    public NavMeshAgent agent;
    private GameObject destinationTarget;

    private void Start()
    {
        destinationTarget = GameObject.FindGameObjectWithTag("Destination");
        MathsCustomLibrary.Vector3 destination = destinationTarget.transform.position;
        agent.SetDestination(destination);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destination"))
            Destroy(this);
    }
}
