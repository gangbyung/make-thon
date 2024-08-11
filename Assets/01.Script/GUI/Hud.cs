using UnityEngine;
using TMPro;
using Unity.Mathematics;
using UnityEngine.UI;
public class Hud : MonoBehaviour
{

    public TextMeshProUGUI cooldownText; // UI �ؽ�Ʈ ���
    public Slider DashcoolSilder; //�뽬�� �����̴�

    void Start()
    {
        if (cooldownText == null)
        {
            Debug.LogError("CooldownText UI element not assigned in Hud!");
        }
    }
    void Update()
    {
        DashcoolSilder.value = PlayerMove.Instance.cooldownTimeLeft;
    }
    public void UpdateCooldownUI(float cooldownTimeLeft)
    {
        if (cooldownText != null)
        {
            if (cooldownTimeLeft > 0)
            {
                cooldownText.text = $"Cooldown: {cooldownTimeLeft:F1}s";
            }
            else
            {
                cooldownText.text = "Cooldown: Ready!";
            }
        }
    }
    
}
