using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    [SerializeField] private Transform bag;
    [SerializeField] private Transform Eye;
    // Start is called before the first frame update
    private void Start() {
        Vector3 pos = bag.position;
        bag.position = new Vector3(Eye.position.x, pos.y, Eye.position.z + 0.5f);
    }
}
