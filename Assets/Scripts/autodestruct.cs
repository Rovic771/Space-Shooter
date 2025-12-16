using UnityEngine;

public class autodestruct : MonoBehaviour
{
    [SerializeField] float timeDelay = 15f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,timeDelay);
    }
}
