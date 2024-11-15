using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private ParticleSystem damageParticles;

    private float currentHealth;

    private KnockBack knockback;

    private void Start()
    {
        currentHealth = maxHealth;
        knockback = GetComponent<KnockBack>();
    }

    public void Damage(float damageAmount, Vector2 hitDirection)
    {
        currentHealth -= damageAmount;

        Instantiate(damageParticles, transform.position, Quaternion.identity);



        if (currentHealth <= 0)
        {

        }
    }
}
