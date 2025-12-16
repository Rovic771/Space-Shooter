using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] GameObject asteroidExplosionVFX;
    [SerializeField] GameObject playerExplosionVFX;
    [SerializeField] 
    GameManager gameManagerRef;

    void Start()
    {
        gameManagerRef = GameObject.FindFirstObjectByType<GameManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("GAME OVER");
            Instantiate(playerExplosionVFX, gameObject.transform.position, Quaternion.identity);
            GameManager.instance.ShowGameOverUi();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Laser")
        {
            GameManager.instance.IncrementScore();
            Debug.Log("+ 50 Points");
            Instantiate(asteroidExplosionVFX, gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Ally")
        {
            Instantiate(playerExplosionVFX, gameObject.transform.position, Quaternion.identity);
            if (collision.gameObject.name == "Ally1")
            {
                collision.gameObject.transform.position = new Vector3(1.5f, 0, -10.3f);
                collision.gameObject.transform.localPosition = new Vector3(1.5f, 0,collision.gameObject.transform.position.z);
                collision.gameObject.transform.rotation = Quaternion.identity;
                
            }
            else if (collision.gameObject.name == "Ally2")
            {
                collision.gameObject.transform.position = new Vector3(-1.5f, 0, -10.3f);
                collision.gameObject.transform.localPosition = new Vector3(-1.5f, 0,collision.gameObject.transform.position.z);
                collision.gameObject.transform.rotation = Quaternion.identity;
                GameManager.instance.caca = false;
            }
        }
        
        Destroy(gameObject);

    }
}
