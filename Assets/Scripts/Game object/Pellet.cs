using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int points = 10;

    //virtual means overrideable
    protected virtual void Eat(){
        //this.gameObject.SetActive(false);
        FindObjectOfType<GameManager>().pelletEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //make sure the object is pacman
        if(other.gameObject.layer == LayerMask.NameToLayer("Pacman")){
            Eat();
        }
    }
}