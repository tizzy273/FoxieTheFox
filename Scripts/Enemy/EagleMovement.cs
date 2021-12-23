using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EagleMovement : Movement
{
    // Start is called before the first frame update
    [SerializeField] Transform target = null;

    [SerializeField] float nextWayPointDistance = 3f;

    Path path;

   

    int currentWayPoint = 0;
    bool playerReached;
    PlayerCollision playerCollision = null;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        playerCollision = GameObject.Find("Player").GetComponent<PlayerCollision>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(UpdatePath());
        HandleSoundEffect();
    }

    IEnumerator UpdatePath()
    {
        while (true)
        {
            if (seeker.IsDone() && !playerCollision.getIsHurt() && nearPlayer())
            {
                Debug.Log(seeker.IsDone());
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            }
            yield return new WaitForSeconds(0.5f); 
        //
        }

    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

        HandleAnimation();

        HandleMovement();

    }

    IEnumerator SoundEffect()
    {
        while (true)
        {
            if (nearPlayer() && collision.getIsAlive())
                AudioManager.instance.PlaySound(AudioManager.Sound.Eagle);
            yield return new WaitForSeconds(2f);
        }
        
    }

    public override void HandleSoundEffect()
    {
        StartCoroutine(SoundEffect());
    }

    public override void HandleMovement()
    {
        if (rigidBody.velocity.x > 0)
            hTranslation = -1;
        else if (rigidBody.velocity.x < 0)
            hTranslation = 1;

        if (path == null)
            return;
        if (currentWayPoint >= path.vectorPath.Count)
        {
            playerReached = true;
            return;
        }
        else
        {
            playerReached = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized; //Versor to player
        Vector2 force = direction * movementSpeed * 100 * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }
    }

}