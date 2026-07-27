using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigPotion : MonoBehaviour
{
    [SerializeField]
    private Player player;
    public int count;
    [SerializeField]
    public float durationTime;
    [SerializeField]
    private float addMul;

    private float timer;
    private bool isDigUp;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Use();
        }
        if(isDigUp)
        {
            timer -= Time.deltaTime;
        }
        if(isDigUp&&timer<0)
        {
            isDigUp = false;
            player.AddDigSpeedMul(-addMul);
            timer= 0;
        }
    }

    private void Use()
    {
        if (count <= 0) return;
        if (!isDigUp)
        {
            isDigUp = true;
            player.AddDigSpeedMul(addMul);
        }
        timer += durationTime;
        count--;
    }
}
