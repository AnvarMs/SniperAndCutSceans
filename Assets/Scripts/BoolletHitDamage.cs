using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BoolletHitDamage : MonoBehaviour
{

    [SerializeField] Damage damage;
    [SerializeField] LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.tag == "Enemy");
        if (other.tag == "Enemy") 
        {
            
            damage = other.transform.GetComponent<Damage>();
            damage.HelthDamage(100);
        }

        



    }
}
