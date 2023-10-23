using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingWeapon : MonoBehaviour
{
    /*public float speed = 20f;
    public int damage = 100;
    public float lifeTime = 5f;
    public float maxThrowDistance = 10f;
    public GameObject explosionEffectPrefab; // 폭발 이펙트 Prefab
    public float explosionDelay = 2f; // 폭발까지의 시간
    public float explosionRadius = 10f; //폭발 범위
    private Vector3 direction;
    private Player player; // 플레이어 참조
    public Sprite throwingWeaponSprite;
    public ThrowingWeapon throwingObjectPrefab;


    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = throwingWeaponSprite;
        gameObject.SetActive(false);
    }
private void Start()
    {
        player = FindObjectOfType<Player>(); // Player 객체를 찾아서 참조합니다.
    }

    private void Update()
    {
        if (UI.instance.gameState == GameState.Play)
        {
            // G키를 눌렀을 때
            if (Input.GetKeyDown(KeyCode.G))
            {
                ThrowWeapon();
            }

            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void ThrowWeapon()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 throwDirection = (mousePos - player.transform.position).normalized;
        float distanceToMouse = Vector3.Distance(player.transform.position, mousePos);

        if (distanceToMouse > maxThrowDistance)
        {
            mousePos = player.transform.position + throwDirection * maxThrowDistance;
        }

        direction = (mousePos - transform.position).normalized;

        // Instantiate ThrowingObject prefab instead of 'this'
        ThrowingWeapon grenade = Instantiate(throwingObjectPrefab, player.transform.position, Quaternion.identity).GetComponent<ThrowingWeapon>();
        gameObject.SetActive(true);
        grenade.SetDirection(direction);
        StartCoroutine(grenade.ExplodeAfterDelay(explosionDelay));
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    public IEnumerator ExplodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hitCollider in hitColliders)
        {
            Monster monster = hitCollider.GetComponent<Monster>();
            if (monster)
            {
                monster.Dead(0.5f, damage);
            }
        }

        Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster = collision.GetComponent<Monster>();
        if (monster)
        {
            monster.Dead(0.5f, damage);
            // 폭발 이펙트 생성
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }*///아직 미구현..버그투성이..ㅅㅂ
}
