using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translate : MonoBehaviour
{
    private Player player;
    [SerializeField]
    private Transform targetPos;

    public void Init(Player player)
    {
        this.player= player;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            player.transform.position=targetPos.position;
            Destroy(this);
        }
    }
}
