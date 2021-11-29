using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move up at 10 speed
        transform.Translate(Vector3.up * _Speed * Time.deltaTime);
        // if my position on y is greater than or equal to 6 = destroy the laser

        if( transform.position.y >= 6)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }

    }
}
