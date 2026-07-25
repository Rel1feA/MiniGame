using UnityEngine;
using RECode.REFramework;

public class MonsterBlock : Block
{
    [SerializeField]
    private float monsterPercent;

    public override void BeDestoryed()
    {
        int rand = Random.Range(0, 100);
        if(rand<monsterPercent)
        {
            Debug.Log("出现蝙蝠");
            GameObject obj=ResourcesManager.Instance.Load<GameObject>("Prefabs/Monsters/Bat");
            obj.transform.position = transform.position;
        }
        base.BeDestoryed();
    }
}
