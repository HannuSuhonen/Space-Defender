﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3.0f;
    [SerializeField] GameObject shootingLaserPrefab;
    [SerializeField] float laserShootingSpeed = 15f;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject damagePrefab;
    [SerializeField] int scoreValue = 100;
    [SerializeField] GameObject itemDropPrefab;
    [SerializeField] float itemDropSpeed = 10f;
    [SerializeField] bool dropItem = false;
    [SerializeField] bool fireLaser = true;
    GameSession gameSession;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots); //This is called for each enemy.
    }

    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void fire()
    {
        if (fireLaser)
        {
            GameObject laser = Instantiate(shootingLaserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserShootingSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            gameSession.ScoreCounter(scoreValue);
            EnemyDie();
        }
        else if(health > 100)
        {
            SustainDamage();
        }
    }

    private void SustainDamage()
    {
        GameObject damage = Instantiate(damagePrefab, transform.position, Quaternion.identity);
        Destroy(damage, 1f);
    }

    private void EnemyDie()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        
        Destroy(explosion, 1f);
        ItemDrop();
    }

    private void ItemDrop()
    {
        if (dropItem)
        {
            GameObject powerUp = Instantiate(itemDropPrefab, transform.position, Quaternion.identity) as GameObject;
            powerUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -itemDropSpeed);
        }
    }
}
