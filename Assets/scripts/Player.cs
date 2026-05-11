using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Header("bullet")]
    GameObject bulletPrefab;
    [SerializeField]
    Transform shotPoint;
    [SerializeField]
    int bulletSpeed = 30;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnMove(InputValue value)
    {
        Debug.Log($"Move[{value.Get<Vector3>()}]");

        if (value.Get<Vector3>() == Vector3.zero)
            return;

        Vector3 move = new Vector3(
        　　value.Get<Vector3>().x, 
            value.Get<Vector3>().y,
            value.Get<Vector3>().z);

        if (transform.position.x + value.Get<Vector3>().x > 8 
            || transform.position.x + value.Get<Vector3>().x < -8)
            return;

        if (transform.position.y + value.Get<Vector3>().y > 5
            || transform.position.y + value.Get<Vector3>().y < -5)
            return;

        move.x = Mathf.Round(move.x);
        move.y = Mathf.Round(move.y);

        //プレイヤーの移動
        transform.Translate(move);
    }

    public void OnAttack(InputValue value)
    {
        Debug.Log($"Attack[{value.Get<float>()}]");

        GameObject bullet = Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(shotPoint.forward * bulletSpeed, ForceMode.Impulse);

        Destroy(bullet, 5.0f);
    }
}
