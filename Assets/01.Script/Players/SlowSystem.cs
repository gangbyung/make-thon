using UnityEngine;

public class SlowSystem : PlayerHp
{
    // ������ ���� �ð� ����
    public float slowMotionScale = 0.5f;

    // ���� �ð� ������ �����ϱ� ���� ����
    private float originalTimeScale;

    // ü�� ���� �ӵ�
    public float damagePerSecond = 5f;

    void Start()
    {
        // ���� �ð� ������ �����մϴ�.
        originalTimeScale = Time.timeScale;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            
            

            // �ð��� ������ ����ϴ�.
            Time.timeScale = slowMotionScale;

            
        }
        else
        {
            // �ð��� ���� ������ ���ư��ϴ�.
            Time.timeScale = originalTimeScale;
        }
    }
}
