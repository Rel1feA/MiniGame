using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translate : MonoBehaviour
{
    [SerializeField]
    private Player player;
    public int count;
    [SerializeField]
    private Transform targetPos;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
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
