using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100f;
    [SerializeField] int scoreValue = 150;

    [Header("Shooting")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float enemyLaserSpeed = 10f;
    [SerializeField] GameObject enemyLaser;
    [SerializeField] GameObject deathVFX;

    [Header("Sound FX")]
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioSource enemyExplosionSound;
    [SerializeField] AudioSource enemyShootSound;
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.5f;
    [SerializeField] [Range(0,1)] float shootingVolume = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        ResetShotCounter();
    }

    private void ResetShotCounter()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            ResetShotCounter();
        }
    }

    private void Fire()
    {
        GameObject projectile = Instantiate(enemyLaser, gameObject.transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -enemyLaserSpeed);
        AudioSource.PlayClipAtPoint(enemyShootSound.clip, Camera.main.transform.position, shootingVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        ParticleEffect();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(enemyExplosionSound.clip, Camera.main.transform.position, deathSoundVolume);
    }

    private void ParticleEffect()
    {
        GameObject particle = Instantiate(deathVFX, gameObject.transform.position, Quaternion.identity);
        Destroy(particle, durationOfExplosion);
    }
}
