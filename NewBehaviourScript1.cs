using UnityEngine;

public class AimShake : MonoBehaviour
{
    public Transform weaponTransform; // 引用武器或手模型的Transform
    public float maxShakeDistance = 5f; // 开始颤抖的最大距离
    public float shakeIntensity = 0.1f; 
    public float shakeSpeed = 10f; 

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = weaponTransform.localPosition;
    }

    void Update()
    {
        // 从屏幕中心射出射线
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            float distance = Vector3.Distance(ray.origin, hit.point);

            // 应用颤抖效果
            if (distance <= maxShakeDistance)
            {
                float shakeAmount = (maxShakeDistance - distance) / maxShakeDistance * shakeIntensity;
                Vector3 shakeOffset = new Vector3(
                    Mathf.Sin(Time.time * shakeSpeed) * shakeAmount,
                    Mathf.Cos(Time.time * shakeSpeed) * shakeAmount,
                    0
                );
                weaponTransform.localPosition = originalPosition + shakeOffset;
            }
            else
            {
                // 超出
                weaponTransform.localPosition = originalPosition;
            }
        }
        else
        {
            // 未检测到
            weaponTransform.localPosition = originalPosition;
        }
    }
}
