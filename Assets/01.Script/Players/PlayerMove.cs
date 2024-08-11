using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    public static PlayerMove Instance { get; private set; }

    public float moveSpeed = 10f;         // ĳ���� �⺻ �̵� �ӵ�
    public float dashSpeed = 20f;        // ��� �ӵ�
    public float dashDuration = 0.2f;    // ��� ���� �ð�
    public float dashCooldown = 5f;      // ��� ��ٿ� �ð�

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 dashDirection;

    private float lastDashTime = -5f;    // �ʱ� ��ٿ� ���¸� ���� -5�� ����
    public float cooldownTimeLeft = 0f; // ��ٿ� ���� �ð�

    private bool isDashing = false;

    private float dashTimeLeft;

    public Hud hud; // Hud �ν��Ͻ�

    private SpriteRenderer spriteRenderer; // SpriteRenderer �߰�
    private Animator animator;

    void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� ������ �� ���� �ÿ��� ��ü�� �����մϴ�.
        }
        else
        {
            Destroy(gameObject); // �̹� �����ϴ� �ν��Ͻ��� ���� ��� ���� ��ü�� �ı��մϴ�.
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer ������Ʈ ��������

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

            // ������ �Է��� ���� ���, �ӵ��� 0���� ����
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
            // E Ű�� ������ �� ��� ����
            Vector2 inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            if (inputDirection != Vector2.zero)
            {
                StartDash(inputDirection);
            }
        }

        // ��� ���� �ƴ� ���� �̵� ���� ����
        if (true)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            movement = new Vector2(horizontal, vertical).normalized;

            // �̵� ���⿡ ���� ��������Ʈ�� x ���� ������
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
                rb.velocity = Vector2.zero; // ��ð� ���� �� �ӵ��� 0���� ����
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
        dashDirection = direction; // ��� ���� ����
        lastDashTime = Time.time;  // ��� ��ٿ� �ʱ�ȭ
        cooldownTimeLeft = dashCooldown; // ��ٿ� �ʱ�ȭ

        // ��� ���⿡ ���� ��������Ʈ�� x ���� ������
        if (direction.x != 0)
        {
            spriteRenderer.flipX = direction.x < 0;
        }
    }
}
