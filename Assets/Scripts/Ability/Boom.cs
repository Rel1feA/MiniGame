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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Use();
        }
    }

    public void Use()
    {
        if(count>0)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.position = transform.position;
            count--;
        }
    }
}
