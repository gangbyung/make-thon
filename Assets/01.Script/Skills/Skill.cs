using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    private float remainingCooldown = 0;
    private void Update()
    {
        if (remainingCooldown > 0)
        {
            remainingCooldown = Mathf.Max(0, remainingCooldown - Time.deltaTime);
            return;
        }
        if (!Input.GetKey(GetTriggerKey())) return;
        
        bool success = Fire();

        if (success)
        {
            remainingCooldown = GetCooldown();
        }
    }
    public abstract bool Fire();
    public abstract int GetCooldown();
    public abstract KeyCode GetTriggerKey();

    public float GetRemainingCooldown()
    {
        return remainingCooldown;
    }

    public bool CanFire()
    {
        return remainingCooldown == 0;
    }

    public void Cooldown()
    {
        remainingCooldown = GetCooldown();
    }

}
