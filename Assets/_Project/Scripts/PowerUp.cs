using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 3.0f;
    [SerializeField]
    private int powerupID; // 0 = TripleShoot; 1 = SpeedUp; 2 = shields; 

   

    // Update is called once per frame
    void Update()
    {
        // move up at 10 speed
        transform.Translate(Vector3.down * _Speed * Time.deltaTime);
        // if my position on y is greater than or equal to 6 = destroy the laser

        if (transform.position.y <= -6)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collider with: " + other.name);

        if(other.tag == "Player")
        {
            //access the player to change the settings
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                if(powerupID == 0)
                {
                    //enable the PowerUp if  powerupID == 0
                    player.TripleShootOn();
                }else if(powerupID == 1)
                {
                    // enable speed boost if  powerupID == 1
                    player.SpeedBoostOn();
                }
                else if (powerupID == 2)
                {
                    // enable shields if  powerupID == 2
                    player.EnableShields();
                }

            }
            // if the PowerUp collide with player then  destroy the object(PowerUp)
            Destroy(this.gameObject);
        }
        
    }
}
