using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    private float helth = 100f;

    public void HelthDamage(float damage)
    {
        helth -= damage;
        ParticleSystem par  =  Instantiate(particle, transform.position, transform.rotation);
    }

    
    private void Update()
    {
        if (helth < 1)
        {
            Destroy(gameObject);
        }
    }
}
