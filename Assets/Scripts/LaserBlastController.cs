using UnityEngine;

public class LaserBlastController : MonoBehaviour
{
    public float speed = 16.0f;

    GameController gC => GameObject.Find("GameController").GetComponent<GameController>();
    GameObject player => GameObject.Find("Spaceship");

    void LateUpdate()
    {
        // make a local copy of the tranform
        Vector3 pos = transform.localPosition;

        // calculates the new position
        pos += transform.TransformDirection(Vector3.right) * Time.deltaTime * speed;

        // if laser is out of the screen, destroy it
        if (pos.x > gC.horizontal_limits + 1.0f || pos.x < -gC.horizontal_limits - 2.0f) {
            DestroyLaserBean();
        }

        // update position
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if player is in the scene
        if (player != null)
        {

            PlayerController pC = player.GetComponent<PlayerController>();

            // if asteroid was hit, get the score and destroy both the asteroid and the laser
            if (other.gameObject.CompareTag("Asteroid"))
            {
                AsteroidController aC = other.GetComponent<AsteroidController>();
                pC.score += aC.score;
                aC.DestroyAsteroid();
                DestroyLaserBean();
            }

            // if enemy was hit, subtract one life of it
            else if (other.gameObject.CompareTag("Enemy"))
            {
                EnemyController eC = other.GetComponent<EnemyController>();
                //Debug.Log("hit enemy");
                eC.life--;

                // if enemy is dead, get the score and destroy enemy
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
        Destroy(gameObject);
    }
}
