using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : Collision
{

    Rigidbody2D rigidBody;
    HeartSystem heartSystem;
    float jumpSpeed = 3f;
    ItemsPicker itemsPicker;
    private void Awake()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        heartSystem = gameObject.GetComponent<HeartSystem>();
        itemsPicker = gameObject.GetComponent<ItemsPicker>();
    }

    bool hurt = false;
    bool invincible = false;
    bool stompedEnemy = false;
    public override void OnCollisionEnter2D(Collision2D other)
    {

        base.OnCollisionEnter2D(other);
        if (other.gameObject.layer == 9)
        {
            

            if (other.collider is BoxCollider2D) // Death from enemy
            {
                if (!invincible)
                {
                    if (other.gameObject.transform.position.x > transform.position.x)
                        rigidBody.velocity = new Vector2(-0.7f, 1.5f) * jumpSpeed;
                    else
                        rigidBody.velocity = new Vector2(0.7f, 1.5f) * jumpSpeed;
                    AudioManager.instance.PlaySound(AudioManager.Sound.PlayerHurt);
                    StartCoroutine(PlayerHurt());
                    heartSystem.removeHearth();
                }
                
            }
            else if (other.collider is CapsuleCollider2D && other.otherCollider is BoxCollider2D)//Killing enemy causing jump
            { 
                Debug.Log("STOMPED");
                
                stompedEnemy = true;
            }
        }
    }

    public override void OnCollisionExit2D(Collision2D other)
    {
        base.OnCollisionExit2D(other);

        if (other.collider is CapsuleCollider2D)
            stompedEnemy = false;
    }



    IEnumerator PlayerHurt()
    {

        
        //Debug.Log("Player hurt");
        Physics2D.IgnoreLayerCollision(9, 10,true);
        hurt = true;
        yield return new WaitForSeconds(1);
        Debug.Log("Player Hurt");
        hurt = false;
       // yield return new WaitForSeconds(1);
        Physics2D.IgnoreLayerCollision(9, 10, false);

    }


    public bool getIsHurt()
    {
        return hurt;
    }

    public void setIsHurt(bool hurt)
    {
        this.hurt = hurt;
    }
    public bool getStompedEnemy()
    {
        return stompedEnemy;
    }

   public bool getIsInvincible()
    {
        return invincible;
    }

   public void setIsInvincible(bool invincible)
    {
        this.invincible = invincible;
    }

}
