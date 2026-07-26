using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public Transform player;        // 玩家 Transform
    public Transform treasure;      // 宝物 Transform

    private void Update()
    {
        // 如果任一引用为空，跳过
        if (player == null || treasure == null)
            return;

        // 1. 计算从玩家到宝物的方向向量（2D）
        Vector2 direction = (Vector2)(treasure.position - player.position).normalized;

        // 2. 如果方向为零向量，不旋转（或者保持当前）
        if (direction.sqrMagnitude < 0.0001f)
            return;

        // 3. 计算从“向上”到目标方向的夹角（度）
        float angle = Vector2.SignedAngle(Vector2.up, direction);

        // 4. 应用旋转（绕 Z 轴）
        transform.localEulerAngles=new Vector3(0,0,angle);
    }
}
