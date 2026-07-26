using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;

public class Bat : MonoBehaviour
{
    [SerializeField]
    private float chaseSpeed;
    [SerializeField]
    private float minSleepTime;
    [SerializeField]
    private float maxSleepTime;
    [SerializeField]
    private float deadDistance;

    private float sleepTime;
    private Vector2 chaseDir;
    private bool isChase;

    private Rigidbody2D rb2D;
    private bool isSleeping=true;
    private float timer;
    private Animator animator;
    private Player player;

    private void Awake()
    {
        rb2D=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        sleepTime=Random.Range(minSleepTime,maxSleepTime);
        EventCenter.Instance.EventTrigger("BatAlive", this);
        RandDir();
    }

    private void Update()
    {
        if(timer<sleepTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            isSleeping= false;
            animator.Play("Fly");
        }
        if(isChase)
        {
            chaseDir=(player.transform.position-transform.position).normalized;
            if(player.isHide)
            {
                isChase = false;
                RandDir();
            }
        }
        else
        {
            if(!player.isHide)
            {
                isChase = true;
            }
        }
        CheckDead();
    }


    private void FixedUpdate()
    {
        if(!isSleeping)
        {
            Chase();
        }
    }

    public void Chase()
    {
        rb2D.velocity = chaseDir * chaseSpeed;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    private void RandDir()
    {
        float randX = Random.Range(-1f, 1f);
        float randY = Random.Range(-1f, 1f);
        chaseDir = new Vector2(randX, randY).normalized;
    }

    private void CheckDead()
    {
        if(Vector3.Distance(player.transform.position,transform.position)>deadDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("扣分！！！");
        }
    }
}
