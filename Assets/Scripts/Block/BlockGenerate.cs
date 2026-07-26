using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;

public class BlockGenerate : MonoBehaviour
{
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private float interval;
    [SerializeField]
    private string blockPrefabName;

    private void Start()
    {
        GenerateBlock();
    }

    private void GenerateBlock()
    {
        for(int i=0;i<height;i++)
        {
            for(int j=0;j<width;j++)
            {
                Vector2 pos=startPos.position+new Vector3(j*interval,-i*interval,0);
                ResourcesManager.Instance.LoadAsync<GameObject>($"Prefabs/Blocks/{blockPrefabName}", (o) =>
                {
                    o.transform.position = pos;
                    o.transform.parent = transform;
                });
            }
        }
    }
}
