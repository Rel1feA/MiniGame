using UnityEngine;

/// <summary>
/// CRT 扫描线后处理效果。
/// 挂载到场景的 Main Camera 上即可生效，不需要修改任何其他代码。
/// </summary>
[RequireComponent(typeof(Camera))]
public class CRTScanlineEffect : MonoBehaviour
{
    [Header("扫描线")]
    [SerializeField, Range(0f, 1f)]  private float scanlineIntensity = 0.35f;
    [SerializeField, Range(50, 500)]  private int   scanlineCount    = 180;
    [SerializeField, Range(-5f, 5f)]  private float scanlineSpeed    = 0f;

    [Header("CRT 屏幕效果")]
    [SerializeField, Range(0f, 0.1f)] private float curvature        = 0.015f;
    [SerializeField, Range(0f, 1f)]   private float vignetteStrength = 0.15f;

    [Header("亮度")]
    [SerializeField, Range(0.5f, 1.5f)] private float brightness = 1.05f;

    private Material material;
    private Shader   shader;

    private void Awake()
    {
        shader = Shader.Find("Hidden/CRTScanline");
        if (shader != null)
            material = new Material(shader);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material == null)
        {
            Graphics.Blit(src, dest);
            return;
        }

        material.SetFloat("_ScanlineIntensity", scanlineIntensity);
        material.SetFloat("_ScanlineCount",     scanlineCount);
        material.SetFloat("_ScanlineSpeed",     scanlineSpeed);
        material.SetFloat("_Curvature",         curvature);
        material.SetFloat("_VignetteStrength",  vignetteStrength);
        material.SetFloat("_Brightness",        brightness);

        Graphics.Blit(src, dest, material);
    }

    private void OnDestroy()
    {
        if (material != null)
        {
            if (Application.isPlaying)
                Destroy(material);
            else
                DestroyImmediate(material);
        }
    }
}
