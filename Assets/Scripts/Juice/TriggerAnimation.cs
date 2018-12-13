using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{

    [SerializeField] private string animTriggerName;
    private Animator objAnim;

    private void Start()
    {
        objAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name);

            objAnim.SetTrigger(animTriggerName);

            Debug.Log(objAnim.name);
        }
    }

}
