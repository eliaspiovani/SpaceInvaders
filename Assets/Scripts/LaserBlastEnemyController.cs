using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlastEnemyController : MonoBehaviour
{
    public float speed = 16.0f;

    GameController gC => GameObject.Find("GameController").GetComponent<GameController>();
    //SpaceController sC => GameObject.Find("Spaceship").GetComponent<SpaceController>();

    //public GameObject explosion;

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

        if (pos.x > gC.horizontal_limits+2.0f || pos.x < -gC.horizontal_limits - 2.0f || pos.z < -gC.vertical_limits - 2.0f || pos.z > gC.vertical_limits + 2.0) {
            DestroyLaserBean();
        }

        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("hit asteroid");
            sC.score += other.GetComponent<AsteroidController>().score;
            Instantiate(explosion, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            DestroyLaserBean();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit enemy");
            other.GetComponent<EnemyController>().life--;

            if (other.GetComponent<EnemyController>().life == 0)
            {
                //increase points
                sC.score += other.GetComponent<EnemyController>().score;
                Destroy(other.gameObject);
            }
        }*/

    }

    private void DestroyLaserBean()
    {
        //sC.numberOfShoots--;
        Destroy(gameObject);
    }
}
