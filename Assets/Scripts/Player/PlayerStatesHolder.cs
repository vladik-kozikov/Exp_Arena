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
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealthPoints;
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
