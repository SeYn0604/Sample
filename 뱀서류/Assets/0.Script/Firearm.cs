using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Firearm : MonoBehaviour
{
    [SerializeField] public Player player;
    [SerializeField] public UI ui;
    public GameObject bulletPrefab;
    public Text ammoText;
    public int maxAmmo = 10;
    public int currentAmmo;
    public bool isReloading = false;

    void Start()
    {
        ui = UI.instance;
        currentAmmo = maxAmmo;
    }
    void Update()
    {
        // RŰ�� ������ ������
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && ui.gameState == GameState.Play)
        {
            StartCoroutine(ReloadCoroutine());
        }

        // ��Ŭ���� ������ �߻�
        if (Input.GetMouseButtonDown(0) && !isReloading)
        {
            Fire();
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
    }
}
