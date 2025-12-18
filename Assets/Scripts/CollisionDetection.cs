using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] GameObject asteroidExplosionVFX;
    [SerializeField] GameObject playerExplosionVFX;

    // Pas de variable "Allynb" ici ! Tout est géré par le GameManager.

    private void OnCollisionEnter(Collision collision)
    {
        // 1. Gestion du JOUEUR (Game Over)
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("GAME OVER");
            Instantiate(playerExplosionVFX, gameObject.transform.position, Quaternion.identity);
            GameManager.instance.ShowGameOverUi();
            Destroy(collision.gameObject);
        }
        // 2. Gestion du LASER (Points)
        else if (collision.gameObject.tag == "Laser")
        {
            GameManager.instance.IncrementScore();
            Debug.Log("+ 50 Points");
            Instantiate(asteroidExplosionVFX, gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
        // 3. Gestion des ALLIÉS (Le point critique)
        else if (collision.gameObject.tag == "Ally")
        {
            Instantiate(playerExplosionVFX, gameObject.transform.position, Quaternion.identity);

            // 1. Récupérer le script PowerUp et le désactiver
            PowerUp pUp = collision.gameObject.GetComponent<PowerUp>();
            if(pUp != null)
            {
                pUp.OnAllyDeath(); // On lui dit "Tu es mort, arrête de bouger"
            }

            // 2. Physique (Stop mouvement)
            Rigidbody allyRb = collision.gameObject.GetComponent<Rigidbody>();
            if (allyRb != null)
            {
                allyRb.linearVelocity = Vector3.zero; 
                allyRb.angularVelocity = Vector3.zero; 
            }

            // 3. Téléportation au garage
            if (collision.gameObject.name == "Ally1")
            {
                collision.gameObject.transform.position = new Vector3(1.5f, 0, -10.3f);
                collision.gameObject.transform.rotation = Quaternion.identity;
            }
            else if (collision.gameObject.name == "Ally2")
            {
                collision.gameObject.transform.position = new Vector3(-1.5f, 0, -10);
                collision.gameObject.transform.rotation = Quaternion.identity;
            }

            // 4. Prévenir le GameManager
            GameManager.instance.AllyHit();
        }
        
        // L'astéroïde se détruit à la fin, quoi qu'il arrive
        Destroy(gameObject);
    }
}

