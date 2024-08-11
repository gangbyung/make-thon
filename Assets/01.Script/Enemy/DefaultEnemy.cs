using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DefaultEnemy : Enemy
{
    private float moveSpeed = 3f;
    private bool isCharging = false;
    Transform projectileParent;
    Vector3 pos;
    private GameObject projectilePrefab;

    public override void Initialized()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        projectileParent = transform.Find("Attack");
        pos = projectileParent.transform.position - spriteRenderer.bounds.center;
        projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile");
    }

    private void Update()
    {
        if (isCharging) return;
        Vector3 moveDirection = GetPlayerDirection() * moveSpeed * Time.deltaTime;

        transform.position += moveDirection;

        if (moveDirection != Vector3.zero)
        {
            spriteRenderer.flipX = moveDirection.x > 0;
            if (moveDirection.x > 0)
            {
                projectileParent.position = spriteRenderer.bounds.center + pos;
            }
            else
            {
                projectileParent.position = spriteRenderer.bounds.center + new Vector3(-pos.x, pos.y, 0);
            }
        }
    }

    private void Attack()
    {
        isCharging = true;

        animator.SetTrigger("Attack");

        StartCoroutine(WaitAttack());
    }
    public IEnumerator WaitAttack()
    {
        yield return null;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 타겟 상태로 전환될 때까지 대기
        while (!stateInfo.IsName("Enemy_Attack"))
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        while (stateInfo.length * (1 - stateInfo.normalizedTime) > 0.3f)
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        GameObject projectile = Instantiate(projectilePrefab);
        projectile.transform.position = new Vector3(projectileParent.position.x, projectileParent.position.y, projectileParent.position.z);
        Projectile projScr = projectile.GetComponent<Projectile>();
        projScr.direction = GetPlayerRawDirection();

        while (!stateInfo.IsName("Enemy_Idel"))
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        isCharging = false;
    }

    public override void OnDamage()
    {
        Attack();
    }
}
