using UnityEngine;

public class scrolling: MonoBehaviour
{
    [Header("Réglages")]
    [SerializeField] float speed = 5f;      
    [SerializeField] float length = 20f;    

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        
        if (transform.position.z < -length)
        {
            RepositionBackground();
        }
    }

    void RepositionBackground()
    {
        Vector3 newPos = new Vector3(0, 0, length * 2);
        transform.position += newPos;
    }
}