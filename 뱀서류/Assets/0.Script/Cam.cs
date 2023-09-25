using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] private Transform target;

    private float shakeTime;
    private float shakeLevel;

    private Vector3 shakeOffset = Vector3.zero; // 흔들림 오프셋

    private bool isShaking = false;

    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10f);
            Vector3 desiredPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5);
            transform.position = desiredPosition + shakeOffset; // 원래 목표 위치에 흔들림 오프셋을 추가
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnCameraShake(0.05f, 0.05f);
        }
    }

    public void OnCameraShake(float shakeTime = 1.0f, float shakeLevel = 0.01f)
    {
        if (isShaking) return;

        this.shakeLevel = shakeLevel;
        this.shakeTime = shakeTime;

        StartCoroutine(ShakeCamera());
    }

    private IEnumerator ShakeCamera()
    {
        isShaking = true;
        float endTime = Time.time + shakeTime;

        while (Time.time < endTime)
        {
            Vector2 randomOffset = Random.insideUnitCircle * shakeLevel;
            shakeOffset = new Vector3(randomOffset.x, randomOffset.y, 0); // 흔들림 오프셋을 계산

            yield return null;
        }

        shakeOffset = Vector3.zero; // 흔들림이 끝나면 오프셋을 0으로 설정
        isShaking = false;
    }
}
