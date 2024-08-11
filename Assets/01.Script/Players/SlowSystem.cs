using UnityEngine;

public class SlowSystem : PlayerHp
{
    // 느리게 만들 시간 배율
    public float slowMotionScale = 0.5f;

    // 원래 시간 배율을 저장하기 위한 변수
    private float originalTimeScale;

    // 체력 감소 속도
    public float damagePerSecond = 5f;

    void Start()
    {
        // 원래 시간 배율을 저장합니다.
        originalTimeScale = Time.timeScale;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            
            

            // 시간을 느리게 만듭니다.
            Time.timeScale = slowMotionScale;

            
        }
        else
        {
            // 시간이 원래 배율로 돌아갑니다.
            Time.timeScale = originalTimeScale;
        }
    }
}
