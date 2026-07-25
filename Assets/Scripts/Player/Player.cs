using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float digDistance;
    [SerializeField]
    private int damage;

    private Vector2 faceDir;
    private Vector2 inputDir;
    private Block currentBlock;
    private Animator animator;
    private PlayMovement movement;

    

    public bool isHide;

    private void OnDisable()
    {
        UnBindInput();
    }

    private void Awake()
    {
        animator= GetComponent<Animator>();
        movement= GetComponent<PlayMovement>();
    }

    private void Start()
    {
        ResetPlayer();
        BindInput();
    }

    private void Update()
    {
        HandleInput();
        SetFaceDir(inputDir);
        CheckBlock();
    }

    private void BindInput()
    {
        InputManager.Instance.BindAction(InputConstants.Action_Skill, UseSkill, InteractionType.Performed);
        InputManager.Instance.BindAction(InputConstants.Action_Dig,DigBlock,InteractionType.Performed);
    }

    private void UnBindInput()
    {
        InputManager.Instance.UnBindAction(InputConstants.Action_Dig, DigBlock, InteractionType.Performed);
        InputManager.Instance.BindAction(InputConstants.Action_Skill, UseSkill, InteractionType.Performed);
    }


    public void HandleInput()
    {
       inputDir=InputManager.Instance.GetAxis(InputConstants.Action_Move);
    }

    public void SetFaceDir(Vector2 dir)
    {
        if(dir==Vector2.up)
        {
            faceDir = Vector2.up;
        }
        else if(dir==Vector2.down)
        {
            faceDir = Vector2.down;
        }
        else if(dir==Vector2.left)
        {
            faceDir = Vector2.left;
        }
        else if(dir==Vector2.right)
        {
            faceDir = Vector2.right;
        }
    }

    public void ResetPlayer()
    {
        faceDir = Vector2.down;
    }

    private void CheckBlock()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, faceDir, digDistance, LayerMask.GetMask("Block"));

        if (hit.collider != null)
        {
            // 获取目标方块组件
            Block targetBlock = hit.collider.GetComponent<Block>();
            if (targetBlock == null) return; // 不是 Block 则忽略

            // 如果当前高亮的不是这个方块，则切换
            if (currentBlock != targetBlock)
            {
                // 取消旧的高亮
                if (currentBlock != null)
                    currentBlock.ExitHighLight();

                // 更新为新方块
                currentBlock = targetBlock;
            }

            // 高亮当前方块（无论是新切换的还是保持不变的）
            currentBlock.HighLight();
        }
        else
        {
            // 未检测到任何方块 → 取消高亮
            if (currentBlock != null)
            {
                currentBlock.ExitHighLight();
                currentBlock = null;
            }
        }
    }

    public void DigBlock()
    {
        if(currentBlock == null) return;
        currentBlock.Knocked(damage);
        currentBlock = null;
    }

    public void UseSkill()
    {
        isHide = !isHide;
        EventCenter.Instance.EventTrigger("PlayerUseSkill", this);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, faceDir * digDistance);
    }
}
