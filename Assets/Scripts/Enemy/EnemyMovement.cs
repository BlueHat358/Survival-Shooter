using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    private void Awake (){
        // cari gameobject dengan tag player
        player = GameObject.FindGameObjectWithTag ("Player").transform;

        // mendapatkan reference component
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }


    void Update (){
        // memindahkan posisi player
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0){
            nav.SetDestination (player.position);
        }else{
            nav.enabled = false;
        }
    }
}
