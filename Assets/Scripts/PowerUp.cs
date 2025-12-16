using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] GameObject Ally1;
    [SerializeField] GameObject Ally2;
    [SerializeField] GameObject Player;
    public bool FollowPlayer = false;
    
    float horizontalInput;
    float verticalInput;
    [SerializeField] float speed = 5f;
    [SerializeField] float tilt = 45f;
    [SerializeField] GameObject LaserPrefab;
    [SerializeField] GameObject LaserSpawn;

    [SerializeField] float FireDelay = 0.2f;
    float LastShotTime;
    public void FixedUpdate()
    {
        //if (FollowPlayer == true)
        //{
        //    if (Ally1.transform.position.z != Player.transform.position.z - 2)
        //    {
        //        Ally1.transform.position = new Vector3(Ally1.transform.position.x, Ally1.transform.position.y, Player.transform.position.z) * Time.fixedDeltaTime;
        //        Ally2.transform.position = new Vector3(Ally2.transform.position.x, Ally2.transform.position.y, Player.transform.position.z) * Time.fixedDeltaTime; 
        //    }

        //}
    }
    

    
    public void Ally()
    {
        if (GameManager.instance.score>= 200)
        {
            Debug.Log("Alliés appelé");
            GameManager.instance.score -= 200;
            Ally1.transform.position = new Vector3(Player.transform.position.x + 2.5f,0,Player.transform.position.z-1);
            Ally2.transform.position = new Vector3(Player.transform.position.x - 2.5f,0,Player.transform.position.z-1);
            FollowPlayer = true;

        }

    }

    public void DisableAlly()
    {
        FollowPlayer = false;
    }
    
    void Update()
    {
        if (GameManager.instance.caca)
        {
            Ally();
        }
        else
        {
            DisableAlly();
        }
 
        if (FollowPlayer)
        {
            //gestion du mouvement
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

            direction.Normalize();
            GetComponent<Rigidbody>().linearVelocity = direction * speed;
            //roulis
            GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 0, -horizontalInput * tilt);
        

            //gestion du tir
            if (Input.GetMouseButtonDown(0) && Time.time > LastShotTime + FireDelay)
            {
                Instantiate(LaserPrefab, LaserSpawn.transform.position, Quaternion.identity);
                GetComponent<AudioSource>().Play();
                LastShotTime = Time.time;
            }
        }
        
    }
}
