using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class SnipeTheEnmy : MonoBehaviour
{
    [SerializeField] Camera camera;
                    RaycastHit hit;
    [SerializeField] float helthDamage = 100;
    [SerializeField] Animator animator;
    public bool isShooting;
    public bool reloading;
           bool isScope = false;
    [SerializeField] ParticleSystem particleShoot;
    [SerializeField] GameObject particlePosition;
    [SerializeField] GameObject Croos;
    
    [Header("Amunation Mecanism")]
    [SerializeField] int magLimit = 7;
    [SerializeField] int amoAvailable = 10;
    [SerializeField] int magRefil = 7;

    [Header("Amo Counting")]
    [SerializeField] TMP_Text AVailebleAmo;
    [SerializeField] TMP_Text MagAmo;

    [Header("Recoile System")]
    [SerializeField] MouseMovement mouseMovement;
    [SerializeField] float recoilx=1;
    [SerializeField] float recoily=1;
    
    [Header("Audio System")]

    [SerializeField] AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(camera.transform.position, camera.transform.forward, out hit);
        Debug.DrawRay(camera.transform.position, camera.transform.forward * hit.distance, Color.red);
        if (Input.GetButtonDown("Fire1") && magLimit > 0 && !isShooting && !reloading)
        {
            audioSource.Play();
            ShootSniper();
            if (!isScope&&!reloading) 
            { 
            StartCoroutine(ShootAnime());
            }
            else if(!reloading)
            {
                StartCoroutine(ZoomeShot());
                
            }
            recoily = Random.Range(-recoily, recoily);
            recoilx = Random.Range(-recoilx, recoilx);
            
            mouseMovement.RecoilSniper(recoily, recoilx);
        }
        else
        {
            mouseMovement.RecoilSniper(0, 0);
        }
        if (magLimit == 0 && amoAvailable >0&&!reloading)
        {


            reloading = true;
            StartCoroutine(RealodeSniper());
            reloading = true;
            
        }
        if (Input.GetKeyDown(KeyCode.R)&&magLimit<magRefil && amoAvailable >0&&!reloading)
        {
            reloading=true;
            StartCoroutine(RealodeSniper());
           
        }
        if (Input.GetButtonDown("Fire2")&&!isShooting)
            
        {
            ZoomTheScope();

        }
        if (isScope || reloading )
        {
            Croos.SetActive(false);
        }
        else
        {
            Croos.SetActive(true);
        }
        AmoCountShowingToUser();
    }
   
    
    void AmoCountShowingToUser()
    {
        MagAmo.text = magLimit.ToString();
        AVailebleAmo.text = amoAvailable.ToString();
        if (magLimit < 1)
        {
            MagAmo.color = Color.red;
        }
        else
        {
            MagAmo.color = Color.white;
        }
        AVailebleAmo.color = amoAvailable < 1 ? Color.red : Color.white;
    }

    void ShootSniper()
    {
        magLimit--;

        ParticleSystem particle = Instantiate(particleShoot, particlePosition.transform.position, particlePosition.transform.rotation);
       
      /*  if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
        {
            
            if (hit.transform.tag == "Enemy")
            {
                Damage damage = hit.transform.GetComponent<Damage>();
                damage.HelthDamage(helthDamage);
                
            }
        }*/
        
    }
    IEnumerator ShootAnime()
    {
       
       
        isShooting = true;
        animator.SetBool("idle", false);
        animator.SetBool("run", false);
        animator.SetBool("walk", false);
        animator.SetBool("jump", false);
        animator.SetBool("zoom", false);

        animator.SetBool("shoot", true);
       
       
        yield return new WaitForSeconds(1f);

        
        animator.SetBool("shoot", false);
        yield return new WaitForSeconds(1f);
        isShooting = false;
    }
    IEnumerator RealodeSniper()
    {
        animator.SetBool("idle", false);
        animator.SetBool("run", false);
        animator.SetBool("walk", false);
        animator.SetBool("jump", false);
        animator.SetBool("zoom", false);
        animator.SetBool("shoot", false);

        animator.SetBool("reaload", true);
       

        yield return new WaitForSeconds(2f);

        
        if (amoAvailable - magRefil > 0&&magLimit==0)
        {   // 30 = 30-7 
            

            amoAvailable -= magRefil;
            
            // 0 = 7
            magRefil = 7;
        }
        else if (amoAvailable>0&&magLimit==0) 
        {
            
            magRefil=amoAvailable;
            amoAvailable = 0;
        }
        else
        {
            amoAvailable -= magRefil- magLimit;
            magRefil = 7; 
            
        }

        magLimit = magRefil;
        
        reloading = false;
        animator.SetBool("reaload", false);
    }
    void ZoomTheScope()
    {
        
        animator.SetBool("idle", false);
        animator.SetBool("run", false);
        animator.SetBool("walk", false);
        animator.SetBool("jump", false);

         isScope = !isScope;
        animator.SetBool("zoom", isScope);

    }
     IEnumerator ZoomeShot()
    {
        isShooting = true;
        animator.SetBool("idle", false);
        animator.SetBool("run", false);
        animator.SetBool("walk", false);
        animator.SetBool("jump", false);

        animator.SetTrigger("shootScop");

        yield return new WaitForSeconds(1);
        isShooting=false;
    }
    
}
