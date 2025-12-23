using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;

    [SerializeField] float speed = 5f;
    [SerializeField] float tilt = 45f;
    [SerializeField] float xLimiteLeft;
    [SerializeField] float xLimiteRight;
    [SerializeField] float zLimiteDown;
    [SerializeField] float zLimiteUp;

    [SerializeField] GameObject LaserPrefab;
    [SerializeField] GameObject LaserSpawn;

    [SerializeField] float FireDelay = 0.2f;
    float LastShotTime;
    void Start()
    {
        LastShotTime = -FireDelay;
    }
    
    void Update()
    {
        //gestion du mouvement
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        direction.Normalize();
        GetComponent<Rigidbody>().linearVelocity = direction * speed;
        //roulis
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 0, -horizontalInput * tilt);

        //Limitation aux bornes du jeu
        if (gameObject.tag == "Player")
        {
            float position_x = transform.position.x;
            float position_z = transform.position.z;

            position_x = Mathf.Clamp(position_x, xLimiteLeft, xLimiteRight);
            position_z = Mathf.Clamp(position_z, zLimiteDown, zLimiteUp);

            Vector3 newPosition = new Vector3(position_x, 0, position_z);
            transform.position = newPosition;
        }

        //gestion du tir
        if (Input.GetMouseButtonDown(0) && Time.time > LastShotTime + FireDelay)
        {
            Instantiate(LaserPrefab, LaserSpawn.transform.position,Quaternion.identity);
            GetComponent<AudioSource>().Play();
            LastShotTime = Time.time;
        }
    }
}
