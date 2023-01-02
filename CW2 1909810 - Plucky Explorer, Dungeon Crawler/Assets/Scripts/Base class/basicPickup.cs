using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicPickup : MonoBehaviour
{
    public bool isHealthPickup;
    public bool isAttackPickup;
    public bool isSpeedPickup;
    public basicPlayer playerCharacter;
    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GetComponent<basicPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
