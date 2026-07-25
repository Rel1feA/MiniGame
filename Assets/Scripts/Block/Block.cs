using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Block : MonoBehaviour
{
    [SerializeField]
    private int health;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

    public virtual void HighLight()
    {
        transform.DOScale(Vector3.one * 1.2f, 0.2f);
    }

    public virtual void ExitHighLight()
    {
        transform.DOScale(Vector3.one, 0.2f);
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
        Destroy(gameObject);
    }
}
