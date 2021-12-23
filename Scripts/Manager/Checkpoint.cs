using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator animator = null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            animator.SetBool("Reached", true);
            Save.instance.SavePosition(transform.position);
            Save.instance.SaveProgress();

        }
        
        
    }


 
}
