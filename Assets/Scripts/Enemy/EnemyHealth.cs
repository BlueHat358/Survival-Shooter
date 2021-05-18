using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    //[SerializeField] private Item[] _itemPrefabs;

    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    //public Transform dropPoint;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;

    bool isDead;
    bool isSinking;


    void Awake() {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }


    void Update() {
        if (isSinking) {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(int amount, Vector3 hitPoint) {
        if (isDead)
            return;

        enemyAudio.Play();

        currentHealth -= amount;
        Debug.Log(currentHealth);

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0) {
            Death();
        }
    }

    /*private void randomDrop() {
        int randomIndex = Random.Range(0, _itemPrefabs.Length);
        Instantiate(_itemPrefabs[randomIndex].gameObject, dropPoint.position, dropPoint.rotation);
    }*/

    void Death() {
        //randomDrop();
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }

    public void StartSinking() {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}
