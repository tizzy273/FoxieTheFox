using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    [System.Serializable]
    enum MovementType
    {
        horizontal,
        vertical,
    }

    [SerializeField] MovementType movementType = MovementType.horizontal;
    [SerializeField] float movementSpeed = 0;
    [SerializeField] float radiusMovement = 0;

    Vector3 pointA;
    Vector3 pointB;
   

    // Start is called before the first frame update
    void Start()
    {
       

        pointA = transform.position;
        if (movementType == MovementType.horizontal)
            pointB = new Vector3(transform.position.x + radiusMovement, transform.position.y);
        else if (movementType == MovementType.vertical)
            pointB = new Vector3(transform.position.x , transform.position.y + radiusMovement);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time * movementSpeed, 1));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(null);
    }

}
