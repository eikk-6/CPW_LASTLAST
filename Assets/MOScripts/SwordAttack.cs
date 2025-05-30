using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float swingAngle = 90f;       // 휘두를 각도
    public float swingSpeed = 360f;      // 회전 속도 (도/초)
    public float cooldownTime = 1f;      // 공격 간격

    public bool reverseSwing = false;    // 반대로 휘두를지 여부 (인스펙터에서 체크 가능)

    private bool isSwinging = false;
    private float lastAttackTime;

    private Quaternion originalRotation;
    private Quaternion targetRotation;

    private void Start()
    {
        originalRotation = transform.localRotation;
    }

    public void Attack()
    {
        if (Time.time - lastAttackTime < cooldownTime || isSwinging) return;

        lastAttackTime = Time.time;
        StartCoroutine(Swing());
    }

    private System.Collections.IEnumerator Swing()
    {
        isSwinging = true;

        // reverseSwing이 true면 -swingAngle, 아니면 swingAngle 사용
        float angle = reverseSwing ? -swingAngle : swingAngle;

        // 왼쪽 → 오른쪽 스윙 (대각선 가능)
        targetRotation = originalRotation * Quaternion.Euler(0f, angle, 0f);

        // 회전 시작
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * (swingSpeed / swingAngle);
            transform.localRotation = Quaternion.Slerp(originalRotation, targetRotation, t);
            yield return null;
        }

        // 되돌리기
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * (swingSpeed / swingAngle);
            transform.localRotation = Quaternion.Slerp(targetRotation, originalRotation, t);
            yield return null;
        }

        isSwinging = false;
    }
}
