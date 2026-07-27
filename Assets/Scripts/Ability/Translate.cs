using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translate : MonoBehaviour
{
    private Player player;
    public int count;
    [SerializeField]
    private Transform targetPos;

    public void Init(Player player)
    {
        this.player = player;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Use();
        }
    }

    public void Use()
    {
        if (count <= 0) return;
        player.transform.position = targetPos.position;
        count--;
    }
}
