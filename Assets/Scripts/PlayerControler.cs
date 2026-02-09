using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerControler : MonoBehaviour
{
   public CharacterController characterController;
    public Animator animator;
    public SnipeTheEnmy snipeTheEnmy;
    [SerializeField] float moveSpead = 10f;

    [SerializeField] float g;
    Vector3 vectorY;

    [SerializeField] LayerMask ground;
    [SerializeField] Transform spearPos;
    [SerializeField] float radius = 2f;
    [SerializeField] float jumpH;
    bool isOnGround;
    bool isWalking;

    // Update is called once per frame
    void Update()
    {

        InputCheck();

    }

    void InputCheck()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !snipeTheEnmy.isShooting)
        {
            moveSpead = 13f;
           
            isWalking = false;
        }
        else
        {
            moveSpead = 5f;
            isWalking= true;
        }
        isOnGround = Physics.CheckSphere(spearPos.position, radius, ground);

        if (isOnGround)
        {
            vectorY.y = -1;
        }
        else
        {
            vectorY.y -= g * Time.deltaTime;

        }
        if (Input.GetButton("Jump") && isOnGround && !snipeTheEnmy.isShooting)
        {
            vectorY.y = jumpH;

            StartCoroutine(JumbAnime());
            
        }
        characterController.Move(vectorY);
        

        float x = Input.GetAxis("Horizontal") * moveSpead * Time.deltaTime;
        float y = Input.GetAxis("Vertical") * moveSpead * Time.deltaTime;

        characterController.Move(transform.forward * y);
        characterController.Move(transform.right * x);

        animatPlayer();
    }

    void animatPlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x == 0f && y == 0f)
        {
            animator.SetBool("idle", true);
            animator.SetBool("run", false);
            animator.SetBool("walk", false);
            
        }else if (isWalking)
        {
            animator.SetBool("idle", false);
            animator.SetBool("run", false);
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("idle", false);
            animator.SetBool("run", true);
            animator.SetBool("walk", false);
        }
    }
   
    IEnumerator JumbAnime()
    {
        animator.SetBool("idle", false);
        animator.SetBool("run", false);
        animator.SetBool("walk", false);
        animator.SetBool("jump", true);

        yield return new WaitForSeconds(1);

        animator.SetBool("jump",false);
    }
}
