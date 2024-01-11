using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    BoxCollider boxCollider;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Hit");
        }
    }
}
