using UnityEngine;
[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    public Movement movement { get; private set; }

    private void Awake(){
        //reference movement component in pacman object
        this.movement = GetComponent<Movement>();
    }
    private void Update(){
        //if(Input.anyKeyDown) print("key pressed");

        if(Input.GetKeyDown(KeyCode.W) 
        || Input.GetKeyDown(KeyCode.UpArrow))
        {
            //print("up");
            this.movement.setDirection(Vector2.up);
        }
        else if(Input.GetKeyDown(KeyCode.A) 
            || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //print("left");
            this.movement.setDirection(Vector2.left);
        }
        else if(Input.GetKeyDown(KeyCode.S) 
            || Input.GetKeyDown(KeyCode.DownArrow))
        {
            //print("down");
            this.movement.setDirection(Vector2.down);
        }
        else if(Input.GetKeyDown(KeyCode.D) 
            || Input.GetKeyDown(KeyCode.RightArrow))
        {
            //print("right");
            this.movement.setDirection(Vector2.right);
        }
    
    }
}
