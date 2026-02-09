using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet: MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 10f;
    public ParticleSystem blodeEffect;
    
    private void Start()
    {
        
        Destroy(gameObject, lifeTime);
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(transform.forward* speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(blodeEffect, transform.position, Quaternion.LookRotation(collision.transform.up));
        Destroy(gameObject);
    }
   
}
