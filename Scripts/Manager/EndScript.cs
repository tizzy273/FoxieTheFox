using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.score += 30000;
        GameManager.instance.credits = true;
        GameManager.instance.GameOver();
    }
}
