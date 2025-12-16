using UnityEngine;

public class mouv : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] Vector3 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = direction * speed;
    }
}
