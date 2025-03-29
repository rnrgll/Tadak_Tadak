using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public float targetAngle = -40f; // 목표 기울기 (오른쪽으로 기울어지는 각도)
    public float returnSpeed = 2.0f; // 원래 각도로 돌아가는 속도
    public float tiltSpeed = 1.5f; // 기울어지는 속도
    public float rayDistance = 2.0f; // 물을 뿌리는 거리
    public Transform spoutPos; // 물이 나오는 위치 (노즐)

    private float currentAngle = 0f; // 현재 회전 각도
    private float lerpProgress = 0f; // 기울기 변화량 추적
    private bool isTilting = false; // 기울이는 중인지 체크
    private float lastSprayTime = 0f; //️ 마지막으로 물을 뿌린 시간
    private float sprayRate = 10f; // 물을 뿌리는 속도 (초당 횟수)

    void Update()
    {
        HandleTilt(); // 기울기 조절
        if (isTilting) SprayWater(); // 기울어지는 중이면 물 뿌리기
    }

    void HandleTilt()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isTilting = true;
            lerpProgress += Time.deltaTime * tiltSpeed; // 목표 각도로 가는 속도 조절
        }
        else
        {
            isTilting = false;
            lerpProgress -= Time.deltaTime * returnSpeed; // 원래 각도로 돌아가는 속도 조절
        }

        lerpProgress = Mathf.Clamp01(lerpProgress); // 0~1 범위 유지
        currentAngle = Mathf.LerpAngle(0f, targetAngle, lerpProgress); // 부드러운 회전
        transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
    }

    void SprayWater()
    {
        if (Time.time - lastSprayTime < 1f / sprayRate) return; // 일정 간격 유지

        RaycastHit2D hit = Physics2D.Raycast(spoutPos.position, spoutPos.right, rayDistance);
        Debug.DrawRay(spoutPos.position, spoutPos.right * rayDistance, Color.blue, 0.1f); // 디버그용 레이 시각화

        if (hit.collider != null && hit.collider.CompareTag("Plant"))
        {
            hit.collider.GetComponent<Plant>().WaterReceived(); // 화분이 물을 받음
        }

        lastSprayTime = Time.time; //  물을 뿌린 시간 업데이트
    }
}