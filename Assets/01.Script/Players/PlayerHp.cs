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
        // �÷��̾ ������� ���� ����
        Debug.Log("Player died");
        // ���� ���, ���� ���� ȭ���� ǥ���ϰų� ������� �� �ֽ��ϴ�.
    }

    
    void Update()
    {
        hpSlider.value = currentHp;
    }
}
