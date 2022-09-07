using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(deactiveObject());
        }
    }

    IEnumerator deactiveObject()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
