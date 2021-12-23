using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Transform weapon = null;
    [SerializeField] LineRenderer laser = null;
    bool shooting;
    bool powerUp = false;
    
    // Update is called once per frame
    void Update()
    {
        if (powerUp)
        {
            if (Input.GetButton(GameManager.instance.fireCommand))
            {
                shooting = true;
                Shoot();


            }
            else
            {
                shooting = false;
                laser.enabled = false;
            }
        }
        else
            laser.enabled = false; 
    }
    void Shoot()
    {
        if (shooting)
        {

         
            RaycastHit2D hit = Physics2D.Raycast(weapon.position, weapon.right,5);

            if (hit && hit.transform.gameObject.layer == 9)
            {
                hit.transform.gameObject.GetComponent<EnemyCollision>().setIsAlive(false);
                laser.SetPosition(0, weapon.position);
                laser.SetPosition(1, hit.transform.position);
                
               
                
            }
            else
            {
                laser.SetPosition(0, weapon.position);
                laser.SetPosition(1, weapon.position + weapon.right * 5);
            }

           
            laser.enabled = true;
           // yield return new WaitForSeconds(0.01f);
            //

        }
    }

    public void Enable(bool powerUp)
    {
        this.powerUp = powerUp;
    }
}
