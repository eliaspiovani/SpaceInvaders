using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum ITEMS
    {
        _HEALTH, _1LASER, _2LASERS, _3LASERS, _SHIELD
    }

    float acc_vertical, acc_horizontal;
    public float speed = 10.0f;
    public float tiltAngle = 20.0f, smooth = 5.0f;


    [HideInInspector]
    public ITEMS laserType = ITEMS._1LASER;

    public ParticleSystem[] pSs;

    [HideInInspector]
    public int score = 0, maxShoots = 100;

    public GameObject laser, laserPoint01, laserPoint02, laserPoint03, shield, playerExplosion;
    public GameController gC;

    [HideInInspector]
    public float health = 1.0f;

    bool dead = false;
    
    void Update()
    {
        // Accelerate the object according to the pressed key
        acc_vertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        acc_horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        // Rotates the object if moving horizontally
        float tiltX = Input.GetAxis("Vertical") * tiltAngle;
        Quaternion target = Quaternion.Euler(tiltX, 0, 0);

        // Slerp the rotation to the final value, if not moving horizontally rotation goes back to zero
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        if (Input.GetKeyDown("space") && !dead)
        {
            Shoot();
            // if laser is not the single one, subtract the number of ammo, if the number is zero, return to the single shot laser
            if (laserType != ITEMS._1LASER)
            {
                maxShoots--;
                if (maxShoots <= 0)
                {
                    laserType = ITEMS._1LASER;
                }
            }
        }

        // if the player is going backwards, stop engines fire
        if (acc_horizontal < 0)
        {
            foreach (var pS in pSs)
            {
                pS.Stop();
            }
        }else if (pSs[0].isStopped)
        {
            foreach (var pS in pSs)
            {
                pS.Play();
            }
        }

    }

    private void LateUpdate()
    {

        // make a local copy of the tranform
        Vector3 pos = transform.position;

        // calculates the new position
        pos += new Vector3(acc_horizontal, 0.0f,  acc_vertical);

        // avoid player going out of screen
        pos.z = Mathf.Clamp(pos.z, -gC.vertical_limits, gC.vertical_limits);
        pos.x = Mathf.Clamp(pos.x, -gC.horizontal_limits, gC.horizontal_limits);

        // update position
        transform.position = pos;

    }

    private void Shoot()
    {
        // shoot lasers according to the type active 
        switch (laserType)
        {
            case ITEMS._1LASER:
                Instantiate(laser, laserPoint01.transform.position, Quaternion.identity);
                break;
            case ITEMS._2LASERS:
                Instantiate(laser, laserPoint02.transform.position, Quaternion.identity);
                Instantiate(laser, laserPoint03.transform.position, Quaternion.identity);
                break;
            case ITEMS._3LASERS:
                Instantiate(laser, laserPoint01.transform.position, Quaternion.identity);
                Instantiate(laser, laserPoint02.transform.position, Quaternion.identity);
                Instantiate(laser, laserPoint03.transform.position, Quaternion.identity);
                break;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // if player hits not a item
        if (!other.CompareTag("Item"))
        {
            if (other.CompareTag("Asteroid"))
            {
                AsteroidController aC = other.GetComponent<AsteroidController>();
                aC.DestroyAsteroid();
            } else if (other.CompareTag("Enemy"))
            {
                EnemyController eC = other.GetComponent<EnemyController>();
                eC.DestroyEnemy();
            } else if (other.CompareTag("LaserEnemy"))
            {
                Destroy(other.gameObject);
            }

            // subtract health, if it is zero, creates an explosion and deactive player from scene
            health -= 0.15f;
            if (health <= 0.0f)
            {
                health = 0.0f;
                dead = true;
                Instantiate(playerExplosion, transform.position, Quaternion.identity);
                this.gameObject.SetActive(false);
            }
        }
        // if it hits an item, verifies what item it is and update status according to the item
        else
        {
            ItemController iT = other.gameObject.GetComponent<ItemController>();
            switch (iT.itemType)
            {
                case ITEMS._HEALTH:
                    health = 1.0f;
                    break;
                case ITEMS._1LASER:
                    laserType = ITEMS._1LASER;
                    break;
                case ITEMS._2LASERS:
                    laserType = ITEMS._2LASERS;
                    maxShoots = 50;
                    break;
                case ITEMS._3LASERS:
                    laserType = ITEMS._3LASERS;
                    maxShoots = 25;
                    break;
                case ITEMS._SHIELD:
                    GameObject shieldGO = GameObject.FindWithTag("Shield");
                    // if shield is already being using, deletes it and creates a new one with the timer reseted
                    if (shieldGO != null) Destroy(shieldGO);
                    Instantiate(shield, transform);
                    break;
            }

            Destroy(other.gameObject);
        }

    }

    public void ResetPlayer()
    {
        gameObject.SetActive(true);
        gameObject.transform.position = new Vector3(-12.55f, 0.0f, 0.0f);
        health = 1.0f;
        dead = false;
        score = 0;
    }
}
