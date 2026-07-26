using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;

public class MonsterBlock : Block
{
    [SerializeField]
    private int batPercent;

    public override void BeDestoryed()
    {
        int rand=Random.Range(0, 100);
        if(rand<batPercent)
        {
            ResourcesManager.Instance.LoadAsync<GameObject>("Prefabs/Monsters/Bat", (o) =>
            {
                o.transform.position=transform.position;
            });
        }
        base.BeDestoryed();
    }
}
