using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    Player player;
    [SerializeField] float powerUps = 0.5f;
    float powerUpIncrement = 2f;

    private void Start()
    {
       player = FindObjectOfType<Player>();
    }
    public void Hit()
    {
        Destroy(gameObject);
    }

    public float GetPowerUp()
    {
        return powerUps;
    }
}
