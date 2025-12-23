using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public int score = 0;
    [SerializeField] TextMeshProUGUI scoretext;
    [SerializeField] GameObject gameOverUiObject;
    
    public bool valide = false;     
    public int alliesVivants = 0;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void GoNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    

    public void IncrementScore()
    {
        score += 25;
        scoretext.text = score.ToString();
    }
    
    public void ShowGameOverUi()
    {
        gameOverUiObject.SetActive(true);
    }
    
    public void ActiveAlly()
    {
        if (score >= 200) 
        {
            score -= 200;
            scoretext.text =  score.ToString(); 
            
            valide = true;      
            alliesVivants = 2;
        }
        else
        {
            Debug.Log("Pas assez de points !");
        }
    }
    
    public void AllyHit()
    {
        alliesVivants--; 

        if (alliesVivants <= 0)
        {
            valide = false; 
            alliesVivants = 0;
        }
    }
    
    public void restartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
