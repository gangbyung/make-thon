using UnityEngine;

public class TestSkill : Skill
{

    public override bool Fire()
    {
        Debug.Log("TestSkill Fired");

        return true;
    }

    public override int GetCooldown()
    {
        return 5;
    }

    public override KeyCode GetTriggerKey()
    {
        return KeyCode.X;
    }
}
