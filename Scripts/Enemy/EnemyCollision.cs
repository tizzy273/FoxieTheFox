using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : Collision
{
   
    public override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        if (other.gameObject.layer == 10)
        {
            if ((other.collider is BoxCollider2D && other.otherCollider is CapsuleCollider2D) || other.gameObject.GetComponent<PlayerCollision>().getIsInvincible()) // If player stomped enemy, or if player take invincible powerup
            {
                AudioManager.instance.PlaySound(AudioManager.Sound.EnemyDeath);
                isAlive = false;
            }
        }
        
    }
}
