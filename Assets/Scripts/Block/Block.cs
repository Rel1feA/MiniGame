using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using RECode.REFramework;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Block : MonoBehaviour
{
    [SerializeField]
    private int health;
    private SpriteRenderer spriteRenderer;
    private Color startColor;

    public TreasureData ContainedTreasure { get; set; }


    private void Awake()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        startColor = spriteRenderer.color;
    }

    public virtual void HighLight()
    {
        spriteRenderer.color = Color.red;
    }

    public virtual void ExitHighLight()
    {
        spriteRenderer.color = startColor;
    }

    public virtual void Knocked(int damage)
    {
        health-=damage;
        if(health<=0)
        {
            BeDestoryed();
        }
    }


    public virtual void BeDestoryed()
    {
        if(ContainedTreasure!=null)
        {
            SpawnTreasureItem();
        }
        Destroy(gameObject);
    }

    private void SpawnTreasureItem()
    {
        GameObject obj = Instantiate(ContainedTreasure.dropPrefab, transform.position, Quaternion.identity);
        TreasureItem treasureItem=obj.GetComponent<TreasureItem>();
        treasureItem.Init(ContainedTreasure);
    }
}
