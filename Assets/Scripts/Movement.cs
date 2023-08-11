using UnityEngine;

//must have a rigidbody2d component
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    //settings
    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;
    public Vector2 initDirection;
    public LayerMask obstacleLayer; //obstacles to avoid

    // "new" keyword hides the inherited member
    public new Rigidbody2D rigidbody { get; private set; }
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; } //queue up the direction - move in this direction when possible
    //easier to control pacman with keyboard
    public Vector3 startPosition { get; private set; }
    

    private void Awake(){
        //reference some initial components
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.startPosition = this.transform.position;
    }

    private void Start(){
        init();
    }
    public void init(){
        this.speedMultiplier = 1.0f;
        this.direction = this.initDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startPosition;
        this.rigidbody.isKinematic = false;
        this.enabled = true;
    }

    private void Update(){
        //Time.deltaTime;
        //continuosly try to set the direction if some direction is queued up
        if(this.nextDirection != Vector2.zero){
            setDirection(this.nextDirection);
        }
    }

    // FixedUpdate is called once per physics frame
    private void FixedUpdate() {
        Vector2 curPos = this.rigidbody.position;
        // calculate movement
        Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
        // update position
        this.rigidbody.MovePosition(curPos + translation);
    }

    public void setDirection(Vector2 direction, bool forced = false){
        if(forced || !isBlocked(direction))
        {
            //if the direction is not blocked, set the direction
            //print("not blocked");
            this.direction = direction;
            this.nextDirection = Vector2.zero;
        }
        else 
        {
            //if the direction is blocked, queue up the direction
            //print("blocked");
            this.nextDirection = direction;
        }
    }

    public bool isBlocked(Vector2 direction)
    {
        //print("Occupied");
        // box cast instead of raycast
        //transform.position is the center of the box so we go half the size over in each direction
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, 
        Vector2.one * 0.75f, 0.0f, direction, 1.5f, this.obstacleLayer);
        // hit.collider is null if nothing was hit
        // hit.collider is not null if something was hit
        return hit.collider != null;
    }
}