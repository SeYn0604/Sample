using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class RangedMonster : Monster
{
    [SerializeField] private GameObject bulletPrefab; // 투사체 프리팹
    [SerializeField] private Animator rangedZombieAnimator;
    private float shootRange = 12f; // 원거리 공격을 시작할 거리
    private float shootInterval = 1f; // 발사 간격
    private bool isRangedAttackCooldown = false;
    public float bulletSpeed = 0.1f;

    protected override void Update()
    {
        base.Update(); // 부모 클래스의 Update 메서드를 호출

        float distance = Vector2.Distance(p.transform.position, transform.position);

        if (distance <= shootRange && !isRangedAttackCooldown)
        {
            StartCoroutine(ShootAtPlayer());
            isRangedAttackCooldown = true;
            StartCoroutine(RangedAttackCooldownRoutine());
        }
    }
    public override void Dead(float freezeTime, int damage)
    {
        base.Dead(1f, 100);
        if (hp <= 0)
        {
            animator.SetBool("Dead", false);
            animator.SetBool("RangedZombieDead", true);
        }
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
    public override void SetPlayer(Player p)
    {
        base.SetPlayer(p);
    }
    IEnumerator ShootAtPlayer()
    {
        for (int i = 0; i < 1; i++) // 필요에 따라 수정 가능
        {
            if (hp <= 0)
            {
                yield break;
            }
            Vector2 bulletDirection = (p.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg - 90; // 90도 회전
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed* 0.05f;
            yield return new WaitForSeconds(3f); // 0.5초 간격으로 발사, 필요에 따라 수정 가능
        }
    }
    IEnumerator RangedAttackCooldownRoutine()
    {
        yield return new WaitForSeconds(shootInterval);
        isRangedAttackCooldown = false;
    }
}
