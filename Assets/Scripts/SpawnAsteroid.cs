using System.Security.Cryptography;
using UnityEngine;

public class SpawnAsteroid : MonoBehaviour
{
    [SerializeField] GameObject asteroid1;
    [SerializeField] GameObject asteroid2;
    [SerializeField] float SpawnDelay = 2f;
    float LastSpawn;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float r = Random.Range(0f,1f);
        if(Time.time>SpawnDelay + LastSpawn && r < 0.5f)
        {
            Instantiate(asteroid1, new Vector3(Random.Range(-4f,4.5f), 0, 10), Quaternion.identity);
            LastSpawn = Time.time;
        }
        if(Time.time > SpawnDelay + LastSpawn && r > 0.5f)
        {
            Instantiate(asteroid2, new Vector3(Random.Range(-4f, 4.5f), 0, 10), Quaternion.identity);
            LastSpawn = Time.time;
        }
        if(Time.time > SpawnDelay + LastSpawn && SpawnDelay > 0.1f)
            {
            SpawnDelay -= 0.01f;
        }
        Debug.Log("Spawn D�lai: " + SpawnDelay);
    }
}
