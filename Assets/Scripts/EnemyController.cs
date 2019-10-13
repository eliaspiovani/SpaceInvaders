using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameController gC => GameObject.Find("GameController").GetComponent<GameController>();
    GameObject player => GameObject.FindWithTag("Player");

    float acc_vertical, acc_horizontal;
    public float speed = 10.0f, horizontal_limits = 14.0f, vertical_limits = 10.0f;
    public float tiltAngle = 20.0f, smooth = 5.0f;

    public int maxShoots = 3;
    public int numberOfShoots = 0;

    public GameObject laser;
    GameObject explosion;
    public GameObject[] laserPoints;

    public bool followPlayer = false;

    public int score = 200;

    public int life = 3;

    BoxCollider bxCol => GetComponent<BoxCollider>();

    bool shootAgain = true;

    void Start()
    {
        // get the explosion for the enemies in the game controller script
        explosion = gC.enemyExplosion;
    }

    void Update()
    {
        // Accelerate the object according to the speed
        acc_horizontal = -Time.deltaTime * speed;

        if (numberOfShoots < (maxShoots * laserPoints.Length) && shootAgain)
        {
            Shoot();
            shootAgain = false;
            // delay between shoots
            StartCoroutine(ShootDelay());
        }

    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(0.5f);
        shootAgain = true;
    }

    private void LateUpdate()
    {
        // make a local copy of the tranform
        Vector3 pos = transform.position;

        // calculates the new position
        pos += transform.TransformDirection(-Vector3.right) * acc_horizontal;

        // if position is out of the bounds of the screen, destroy the object
        if (pos.x < -gC.horizontal_limits - 2.0f || pos.z < -gC.vertical_limits - 2.0f || pos.z > gC.vertical_limits + 2.0f)
        {
            Destroy(gameObject);
        }

        // if it is a enemey that follows the player, player exists in the scene and position is greater than the position of the player, rotates it towards the player
        if (player != null && followPlayer && pos.x > (player.transform.position.x + 4.0f))
        {
            Vector3 playerDir = (player.transform.position - pos).normalized;
            float angle = Vector3.SignedAngle(-Vector3.right, playerDir, Vector3.up);
            transform.rotation = Quaternion.Euler(0.0f, 180.0f + angle, 0.0f);
        }

        // update the position of the object
        transform.position = pos;

    }

    private void Shoot()
    {
        // for all laser canons in the spaceshop, create a laser bean
        foreach (var laserPoint in laserPoints)
        {
            Instantiate(laser, laserPoint.transform.position, laserPoint.transform.rotation);
            numberOfShoots++;
        }

    }

    public void DestroyEnemy()
    {
        // if the enemy dies, create an explosion
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
