using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollision : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && collision is BoxCollider2D)
        {
            GameManager.instance.health--;
            GameManager.instance.RestartFromCheckpoint();
        }
    }
}
