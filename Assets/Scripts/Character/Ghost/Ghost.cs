
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement movement { get; private set; } //set direction etc

    //behavior list:
    public Chase chase { get; private set; }
    public Scatter scatter { get; private set; }
    public Frightened frightened { get; private set; }
    public Home home { get; private set; }

    public Behavior initBehavior; //behavior to start with

    public Transform target; //target to chase

    //gameplay variables
    public int points = 200;

    private void Awake() {
        this.movement = GetComponent<Movement>();
        this.chase = GetComponent<Chase>();
        this.scatter = GetComponent<Scatter>();
        this.frightened = GetComponent<Frightened>();
        this.home = GetComponent<Home>();
    }

    private void Start(){
        reset();
    }

    public void reset(){
        this.gameObject.SetActive(true);
        this.movement.init();

    }
}
