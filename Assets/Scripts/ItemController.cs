using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    Transform gO;

    public PlayerController.ITEMS itemType;

    private IEnumerator autodestroy;

    // Start is called before the first frame update
    void Start()
    {
        gO = GetComponentInChildren<Transform>();
        //itemType = Random.Range(0, 2);
        autodestroy = Autodestroy();
        StartCoroutine(autodestroy);
    }

    // Update is called once per frame
    void Update()
    {
        gO.transform.Rotate(Vector3.forward, 2.0f);
    }

    IEnumerator Autodestroy()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }

}
