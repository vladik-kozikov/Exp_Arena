using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatesHolder : MonoBehaviour
{
    public int maxHealthPoints;
    public int currentHealth;
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
        currentHealth -= damage;
        
    }
}
