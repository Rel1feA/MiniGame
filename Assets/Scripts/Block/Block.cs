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
    [SerializeField]
    private Sprite deadSprite;
    private SpriteRenderer spriteRenderer;
    private Color startColor;
    private Collider2D col;


    private void Awake()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
        col= GetComponent<BoxCollider2D>();    
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
        ExitHighLight();
        spriteRenderer.sprite=deadSprite;
        col.enabled = false;
    }
}
