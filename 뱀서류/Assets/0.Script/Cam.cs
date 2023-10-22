using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cam : MonoBehaviour
{
    public float xOffset = 15f;
    public float yOffset = 0f;
    [SerializeField] private Transform target;
    [SerializeField] public Firearm firearm;
    [SerializeField] public GameObject aimSprite;
    [SerializeField] public TextMeshProUGUI ammoText;
    [SerializeField] private UI ui;

    private float shakeTime;
    private float shakeLevel;

    private Vector3 shakeOffset = Vector3.zero;
    private bool isShaking = false;
    public bool isLevelUpPopupActive = false;

    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10f);
            Vector3 desiredPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5);
            transform.position = desiredPosition + shakeOffset;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !firearm.isReloading && firearm.currentAmmo > 0)
        {
            if (ui == null || ui.gameState != GameState.Pause)
            {
                OnCameraShake(0.05f, 0.05f);
            }
        }

        UpdateAimCursorAndAmmoDisplay();
    }

    void UpdateAimCursorAndAmmoDisplay() // 장탄 표기가 마우스 에임을 따라가게끔
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        aimSprite.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, aimSprite.transform.position);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(ammoText.canvas.GetComponent<RectTransform>(), screenPoint, ammoText.canvas.worldCamera, out localPoint);
        ammoText.rectTransform.anchoredPosition = localPoint + new Vector2(xOffset, yOffset);
        ammoText.text = $"{firearm.currentAmmo}/{firearm.maxAmmo}";
    }

    public void OnCameraShake(float shakeTime = 1.0f, float shakeLevel = 0.01f)
    {
        if (isShaking || (ui != null && ui.gameState == GameState.Pause) || ui.isLevelUpPopupActive)
            return;

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
            shakeOffset = new Vector3(randomOffset.x, randomOffset.y, 0);

            yield return null;
        }

        shakeOffset = Vector3.zero;
        isShaking = false;
    }
}
