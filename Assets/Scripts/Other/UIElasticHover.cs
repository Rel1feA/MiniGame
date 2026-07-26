using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

/// <summary>
/// 鼠标悬停时让 UI 元素产生 Q 弹挤压拉伸效果。
/// 挂载到带有 RectTransform 的 UI 物体上即可。
/// </summary>
public class UIElasticHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("动画参数")]
    [SerializeField, Range(0.01f, 0.5f)] private float squeezeAmount = 0.05f;   // 第一阶段挤压/拉伸的幅度
    [SerializeField, Range(0.01f, 0.5f)] private float stretchAmount = 0.03f;  // 第二阶段反向拉伸的幅度
    [SerializeField, Range(0.05f, 0.5f)] private float phaseDuration = 0.08f;  // 每个变形阶段的时长
    [SerializeField, Range(0.1f, 1f)]     private float recoverDuration = 0.25f;// 回弹恢复时长
    [SerializeField] private Ease recoverEase = Ease.OutBack;                 // 回弹缓动类型

    private RectTransform rectTransform;
    private Sequence animationSequence;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("UIElasticHover 需要挂载在含有 RectTransform 的物体上", this);
        }
    }

    private void OnDestroy()
    {
        // 销毁时安全地终止动画
        animationSequence?.Kill();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 终止上一次动画（如果还在播放），并重置缩放
        animationSequence?.Kill(true);
        rectTransform.localScale = Vector3.one;

        // 创建新序列
        animationSequence = DOTween.Sequence();

        // 第一阶段：挤压（左右收缩 X，上下扩展 Y）
        float squeezedX = 1f - squeezeAmount;
        float squeezedY = 1f + squeezeAmount;
        animationSequence.Append(rectTransform.DOScale(new Vector3(squeezedX, squeezedY, 1f), phaseDuration));

        // 第二阶段：回弹式拉伸（左右扩展 X，上下收缩 Y）
        float stretchedX = 1f + stretchAmount;
        float stretchedY = 1f - stretchAmount;
        animationSequence.Append(rectTransform.DOScale(new Vector3(stretchedX, stretchedY, 1f), phaseDuration));

        // 第三阶段：弹性恢复到原始大小
        animationSequence.Append(rectTransform.DOScale(Vector3.one, recoverDuration).SetEase(recoverEase));

        // 防止序列完成后内存驻留
        animationSequence.OnComplete(() => animationSequence = null);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 鼠标离开时直接终止动画，并强制恢复原始大小
        animationSequence?.Kill(true);
        rectTransform.localScale = Vector3.one;
        animationSequence = null;
    }
}
