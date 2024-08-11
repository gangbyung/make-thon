using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    private float speed = 30f;
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)

    {

        if (collision.gameObject.name != "Player") return;

        GameObject player = collision.gameObject;

        Debug.Log("플레이어 아야함");
        Destroy(gameObject);

    }
}
