using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HoopCollision : MonoBehaviour
{

    [SerializeField] private Hoop _hoop;
    private HoopMove _move;

    private void Start()
    {
        _move = _hoop.GetComponent<HoopMove>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        
            if (other.CompareTag("Edge"))
            {

                _move.turnAround();
            } 
        
        
        
    }
}
