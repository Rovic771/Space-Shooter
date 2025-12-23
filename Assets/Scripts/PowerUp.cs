using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] GameObject Player; 
    public bool IsActive = false;
    private bool hasDied = false; 
    
    [Header("Positionnement")]
    [SerializeField] float DecallageX = 2.5f;
    [SerializeField] float DecallageZ = -1f; 
    [SerializeField] float tilt = 45f;
    [SerializeField] GameObject LaserPrefab;
    [SerializeField] GameObject LaserSpawn;
    [SerializeField] float FireDelay = 0.2f;
    
    float LastShotTime;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ActivateAlly()
    {
        if (IsActive) return;

        Debug.Log("Allié appelé");
        
        UpdatePosition();
        IsActive = true;
        hasDied = false;
    }

    public void DisableAlly()
    {
        IsActive = false;
    }
    
    public void OnAllyDeath()
    {
        IsActive = false;
        hasDied = true;
    }
    
    void Update()
    {
        if (GameManager.instance.valide && !IsActive && !hasDied)
        {
             ActivateAlly();
        }
        else if (!GameManager.instance.valide)
        {
            DisableAlly();
            hasDied = false; 
        }
        
        if (IsActive)
        {
            HandleShooting();
        }
    }

    void LateUpdate()
    {
        if (IsActive && Player != null)
        {
            UpdatePosition();
            HandleRotation();
        }
    }

    void UpdatePosition()
    {
        Vector3 targetPosition = Player.transform.position;
        targetPosition.x += DecallageX;
        targetPosition.z += DecallageZ;
        transform.position = targetPosition;
    }

    void HandleRotation()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.rotation = Quaternion.Euler(0, 0, -horizontalInput * tilt);
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > LastShotTime + FireDelay)
        {
            Instantiate(LaserPrefab, LaserSpawn.transform.position, Quaternion.identity);
            if(TryGetComponent<AudioSource>(out AudioSource audio))
            {
                audio.Play();
            }
            LastShotTime = Time.time;
        }
    }
}
