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
        // R키를 누르면 재장전
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && ui.gameState == GameState.Play)
        {
            StartCoroutine(ReloadCoroutine());
        }

        // 좌클릭을 누르면 발사
        if (Input.GetMouseButtonDown(0) && !isReloading)
        {
            Fire();
        }
    }
    void Fire()// 탄환 프리팹 생성 및 발사
    {
        if(ui.gameState != GameState.Play) //레벨업 해서 팝업창 뜰시 함수 비활성화
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
        float reloadTime = 2f; // 이후 레벨업 등 스펙업 요소 구현 시 어떻게??
        yield return new WaitForSeconds(reloadTime);
        Reload();
        isReloading = false;
    }
    void Reload()
    {
        currentAmmo = maxAmmo;
    }
}
