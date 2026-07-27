using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField]
    private Player player;
    public int count;
    [SerializeField]
    private GameObject prefab;

    private float timer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

        }
    }
}
