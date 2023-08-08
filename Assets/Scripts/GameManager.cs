
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;

    public Transform pellets;

    public int score { get; private set; }

    public int lives { get; private set; }

    private void Start()
    {
        NewGame();
    }

    private void Update(){
        if(this.lives<=0 && Input.GetKeyDown(KeyCode.Return)){
            NewGame();
        }
    }

    private void NewGame()
    {
        setScore(0);
        setLives(3);
        newRound();
    }

    private void newRound(){
        // turn on all pellets
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        resetState();
    }

    private void GameOver(){
        // turn on all ghosts
        for(int i=0; i<ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        // turn on pacman
        this.pacman.gameObject.SetActive(false);
    }
    
    private void resetState()
    {
        
        // turn on all ghosts
        for(int i=0; i<ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(true);
        }

        // turn on pacman
        this.pacman.gameObject.SetActive(true);
    }

    private void setScore(int score)
    {
        this.score = score;
    }

    private void setLives(int lives)
    {
        this.lives = lives;
    }

    public void GhostEaten(Ghost ghost)
    {
        setScore(this.score + ghost.points);
    }

    public void PacmanEaten()
    {
        //turn off pacman
        this.pacman.gameObject.SetActive(false);

        if(lives > 1)
        {
            setLives(this.lives - 1);
            
            //after 3 seconds, reset the state
            Invoke(nameof(resetState), 3.0f);
            
        }
        else
        {
            GameOver();
        }
    }
}
