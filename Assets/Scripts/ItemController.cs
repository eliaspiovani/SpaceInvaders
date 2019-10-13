using System.Collections;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    Transform gO;

    public PlayerController.ITEMS itemType;

    private IEnumerator autodestroy;

    void Start()
    {
        gO = GetComponentInChildren<Transform>();

        // Coroutine to autodestroy item if not picked up
        autodestroy = Autodestroy();
        StartCoroutine(autodestroy);
    }

    void Update()
    {
        // rotates item
        gO.transform.Rotate(Vector3.forward, 2.0f);
    }

    IEnumerator Autodestroy()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }

}
