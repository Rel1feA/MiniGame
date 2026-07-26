using RECode.REFramework;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float flySpeed;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    [Range(0f, 2f)]
    private float DownGravityScaleMultiply = 1f;
    [SerializeField]
    private Vector2 groundBoxOrigin;
    [SerializeField]
    private Vector2 groundBoxSize;

    private Rigidbody2D rb2D;
    private Collider2D col2D;
    private float normalGravityScale;

    public float VelX { get => rb2D.velocity.x; }
    public float VelY { get => rb2D.velocity.y; }

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();
    }

    private void Start()
    {
        SetGroundBoxValue();
        normalGravityScale = rb2D.gravityScale;
    }

    private void Update()
    {
        SetGroundBoxValue();
    }

    private void FixedUpdate()
    {
        ChangeGravityScale();
    }

    public void Move(float input)
    {
        rb2D.velocity = new Vector2(input * speed, rb2D.velocity.y);
    }

    public void Fly()
    {
        rb2D.velocity=new Vector2(rb2D.velocity.x,flySpeed);
    }

    public bool isOnGround()
    {
        return Physics2D.OverlapBox(groundBoxOrigin, groundBoxSize, 0, groundLayer);
    }

    public void SetGroundBoxValue()
    {
        groundBoxOrigin = (Vector2)col2D.bounds.center + Vector2.down * col2D.bounds.extents.y;
        groundBoxSize = new Vector2((col2D.bounds.extents.x - 0.1f) * 2, 0.2f);
    }

    public void ChangeGravityScale()
    {
        if (rb2D.velocity.y < 0 && !isOnGround())
        {
            rb2D.gravityScale = normalGravityScale * DownGravityScaleMultiply;
        }
        else
        {
            rb2D.gravityScale = normalGravityScale;
        }
    }

    public void ChangeVel(Vector2 vel)
    {
        rb2D.velocity = vel;
    }
}
