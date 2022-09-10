using InfimaGames.LowPolyShooterPack;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatesHolder : MonoBehaviour
{
    public int maxHealthPoints = 3;
    public int currentHealth = 3;
    public static Action EventMinusPlayerHp { get; set; }
    public static Action EventToDeadPlayer { get; set; }
    public UnityEvent recieveDamage;



    /// <summary>
    Movement movement;
    PShootingController shootingController;
/// </summary>
    // Start is called before the first frame update
    void Start()
    {
        movement = gameObject.GetComponent<Movement>();
        shootingController = gameObject.GetComponent<PShootingController>();
        currentHealth = maxHealthPoints;
    }

    public void UpgradeMovement(float Coefficient)
    {
        movement.speedRunning *= Coefficient;
        movement.speedWalking *= Coefficient;

    }
    public void UpgradeDamage(float Coefficient)
    {
        shootingController.damage = (int)(shootingController.damage * Coefficient);
    }
    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0) gameObject.GetComponent<PMoveController>().die.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if(recieveDamage != null)recieveDamage.Invoke();
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            EventToDeadPlayer?.Invoke();
        }
        EventMinusPlayerHp?.Invoke();

    }
}
