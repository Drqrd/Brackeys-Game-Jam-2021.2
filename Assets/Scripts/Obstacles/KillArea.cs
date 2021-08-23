using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillArea : Area
{
    Player p;
    Main m;

    private void OnTriggerEnter(Collider other)
    {
        playerRef.GetComponent<Player>().SetState(new PlayerDeath(playerRef.GetComponent<Player>(), gameRef.GetComponent<Main>()));
    }
}
