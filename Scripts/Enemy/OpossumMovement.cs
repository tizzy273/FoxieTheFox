using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumMovement : Movement

{

    [System.Serializable]
    enum MovementType
    {
        horizontal,
        vertical,
    }

    [SerializeField] MovementType movementType = MovementType.horizontal;
    [SerializeField] float radiusMovement = 0;

    Vector3 pointA;
    Vector3 pointB;

    void Start()
    {


        pointA = transform.position;
        if (movementType == MovementType.horizontal)
            pointB = new Vector3(transform.position.x + radiusMovement, transform.position.y);
        else if (movementType == MovementType.vertical)
            pointB = new Vector3(transform.position.x, transform.position.y + radiusMovement);
        hTranslation = 1;
    }

   
    void Update()
    {
       
        HandleMovement();
        HandleAnimation();
    }


    public override void HandleMovement()
    {
        if(collision.getIsAlive())
            transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time *movementSpeed, 1)); 
    
        StartCoroutine(Run());
        Vector3 prevLocation = transform.position;
       
    }

    public override void HandleSoundEffect()
    {
            StartCoroutine(SoundEffect());
    }

    IEnumerator SoundEffect()
    {
        while (true)
        {
            if (nearPlayer() && collision.getIsAlive())
                AudioManager.instance.PlaySound(AudioManager.Sound.Opossum);
            yield return new WaitForSeconds(1.5f);
        }
       
    }


    IEnumerator Run()
     {
        Vector3 prevLocation = transform.position; 
        yield return new WaitForSeconds(Mathf.Epsilon);
        if ((transform.position - prevLocation).normalized.x < 0)
            hTranslation = Mathf.Abs(hTranslation);
        else if ((transform.position - prevLocation).normalized.x > 0)
            hTranslation = -Mathf.Abs(hTranslation);

    }
}
