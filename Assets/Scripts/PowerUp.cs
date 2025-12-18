using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] GameObject Player; 
    public bool IsActive = false;
    private bool hasDied = false; // Pour empêcher le respawn immédiat
    
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
        // ON NE TOUCHE PLUS AU SCORE ICI !
        
        UpdatePosition();
        IsActive = true;
        hasDied = false;
    }

    public void DisableAlly()
    {
        IsActive = false;
    }
    
    // Nouvelle fonction appelée quand il meurt
    public void OnAllyDeath()
    {
        IsActive = false;
        hasDied = true; // On le marque comme mort pour ce tour
    }
    
    void Update()
    {
        // 1. GESTION DE L'ACTIVATION
        // On vérifie qu'il n'est pas mort durant ce tour (!hasDied)
        if (GameManager.instance.caca && !IsActive && !hasDied)
        {
             ActivateAlly();
        }
        else if (!GameManager.instance.caca)
        {
            // Quand le bouton est relâché (tout le monde mort), on reset l'état
            DisableAlly();
            hasDied = false; 
        }
 
        // 2. GESTION DU TIR
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
