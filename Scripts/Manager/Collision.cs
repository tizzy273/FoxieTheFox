using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{


    protected bool isGrounded = false;
    protected bool isAlive = true;

    public bool getIsAlive()
    {
        return isAlive;
    }

    public bool getIsGrounded()
    {
        return isGrounded;
    }

    public void setIsAlive(bool Alive)
    {
        isAlive = Alive;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 8 || collision.gameObject.layer == 16)&& collision.otherCollider is BoxCollider2D) //
            isGrounded = true;

   
    }

    public virtual void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 8 || collision.gameObject.layer == 16) && collision.otherCollider is BoxCollider2D)
            isGrounded = false;
       
            
    }

}
