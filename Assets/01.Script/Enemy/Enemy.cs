using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public List<GameObject> lifes;
    public int remainingLife = 4;

    private Vector3 lifeObjectGap = new Vector3(0.5f, 0, 0);
    private string lifePrefabPath = "Prefabs/LifePrefab";
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        animator = GetComponent<Animator>();
        Transform lifeParent = transform.Find("Lifes");

        GameObject lifePrefab = Resources.Load<GameObject>(lifePrefabPath);

        if (lifePrefab == null) throw new System.Exception("Cannot find LifePrefab");

        Vector3 lifeObjectsCenter = (lifeObjectGap * (remainingLife - 1)) / 2;

        for (int i = 0; i < remainingLife; i++)
        {
            GameObject life = Instantiate(lifePrefab);
            life.transform.parent = lifeParent;
            life.transform.position = lifeParent.position + lifeObjectGap * i - lifeObjectsCenter;
            lifes.Add(life);
        }



    }

    void TriggerExplosion()
    {
        if (remainingLife >= 0)
        {
            Animator animator = lifes[remainingLife].GetComponent<Animator>();
            animator.SetTrigger("Explosion");
        }
        if (remainingLife == 0)
        {
            Animator animator = lifes[0].GetComponent<Animator>();
            StartCoroutine(Kill(animator));
        }
    }

    IEnumerator Kill(Animator animator)
    {
        yield return null; 
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 타겟 상태로 전환될 때까지 대기
        while (stateInfo.IsName("LifeExplosion") == false) 
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        // 타겟 상태의 애니메이션이 끝날 때까지 대기
        while (stateInfo.normalizedTime < 1.0f) 
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0); 
        }

        Debug.Log("애니메이션 종료!");
        Destroy(gameObject);
    }
    public void TakeDamage()
    {
        animator.SetTrigger("HitTrigger");
        remainingLife--;
        TriggerExplosion();
    }
}
