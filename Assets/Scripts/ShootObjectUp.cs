

using UnityEngine;
public class ShootObjectUp : MonoBehaviour
{

private Vector3 newPosition = new Vector3(0, 1.0f, 0.25f);
// private Vector3 SecondaryPosition = new Vector3(0, 1.2f, 0.25f);
private Vector3 startPosition = new Vector3(0, 0.5f, 0.25f);

 void Start()
 {
  
 }

 private void Update()
 {
    // Moves the object to target position
    if(this.gameObject.transform.position.y < 1.0f)
    {
      transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * 2.5f);
    }
  }
}
