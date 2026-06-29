using UnityEngine;

public class bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;

    void Start()
    {
        TryGetComponent(out _rigidbody);
    }

    void Update()
    {
        RayCast();
    }

    public void RayCast()
    {
        Vector3 posA = transform.position + _rigidbody.linearVelocity;
        Vector3 posB = transform.position;
        float distance = Vector3.Distance(posA, posB);

        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        RaycastHit hit;
        bool collition = Physics.Raycast(ray, out hit, 100, 0 >> 1);
        if (collition)
        {
            Debug.Log("オブジェクト検出");
            if(hit.transform.name.Contains("Enemy"))
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
