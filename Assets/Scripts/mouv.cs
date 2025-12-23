using UnityEngine;

public class mouv : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] Vector3 direction;
    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = direction * speed;
    }
}
