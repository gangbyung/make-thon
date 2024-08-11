using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    public static PlayerMove Instance { get; private set; }

    public float moveSpeed = 10f;         // 캐릭터 기본 이동 속도
    public float dashSpeed = 20f;        // 대시 속도
    public float dashDuration = 0.2f;    // 대시 지속 시간
    public float dashCooldown = 5f;      // 대시 쿨다운 시간

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 dashDirection;

    private float lastDashTime = -5f;    // 초기 쿨다운 상태를 위해 -5로 설정
    public float cooldownTimeLeft = 0f; // 쿨다운 남은 시간

    private bool isDashing = false;

    private float dashTimeLeft;

    public Hud hud; // Hud 인스턴스

    private SpriteRenderer spriteRenderer; // SpriteRenderer 추가
    private Animator animator;

    void Awake()
    {
        // 싱글톤 패턴 적용
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 게임 오버나 씬 변경 시에도 객체를 유지합니다.
        }
        else
        {
            Destroy(gameObject); // 이미 존재하는 인스턴스가 있을 경우 현재 객체를 파괴합니다.
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer 컴포넌트 가져오기

        if (hud == null)
        {
            Debug.LogError("Hud instance not assigned in PlayerController!");
        }
    }

    void Update()
    {
        HandleInput();
        HandleDash();
        if (hud != null)
        {
            hud.UpdateCooldownUI(cooldownTimeLeft);
        }
    }

    void FixedUpdate()
    {
        if (animator.GetBool("IsAttacking"))
        {
            movement = Vector2.zero;
            rb.velocity = Vector2.zero;
            animator.SetBool("IsRunning", false);
            return;
        }
        animator.SetBool("IsRunning", movement != Vector2.zero);
        if (isDashing)
        {
            rb.velocity = dashDirection * dashSpeed;
        }
        else
        {
            rb.velocity = movement * moveSpeed;

            // 움직임 입력이 없을 경우, 속도를 0으로 설정
            if (movement == Vector2.zero)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    void HandleInput()
    {
        if (animator.GetBool("IsAttacking"))
        {
            movement = Vector2.zero;
            rb.velocity = Vector2.zero;
            animator.SetBool("IsRunning", false);
            return;
        }
        if (Input.GetKeyDown(KeyCode.E) && !isDashing && Time.time - lastDashTime >= dashCooldown)
        {
            // E 키가 눌렸을 때 대시 실행
            Vector2 inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            if (inputDirection != Vector2.zero)
            {
                StartDash(inputDirection);
            }
        }

        // 대시 중이 아닐 때만 이동 방향 설정
        if (true)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            movement = new Vector2(horizontal, vertical).normalized;

            // 이동 방향에 따라 스프라이트의 x 축을 뒤집기
            if (horizontal != 0)
            {
                spriteRenderer.flipX = horizontal > 0;
            }
        }
    }

    void HandleDash()
    {
        if (animator.GetBool("IsAttacking"))
        {
            movement = Vector2.zero;
            rb.velocity = Vector2.zero;
            animator.SetBool("IsRunning", false);
            return;
        }
        if (isDashing)
        {
            dashTimeLeft -= Time.deltaTime;
            if (dashTimeLeft <= 0)
            {
                isDashing = false;
                rb.velocity = Vector2.zero; // 대시가 끝난 후 속도를 0으로 설정
            }
        }
        else
        {
            cooldownTimeLeft = Mathf.Max(0, dashCooldown - (Time.time - lastDashTime));
        }
    }

    void StartDash(Vector2 direction)
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        dashDirection = direction; // 대시 방향 설정
        lastDashTime = Time.time;  // 대시 쿨다운 초기화
        cooldownTimeLeft = dashCooldown; // 쿨다운 초기화

        // 대시 방향에 따라 스프라이트의 x 축을 뒤집기
        if (direction.x != 0)
        {
            spriteRenderer.flipX = direction.x < 0;
        }
    }
}
