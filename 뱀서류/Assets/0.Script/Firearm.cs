using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.UIElements;
using UnityEngine.SocialPlatforms;

public class Firearm : MonoBehaviour
{
    [SerializeField] public Player player;
    [SerializeField] public UI ui;
    [SerializeField] private SpriteRenderer weaponSprite;
    public GameObject bulletPrefab;
    public Text ammoText;
    public int maxAmmo = 10;
    public int currentAmmo;
    public bool isReloading = false;
    public float recoilDuration = 0.1f;
    public float recoilIntensity = 0.1f;
    public Vector3 originalPosition;
    public bool isRecoiling = false;
    public Vector3 worldOriginalPosition;
    public Cam cam;

    void Start()
    {
        ui = UI.instance;
        currentAmmo = maxAmmo;
        originalPosition = transform.localPosition;
    }
    void Update()
    {
        worldOriginalPosition = transform.parent.TransformPoint(originalPosition);
        // RŰ�� ������ ������
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && ui.gameState == GameState.Play)
        {
            StartCoroutine(ReloadCoroutine());
        }

        // ��Ŭ���� ������ �߻�
        if (Input.GetMouseButtonDown(0) && !isReloading && !isRecoiling)
        {
            StartCoroutine(Recoil());
            Fire();
        }
        //���콺 Ŀ�� ��ġ�� ���� ȸ��(�÷��̾��� flip�� ����)
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector2 aimDirection = (mousePosition - player.transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        weaponSprite.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (aimDirection.x < 0) //���ʺ���
        {
            weaponSprite.flipY = true;
            weaponSprite.transform.localPosition = new Vector3(0.157f, -0.256f, 0);
        }
        else if (aimDirection.x > 0) //�����ʺ���
        {
            weaponSprite.flipY = false;
            weaponSprite.transform.localPosition = new Vector3(-0.157f, -0.256f, 0);
        }
    }
    void Fire()// źȯ ������ ���� �� �߻�
    {
        if(ui.gameState != GameState.Play) //������ �ؼ� �˾�â ��� �Լ� ��Ȱ��ȭ
        {
            return;
        }
        if (currentAmmo > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, player.firePos.position, player.firePos.rotation);
            currentAmmo--;
            cam.UpdateAimCursorAndAmmoDisplay();
        }
    }
    IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        float reloadTime = 2f; // ���� ������ �� ����� ��� ���� �� ���??
        yield return new WaitForSeconds(reloadTime);
        Reload();
        isReloading = false;
    }
    void Reload()
    {
        currentAmmo = maxAmmo;
        cam.UpdateAimCursorAndAmmoDisplay();
    }
    private IEnumerator Recoil()
    {
        isRecoiling = true;

        float startTime = Time.time;
        float endTime = startTime + recoilDuration;

        Vector3 initialPosition = weaponSprite.transform.localPosition;
        Vector3 targetRecoilPosition = initialPosition + new Vector3(0, -recoilIntensity, 0);

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / recoilDuration;
            weaponSprite.transform.localPosition = Vector3.Lerp(initialPosition, targetRecoilPosition, t);
            yield return null;
        }

        weaponSprite.transform.localPosition = initialPosition;
        isRecoiling = false;
    }

}
