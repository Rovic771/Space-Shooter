using UnityEngine;
using UnityEngine.SceneManagement;//a rajouter
using TMPro; // pour l'UI

public class GameManager : MonoBehaviour
{

    static public GameManager instance;
    public int score = 0;
    [SerializeField] TextMeshProUGUI scoretext;
    [SerializeField] GameObject gameOverUiObject;
    public bool caca = false;

    private void Awake()
    {
       if(instance == null)
        {
            instance = this;
        }
       else
        {
            Destroy(gameObject);
        }
    }
    

    public void GoNextLevel()
    {
        int n = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene((n+1)% SceneManager.sceneCountInBuildSettings);
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void IncrementScore()
    {
        score += 25;
        scoretext.text = "Score :" + score.ToString();
    }
    public void ShowGameOverUi()
    {
        gameOverUiObject.SetActive(true);
    }

    public void ActiveAlly()
    {
        caca = true;
    }
}
