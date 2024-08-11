using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public float maxHp = 100f;
    private float currentHp = 100f;
    public Slider hpSlider;
    void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // 플레이어가 사망했을 때의 로직
        Debug.Log("Player died");
        // 예를 들어, 게임 오버 화면을 표시하거나 재시작할 수 있습니다.
    }

    
    void Update()
    {
        hpSlider.value = currentHp;
    }
}
