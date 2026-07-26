using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TreasureItem : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public TreasureData Data { get; private set; }

    private void Awake()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

    public void Init(TreasureData data)
    {
        Data = data;
        spriteRenderer.sprite = data.icon;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            
        }
    }
}
