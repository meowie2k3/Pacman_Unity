
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;

    public Transform pellets;

    public int score { get; private set; }

    public int lives { get; private set; }

    //first ghost is 1, second is 2, etc.
    public int ghostMultiplier { get; private set; } = 1;

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

        //reset ghost multiplier
        resetGhostMultiplier();
        
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
        int points = ghost.points * this.ghostMultiplier;
        setScore(this.score + points);
        this.ghostMultiplier++;
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

    public void pelletEaten(Pellet pellet)
    {
        setScore(this.score + pellet.points);
        pellet.gameObject.SetActive(false);

        if(!hasRemainingPellet())
        {
            //turn off pacman
            this.pacman.gameObject.SetActive(false);

            // start new round after 3 seconds
            Invoke(nameof(newRound), 3.0f);
        }
    }

    public void powerPelletEaten(PowerPellet powerPellet)
    {
        //TODO: change ghost state to blue
        pelletEaten(powerPellet);
        CancelInvoke(nameof(resetGhostMultiplier)); //incase there is a power pellet already active
        Invoke(nameof(resetGhostMultiplier), powerPellet.duration);
    }

    public bool hasRemainingPellet()
    {
        foreach (Transform pellet in this.pellets)
        {
            //check if game object is active
            if(pellet.gameObject.activeSelf){
                return true;
            }
        }
        return false;
    }

    public void resetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }
}
