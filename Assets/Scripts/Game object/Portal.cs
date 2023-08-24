using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform connector;
    private void OnTriggerEnter2D(Collider2D other) {

        //reposition the object to the connection (kinda like a portal)
        Vector3 pos = other.transform.position;
        pos.x = connector.position.x;
        pos.y = connector.position.y;
        other.transform.position = pos;
    }
}
