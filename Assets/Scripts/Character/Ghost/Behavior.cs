using UnityEngine;


[RequireComponent(typeof(Ghost))]
//abstract classes so that we cant add behaviors to the ghost directly
public abstract class Behavior : MonoBehaviour
{
    public Ghost ghost {get; private set;}
    public float duration;

    //awake is called when the object is created
    private void Awake() {
        this.ghost = GetComponent<Ghost>();
        this.enabled = false;
    }

    public void Enable() {
        Enable(this.duration);
    }
    public virtual void Enable(float duration){
        this.enabled = true;
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }
    public virtual void Disable() {
        this.enabled = false;
        //make sure other behaviors pending disable are cancelled
        CancelInvoke();
    }
}
