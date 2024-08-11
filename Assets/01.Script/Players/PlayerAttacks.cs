using System.Collections;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public Animator animator; // Animator 컴포넌트
    public float attackRange = 10f; // 히트스캔 공격의 범위
    public LayerMask enemyLayer; // 적 레이어
    public float damageAmount = 10f; // 공격 데미지

    private void Update()
    {
        // 마우스 클릭 시 공격
        if (Input.GetMouseButtonDown(0))
        {
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        if (animator != null)
        {
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsAttacking", true);
            animator.SetTrigger("Attack");
            StartCoroutine(Wait());
            Debug.Log("Attack trigger called");
        }
        else
        {
            Debug.LogError("Animator is not assigned.");
        }

        // 마우스 위치와 플레이어의 위치 차이를 계산하여 방향을 설정
        Vector2 attackDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);

        // 2D 환경에서는 `Vector2`를 사용합니다
        RaycastHit2D hit = Physics2D.Raycast(transform.position, attackDirection.normalized, attackRange, enemyLayer);

        if (hit.collider != null)
        {
            // 적을 맞춘 경우
            Debug.Log("Hit: " + hit.collider.name);
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage();
            }
        }
    }

    IEnumerator Wait()
    {
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsAttacking", true);
        yield return null;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 타겟 상태로 전환될 때까지 대기
        while (stateInfo.IsName("Attack1") == false && stateInfo.IsName("Attack2") == false)
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        // 타겟 상태의 애니메이션이 끝날 때까지 대기
        while (stateInfo.IsName("Idel") == false)
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        animator.SetBool("IsAttacking", false);
    }
}
