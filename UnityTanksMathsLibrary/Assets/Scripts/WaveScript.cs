using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaveScript : MonoBehaviour
{
    public GameObject tank;
    public float waveTimer = 15, numInWave = 5;
    public static int score;
    public TextMeshProUGUI text;
    TowerScript[] towers;
    void Start()
    {
        StartCoroutine( "SpawnTanks" );
        towers = TowerScript.FindObjectsOfType<TowerScript>();
    }
    private void Update() {
        text.text = "Score: " + score;
    }
    IEnumerator SpawnTanks()
    {
        while ( true ) {
            for ( int i = 0; i < numInWave; i++ ) {
                Instantiate( tank, transform.position + ( Vector3.up * 0.5f ), transform.rotation );
                yield return new WaitForSeconds( 0.5f );
            }
            foreach ( TowerScript tower in towers ) {
                tower.UpdateTankList();
            }
            yield return new WaitForSeconds( waveTimer );
        }
    }
}