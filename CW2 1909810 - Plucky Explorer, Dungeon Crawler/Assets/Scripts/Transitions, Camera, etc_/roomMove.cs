using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomMove : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;
    private camFollow cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<camFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player")) {
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
        }
    }

    public static implicit operator Collider2D(roomMove v)
    {
        throw new NotImplementedException();
    }
}
