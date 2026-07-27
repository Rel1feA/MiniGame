using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;

public class BoomItem : MonoBehaviour
{
    [SerializeField]
    private float radius;

    private Animator animator;

    private void Awake()
    {
        animator=GetComponent<Animator>();
    }

    public void StartBoom()
    {
       Collider2D[] cols= Physics2D.OverlapCircleAll(transform.position, radius,LayerMask.GetMask("Block"));
        foreach(var col in cols)
        {
            Block block=col.GetComponent<Block>();
            if(block!=null)
            {
                block.BeDestoryed();
            }
        }
    }

    public void DestorySelf()
    {
        Destroy(gameObject);
    }

    public void PlayAudio()
    {
        AudioManager.Instance.PlayAudio("Boom");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
