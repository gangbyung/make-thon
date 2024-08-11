using UnityEngine;

public class DefaultEnemy : Enemy
{
    private float moveSpeed = 3f;

    private void Update()
    {
        Vector3 moveDirection = GetPlayerDirection() * moveSpeed * Time.deltaTime;

        transform.position += moveDirection;
    }
}
