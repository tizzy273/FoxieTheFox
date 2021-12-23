using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using ExtensionMethods;
public class ItemsPicker : MonoBehaviour
{

    //float cherries = 0; 
    //float gems; 
    [SerializeField] TextMeshProUGUI textCherries = null;
    [SerializeField] TextMeshProUGUI textGems = null;
    PlayerCollision playerCollision;
    Shooting shooting;
    public ParticleSystem particleLaser;
    void createLaserEffect()
    {
        particleLaser.Play();
    }
    private void Awake()
    {
        playerCollision = gameObject.GetComponent<PlayerCollision>();
        shooting = gameObject.GetComponent<Shooting>();
    }

    private void Update()
    {
        textCherries.text = GameManager.instance.cherries.ToString();
        textGems.text = GameManager.instance.gems.ToString();
    }


    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 11)
        {
            AudioManager.instance.PlaySound(AudioManager.Sound.Cherry);
            GameManager.instance.cherries++;
            GameManager.instance.score+= 100;
            Debug.Log(GameManager.instance.cherries);
             //cambiare in seguito se si aggiunge il tempo, creare una classe per UI 
            Destroy(other.gameObject);
        }
        else  if (other.gameObject.layer == 12)
        {

            PowerUp();
            AudioManager.instance.PlaySound(AudioManager.Sound.Gem);
            GameManager.instance.gems++;
            GameManager.instance.score += 1000;
            
            Destroy(other.gameObject);
 
        }
        else if(other.gameObject.layer == 15)
        {
            AudioManager.instance.PlaySound(AudioManager.Sound.Clock);
            DecreaseTime();
            Destroy(other.gameObject);
        }
    }

    void PowerUp()
    {

        int powerUP = UnityEngine.Random.Range(1, 4);
        Debug.Log("PowerUp: " + powerUP);
        switch (powerUP)
        {
            case 1:
                LifeUp();
                break;
            case 2:
                Invincible();
                break;
            case 3:
                Shooter();
                break;
        }
    }

    void LifeUp()
    {
        GameManager.instance.health++;
    }
        
    void Invincible()
    {
        playerCollision.setIsInvincible(true);
        transform.position = transform.position + new Vector3(0,0.15f, 0);
        transform.globalScale(new Vector3(4, 4, 0));
        StartCoroutine(InvincibleTimeSpan());
       
    }


   
    void DecreaseTime()
    {
        TimeManager.instance.elapsedTime -= 5;
    }

    void Shooter()
    {
        shooting.Enable(true);
        createLaserEffect();
        StartCoroutine(ShooterTimeSpan());
        
        
    }
    IEnumerator InvincibleTimeSpan()
    {
        yield return new WaitForSeconds(8);
        playerCollision.setIsInvincible(false);
        transform.globalScale(new Vector3(3, 3, 0));

    }

    IEnumerator ShooterTimeSpan()
    {
        yield return new WaitForSeconds(8);
        shooting.Enable(false);

    }
}
