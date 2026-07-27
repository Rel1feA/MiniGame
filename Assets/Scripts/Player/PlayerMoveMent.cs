using RECode.REFramework;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private float maxFlyEnergy;
    [SerializeField]
    private float resumeFlyEnergySpeed;
    [SerializeField]
    private float useFlyEnergySpeed;
    [SerializeField]
    private float flySpeedMul;
    [SerializeField]
    private float flyEnergyMul;
    [SerializeField]
    private float flyEnergyResumeCoolTime;
    [SerializeField]
    private Slider energySlider;

    private float currentFlyEnergy;
    private float timer;

    private Rigidbody2D rb2D;
    private Collider2D col2D;
    private float normalGravityScale;

    public float VelX { get => rb2D.velocity.x; }
    public float VelY { get => rb2D.velocity.y; }
    public float FlySpeedMul { get => flySpeedMul; }

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();
    }

    private void Start()
    {
        SetGroundBoxValue();
        normalGravityScale = rb2D.gravityScale;
        currentFlyEnergy = maxFlyEnergy * flyEnergyMul;
    }

    private void Update()
    {
        SetGroundBoxValue();
        if(rb2D.velocity.y<0.1f)
        {
            StartFlyEnergyResumeTimer();
            if(timer>flyEnergyResumeCoolTime)
            {
                ResumeEnergy(Time.deltaTime);
            }
        }
        energySlider.value = GetFlyEnergyPercent();
    }

    private void FixedUpdate()
    {
        ChangeGravityScale();
    }

    public void UseEnergy(float detlta)
    {
        currentFlyEnergy-=useFlyEnergySpeed*detlta;
        if (currentFlyEnergy <= 0) currentFlyEnergy = 0;
    }

    public void ResumeEnergy(float detlta)
    {
        currentFlyEnergy += resumeFlyEnergySpeed * detlta;
        if (currentFlyEnergy > maxFlyEnergy*flyEnergyMul) currentFlyEnergy = maxFlyEnergy * flyEnergyMul;
    }

    public void ResetFlyEnergy()
    {
        currentFlyEnergy= 0;
    }

    public void AddMaxFlyMul(float value)
    {
        flyEnergyMul+=value;
    }

    public void AddFlySpeedMul(float value)
    {
        flySpeedMul += value;
    }

    public float GetFlyEnergyPercent()
    {
        return currentFlyEnergy / (maxFlyEnergy * flyEnergyMul);
    }


    public void Move(float input)
    {
        rb2D.velocity = new Vector2(input * speed, rb2D.velocity.y);
    }

    public void Fly()
    {
        if(currentFlyEnergy>0.5f)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, flySpeed*flySpeedMul);
            UseEnergy(Time.fixedDeltaTime);
            timer = 0;
        }
    }

    public void StartFlyEnergyResumeTimer()
    {
        if(timer<flyEnergyResumeCoolTime+1)
        timer += Time.deltaTime;
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
