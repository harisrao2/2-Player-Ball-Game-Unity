using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFinal : MonoBehaviour { 

    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        transform.position = player.transform.position + offset;
    }
    // Update is called once per frame , Guranted to run after all items have been processed
    
}
