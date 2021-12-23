using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] Transform spawnPoint = null;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        player = Instantiate(player, spawnPoint.position, spawnPoint.rotation);
        player.GetComponent<Player>().Spawn(spawnPoint);
    }

}
