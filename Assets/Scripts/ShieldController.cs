using System.Collections;
using UnityEngine;

public class ShieldController : LaserBlastController
{
    IEnumerator shieldTime;

    private void Start()
    {
        shieldTime = ShieldTime();
        StartCoroutine(shieldTime);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Rotate(Vector3.right, 33.0f);
    }

    // if shield collides with something, destroy the other object
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            AsteroidController aC = other.GetComponent<AsteroidController>();
            aC.DestroyAsteroid();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController eC = other.GetComponent<EnemyController>();
            eC.DestroyEnemy();
        } 
        else if (other.gameObject.CompareTag("LaserEnemy"))
        {
            Destroy(other.gameObject);
        }
    }

    IEnumerator ShieldTime()
    {
        yield return new WaitForSeconds(15.0f);

        Destroy(this.gameObject);
    }
}
