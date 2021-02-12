using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
	private float magnetude = 5f;
	private float offset = 0;
	private Vector3 startPosition;
	private float frequency = 5f;
	
    void Start()
    {
        startPosition = transform.position;
    }

   
    void Update()
    {
        transform.position = startPosition + transform.up * Mathf.Sin(Time.time * frequency + offset) * magnetude;
    }

    
}
