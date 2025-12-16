using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.value,Random.value,Random.value);
    }
}
