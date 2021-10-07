using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Stats : MonoBehaviour
{
    [Space]
    [Header("Stat :")]
    public float MaxHealth;
    public float Health;
    public int damage = 10;

    public GameObject player;
    public static Player_Stats PlayerStats;

    public Slider HealthBar;

    void Awake()
    {
        if(PlayerStats != null)
        {
            Destroy(PlayerStats);
        }
        else
        {
            PlayerStats = this;
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Health = MaxHealth;
        HealthBar.maxValue = MaxHealth;
    }

    public void Update()
    {
        if (Health >= 0)
        {
            HealthBar.value = 0;
        }
        HealthBar.value = Health;
    }

    public void DealDMG(float damage)
    {
        Health -= damage;
        CheckDeath();
    }

    public void Heal(float heal)
    {
        Health += heal;
        Overheal();
    }

    public void Overheal()
    {
        if(Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    void CheckDeath()
    {
        if (Health <= 0)
        {
            Destroy(player.gameObject);
        }
    }
}
