using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject transition;
    public static GameController instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    
    public void ShowGameOver()
    {
        transition.SetActive(true);
        transition.GetComponent<Animator>().Play("FadeIn");
        //gameOver.SetActive(true);
    }

    // it's used to start and restart the game 
    public void RestartGame(string lvlname)
    {
        SceneManager.LoadScene(lvlname);
    }
}
