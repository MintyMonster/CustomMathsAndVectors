using UnityEngine;
using UnityEngine.AI;
public class MoveToDestination : MonoBehaviour {
    GameObject destination;
    NavMeshAgent agent;
    void Start() {
        destination = GameObject.Find( "Goal" );
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.transform.position);
    }

    private void OnTriggerEnter( Collider other ) {
        if ( other.CompareTag( "Goal" ) ) {
            WaveScript.score++;
            Destroy(gameObject);
        }
    }
}
