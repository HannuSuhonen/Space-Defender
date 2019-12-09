using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //config param
    [SerializeField] int health = 200;
    [SerializeField] float playerMoveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] GameObject shootingLaserPrefab;
    [SerializeField] float laserShootingSpeed = 15f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] GameObject smokePrefab;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float explosionTime = 1f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    Coroutine firingCoroutine;
    Level level;
    
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        level = FindObjectOfType<Level>();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        fire();
    }

    private void fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuosly());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }
    
    IEnumerator FireContinuosly()
    {
        while (true)
        {
            GameObject laser = Instantiate(shootingLaserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserShootingSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    private void Move()
    {
        // Calculate new position and Clamp to screen boundary.
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerMoveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerMoveSpeed;

        var newXPos = Mathf.Clamp(deltaX + transform.position.x,xMin + padding,xMax - padding);
        var newYPos = Mathf.Clamp(deltaY + transform.position.y,yMin + padding,yMax - padding);
        transform.position = new Vector2(newXPos, newYPos);
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
        smokePrefab.SetActive(true);
        damageDealer.Hit();
        if (health <= 0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        
        Destroy(gameObject);
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, explosionTime);
        level.LoadGameOver();
    }
}
