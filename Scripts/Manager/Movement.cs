using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    
    [SerializeField] protected float jumpSpeed;
    protected float hTranslation = 1;
    protected Rigidbody2D rigidBody;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Collision collision;
    [SerializeField] protected float movementSpeed;
    Transform player;
    Transform firePoint;

    private void Awake()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
        collision = gameObject.GetComponent<Collision>();
        player = GameObject.Find("Player").GetComponent<Transform>();
     
    }
   
    public virtual void Flipping()
    {
        Vector3 Direction = transform.rotation.eulerAngles;
       


        Vector3 playerDirection = transform.localScale;
        if (hTranslation < 0)
        {
            Direction.y = 180;
            transform.rotation = Quaternion.Euler(Direction);

        } 

        
        if (hTranslation > 0)
        {
            Direction.y = 0;
            transform.rotation = Quaternion.Euler(Direction);
        }
 
    }


    public virtual void HandleMovement() { }



    protected float goingUp()
    {
        if (!collision.getIsGrounded())
        {
            if (rigidBody.velocity.y > 0)
            {
                
                return 1;
            }
            if (rigidBody.velocity.y < 0)
                return -1;
        }
       
        return 0;
    }


    protected bool nearPlayer()
    {
        if (Vector3.Distance(player.position, transform.position) < 10)
            return true;
        else return false;
    }

    public virtual void HandleSoundEffect() { }

    public virtual void  HandleAnimation()
    {
        Flipping();
        // animator.SetFloat("GoingUp", goingUp());
        animator.SetBool("Alive", collision.getIsAlive());

        if (!collision.getIsAlive())
            StartCoroutine(DeathAnimation());
      
    }

    public virtual IEnumerator DeathAnimation()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        rigidBody.velocity = Vector2.zero; //Stop movement
        animator.SetBool("Alive", false); //Start death animation
        //AudioManager.instance.PlaySound(AudioManager.Sound.EnemyDeath);
        yield return new WaitForSeconds(0.750f);//Wait to finish animation
        gameObject.SetActive(false);
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
}