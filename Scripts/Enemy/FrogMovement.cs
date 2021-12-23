using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : Movement
{
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
        HandleMovement();

    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimation();
    }

    public override void HandleAnimation()
    {
        base.HandleAnimation();
        animator.SetFloat("GoingUp", goingUp());
    }

    public override void HandleMovement()
    { 
        StartCoroutine(Jumping());
    }

    public override void HandleSoundEffect()
    {
        if(nearPlayer() && collision.getIsAlive())
            AudioManager.instance.PlaySound(AudioManager.Sound.Frog);
    }

    IEnumerator Jumping()
    {
        while (true)
        {

            HandleSoundEffect();
            if (collision.getIsGrounded())
            {

                rigidBody.velocity = Vector2.up * jumpSpeed;
                rigidBody.velocity = new Vector2(hTranslation, rigidBody.velocity.y);
            }

            hTranslation = -hTranslation;
            
            yield return new WaitForSeconds(movementSpeed);
        }
       
    }
}
