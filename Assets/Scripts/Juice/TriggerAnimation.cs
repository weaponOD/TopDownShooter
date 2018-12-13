using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{

    [SerializeField] private string animTriggerName;
    [SerializeField] private string triggererTag;
    private Animator objAnim;

    private void Start()
    {
        objAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag(triggererTag))
        //{
            objAnim.SetTrigger(animTriggerName);
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag(triggererTag))
        //{
            objAnim.SetTrigger(animTriggerName);
        //}
    }
}
