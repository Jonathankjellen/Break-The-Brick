using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Ball ball {get; private set;}
    public Paddle paddle {get; private set;}
    public Brick[] bricks {get; private set;}
    public List<Ball> balls {get; set;}
    public int level = 0;
    public int score = 0;
    public int lives = 3;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
        SceneManager.sceneLoaded += OnLevelLoaded;
    }
    private void Start()
    {
        if(PlayerPrefs.HasKey("LastLevel"))
        {
            this.level = PlayerPrefs.GetInt("LastLevel");
        } else {
            PlayerPrefs.SetInt("LastLevel", 0);
        }
        //SceneManager.LoadScene("StarterScene");
        //NewGame();
    }
    public void NewGame()
    {
        this.score = 0;
        this.lives = 3;
        
        LoadLevel(this.level + 1);
    }

    private void LoadLevel(int level)
    {
        //this.level = level;
        if(level > 10)
        {
            SceneManager.LoadScene("winScrenn");
        }
        else 
        {
            SceneManager.LoadScene("Level" + level);
        }
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void UnPause()
    {
        Time.timeScale = 1;
    }


    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindObjectOfType<Ball>();
        this.paddle = FindObjectOfType<Paddle>();
        this.bricks = FindObjectsOfType<Brick>();
        this.balls = new List<Ball>();
        this.balls.Add(this.ball);
        
    }
    public void Hit(Brick brick)
    {
        this.score += brick.points;
        if(Cleared())
        {
            PlayerPrefs.SetInt("LastLevel", this.level + 1);
            LoadLevel(this.level + 1);
        }
    }
    private void ResetLevel()
    {
        this.ball.ResetBall();
        this.paddle.ResetPaddle();
        
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
        //NewGame();
        //FindObjectOfType<LifeHandler>().activateAll();
    }
    public void Miss()
    {
        if(!BallsLeft()){
            //GetComponent<LifeHandler>().removeLife(this.lives);
            FindObjectOfType<LifeHandler>().removeLife(this.lives);
            this.lives--;
            if(this.lives > 0)
            {
                ResetLevel();
            } 
            else 
            {
                GameOver();
            }
        }
        
    }
    private bool Cleared()
    {
        for(int i=0; i < this.bricks.Length; i++)
        {
            if(this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable)
            {
                return false;
            } 
        }
        return true;
    }
    private bool BallsLeft()
    {
        
        for(int i=0; i < this.balls.Count; i++)
        {
            if(this.balls[i].gameObject.activeInHierarchy)
            {
                return true;
            } 
        }
        return false;
    }
}
