using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : Movement
{
    bool jumped;
    public ParticleSystem dust;
    //GameObject player;
    //private Rigidbody2D rigidBody;
    //[SerializeField] Animator animator;


    PlayerCollision playerCollision;
    BoxCollider2D boxCollider2D;
    

  
    // Start is called before the first frame update

    private void Awake()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
        playerCollision = gameObject.GetComponent<PlayerCollision>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
       // Debug.Log(GameManager.instance.hCommands);
      
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("CIAO");
        HandleMovement();
        HandleAnimation();
    }

 
    void CreateDust()
    {
        dust.Play();
    }


   public override void  HandleAnimation()
    {

        //Flipping fox
        base.Flipping();

       
          animator.SetFloat("Speed", Mathf.Abs(hTranslation));
        animator.SetFloat("GoingUp", goingUp());
        //  animator.SetTrigger("Player_Hurt");
      //  if (playerCollision.getIsHurt())
      //  {
            //rigidBody.AddForce(Vector2.left *7, ForceMode2D.Impulse)
                //boxCollider2D.enabled = false;
                animator.SetBool("Hurt", playerCollision.getIsHurt());
       // Debug.Log(playerCollision.getIsHurt());
               // playerCollision.setIsHurt(false);
           //     boxCollider2D.enabled = true;
            // playerCollision.setIsAlive(true);   
        //}
           // rigidBody.AddForce(new Vector2(2, 2), ForceMode2D.Impulse);
           // StartCoroutine(DeathAnimation());
    }

   

    public override void HandleMovement()
    {


        //Running

        if (!playerCollision.getIsHurt())
        {
            hTranslation = Input.GetAxis(GameManager.instance.hCommands) * movementSpeed;
            rigidBody.velocity = new Vector2(hTranslation, rigidBody.velocity.y);



            if ((Input.GetKeyDown(KeyCode.Space) && playerCollision.getIsGrounded()) || playerCollision.getStompedEnemy())//|| playerCollision.getIsHurt())  //Salto
            {
                CreateDust();
                rigidBody.velocity = Vector2.up * 7f;
                // playerCollision.setIsHurt(false);
            }


            if (!playerCollision.getIsGrounded())// Discesa veloce
            {
                if (GameManager.instance.hCommands == "arrows")
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow)) 
                        rigidBody.velocity = Vector2.down * jumpSpeed;
      
                }
                else if(GameManager.instance.hCommands == "ad")
                {
                    if (Input.GetKeyDown(KeyCode.S))
                        rigidBody.velocity = Vector2.down * jumpSpeed;
                }
            }
        }
       /* else
        {
            if (!jumped)
            {
                rigidBody.velocity = Vector2.up * jumpSpeed;
                jumped = true;
            }
            if (playerCollision.getIsGrounded())
            {
                animator.SetTrigger("Hurt");
                playerCollision.setIsHurt(false);
            }
        }*(
            /*if (Input.GetKeyDown(KeyCode.RightArrow))*/


        }
    
}
