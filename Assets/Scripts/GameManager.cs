using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public int score = 0;
    [SerializeField] TextMeshProUGUI scoretext;
    [SerializeField] GameObject gameOverUiObject;
    
    public bool caca = false;     // Variable qui bloque le bouton
    public int alliesVivants = 0; // Compteur officiel

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }

    // ... tes autres fonctions (Restart, GoNext, etc.) ...

    public void IncrementScore()
    {
        score += 25;
        scoretext.text = "Score :" + score.ToString();
    }
    
    public void ShowGameOverUi()
    {
        gameOverUiObject.SetActive(true);
    }

    // APPELÉE PAR LE BOUTON
    public void ActiveAlly()
    {
        // On vérifie ici si on a assez de points pour appeler l'escadron
        if (score >= 200) 
        {
            score -= 200; // On paye une seule fois
            scoretext.text = "Score :" + score.ToString(); // On met à jour l'affichage
            
            caca = true;       // On active les alliés
            alliesVivants = 2; // On initialise le compteur
        }
        else
        {
            Debug.Log("Pas assez de points !");
        }
    }

    // APPELÉE PAR COLLISION DETECTION
    public void AllyHit()
    {
        alliesVivants--; // On en perd un

        if (alliesVivants <= 0)
        {
            caca = false; // Tout le monde est mort, on débloque le bouton
            alliesVivants = 0; // Sécurité
        }
    }
}
