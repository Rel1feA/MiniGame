using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float digDistance;
    [SerializeField]
    private Vector2 digRayOffset;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float hideCoolTime;
    [SerializeField]
    private float hideDuration;
    [SerializeField]
    private SpriteRenderer pickAxeSprite;

    private Vector2 faceDir;
    private Vector2 inputDir;
    private Block currentBlock;
    private Animator animator;
    private PlayMovement movement;
    private SpriteRenderer spriteRenderer;
    private float timer;
    private float digSpeedMul=1;
    private Translate translate;
    private DigPotion digPotion;
    private Boom boom;

    public P_MoveState moveState;
    public P_AirState airState;
    public P_DigState digState;
    public P_HideState hideState;
    private State<Player> currentState;
    
    public bool isHide;

    public PlayMovement Movement { get => movement; }
    public Vector2 InputDir { get => inputDir;}
    public Animator _Animator { get { return animator; } }
    public SpriteRenderer _SpriteRenender { get { return spriteRenderer; } }
    public float HideDuration { get { return hideDuration; } }
    public float Timer { get { return timer; } }
    public float HideCoolTime { get { return hideCoolTime; } }
    public SpriteRenderer PickaxeSprite { get { return pickAxeSprite; } }

    public float DigSpeedMul { get { return digSpeedMul; } }
    public Translate _Translate { get { return translate; } }
    public DigPotion _Digpotion { get { return digPotion; } }
    public Boom _Boom { get { return boom; } }


    private void Awake()
    {
        animator= GetComponent<Animator>();
        movement= GetComponent<PlayMovement>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        translate= GetComponent<Translate>();
        digPotion= GetComponent<DigPotion>();
        boom= GetComponent<Boom>();
        moveState =new P_MoveState();
        airState=new P_AirState();
        digState=new P_DigState();
        hideState=new P_HideState();
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddListener<Bat>("BatAlive", OnBatAlive);
        EventCenter.Instance.AddListener("RestartGame", ResetPlayer);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener<Bat>("BatAlive", OnBatAlive);
        EventCenter.Instance.RemoveListener("RestartGame", ResetPlayer);
    }

    private void Start()
    {
        ResetPlayer();
        GameManager.Instance.player = this;
    }

    private void Update()
    {
        HandleInput();
        SetFaceDir(InputDir);
        CheckBlock();
        currentState.FrameUpdate(this);
        HandleChangeState();
        if (timer < hideCoolTime) timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdate(this);
    }

    private void HandleInput()
    {
       inputDir=InputManager.Instance.GetAxis(InputConstants.Action_Move);
    }

    private void HandleChangeState()
    {
        State<Player> nextState = currentState.ChangeState(this);
        if(nextState!=null)
        {
            currentState.ExitState(this);
            currentState=nextState;
            currentState.EnterState(this);
        }
    }

    private void SetFaceDir(Vector2 dir)
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
        if(dir.x > 0)
        {
            transform.localScale= Vector3.one;
        }
        else if(dir.x< 0)
        {
            transform.localScale= new Vector3(-1,1,1);
        }
    }

    public void ResetPlayer()
    {
        faceDir = Vector2.right;
        currentState = moveState;
        currentState.EnterState(this);
        timer = 3.5f;
        transform.position = Vector3.zero;
    }

    public void AddDigSpeedMul(float value)
    {
        digSpeedMul+= value;
        animator.SetFloat("DigMul", digSpeedMul);
    }

    private void CheckBlock()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position+(Vector3)digRayOffset, faceDir, digDistance, LayerMask.GetMask("Block"));
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
        AudioManager.Instance.PlayAudio("gravel1");
        currentBlock = null;
    }

    public void UseSkill()
    {
        isHide = !isHide;
        EventCenter.Instance.EventTrigger("PlayerUseSkill", this);
    }

    public void ResetTimer()
    {
        timer = 0;
    }

    private void OnBatAlive(Bat bat)
    {
        bat.SetPlayer(this);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position+(Vector3)digRayOffset, faceDir * digDistance);
    }
}
