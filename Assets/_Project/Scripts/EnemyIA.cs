using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{

    [SerializeField]
    private float _Speed = 2f;

    [SerializeField]
    private GameObject _EnemyExplosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down
        transform.Translate(Vector3.down * _Speed * Time.deltaTime);
        // when enemy is off screen  respawn back at the top with a random x postion
        if(transform.position.y < -7f)
        {
            float randomX = Random.Range(-7f, 7f);
            transform.position = new Vector3(randomX, 7, 0 );
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collider with:" + other.tag);

        if(other.tag == "Laser")
        {
            if( other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }

            Destroy(this.gameObject);
            Instantiate(_EnemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        else if( other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
            Instantiate(_EnemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }
    }
}
