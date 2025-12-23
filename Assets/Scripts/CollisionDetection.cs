using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] GameObject asteroidExplosionVFX;
    [SerializeField] GameObject playerExplosionVFX;
    

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
            
            PowerUp pUp = collision.gameObject.GetComponent<PowerUp>();
            if(pUp != null)
            {
                pUp.OnAllyDeath();
            }
            
            Rigidbody allyRb = collision.gameObject.GetComponent<Rigidbody>();
            if (allyRb != null)
            {
                allyRb.linearVelocity = Vector3.zero; 
                allyRb.angularVelocity = Vector3.zero; 
            }
            
            if (collision.gameObject.name == "Ally1")
            {
                collision.gameObject.transform.position = new Vector3(1.5f, 0, -100);
                collision.gameObject.transform.rotation = Quaternion.identity;
            }
            else if (collision.gameObject.name == "Ally2")
            {
                collision.gameObject.transform.position = new Vector3(-1.5f, 0, -100);
                collision.gameObject.transform.rotation = Quaternion.identity;
            }
            
            GameManager.instance.AllyHit();
        }
        
        Destroy(gameObject);
    }
}

