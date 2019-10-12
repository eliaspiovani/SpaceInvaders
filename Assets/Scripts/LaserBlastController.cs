using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlastController : MonoBehaviour
{
    public float speed = 16.0f;

    GameController gC => GameObject.Find("GameController").GetComponent<GameController>();
    GameObject player => GameObject.Find("Spaceship");

    //public GameObject asteroidExplosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = transform.localPosition;

        //pos += new Vector3(Time.deltaTime * speed, 0.0f, 0.0f);
        pos += transform.TransformDirection(Vector3.right) * Time.deltaTime * speed;

        if (pos.x > gC.horizontal_limits + 1.0f || pos.x < -gC.horizontal_limits - 2.0f) {
            DestroyLaserBean();
        }

        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (player != null)
        {
            PlayerController pC = player.GetComponent<PlayerController>();
            if (other.gameObject.CompareTag("Asteroid"))
            {
                //Debug.Log("hit asteroid");
                AsteroidController aC = other.GetComponent<AsteroidController>();
                pC.score += aC.score;
                aC.DestroyAsteroid();
                DestroyLaserBean();
            }
            else if (other.gameObject.CompareTag("Enemy"))
            {
                EnemyController eC = other.GetComponent<EnemyController>();
                //Debug.Log("hit enemy");
                eC.life--;

                if (eC.life == 0)
                {
                    //increase points
                    pC.score += eC.score;
                    eC.DestroyEnemy();
                }
                DestroyLaserBean();
            }
        }


    }

    private void DestroyLaserBean()
    {
        //if (sC.numberOfShoots > 0) sC.numberOfShoots--;
        Destroy(gameObject);
    }
}
