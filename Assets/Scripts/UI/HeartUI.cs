using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUI : MonoBehaviour
{
    [SerializeField] public GameObject heartFill;

    [Header("Spawn Animation")]
    [SerializeField] private float popInTime;
    [SerializeField] private AnimationCurve popInCurve;
    [SerializeField] private float centerFillTime;
    [SerializeField] private AnimationCurve centerFillCurve;

    [Header("Particles")]
    [SerializeField] private GameObject popParticle;

    private Transform myTransform;

    public void Init()
    {
        myTransform = transform;
        heartFill.transform.localScale = Vector3.zero;
        myTransform.localScale = Vector3.zero;
    }

    public void StartAnimation(HealthUI healthUI)
    {
        StartCoroutine(SpawnIn(healthUI));
    }

    private IEnumerator SpawnIn(HealthUI healthUI)
    {
        float t = 0f;

        Vector3 startScale = myTransform.localScale;

        while(t < popInTime)
        {
            t += Time.deltaTime;
            myTransform.localScale = Vector3.LerpUnclamped(startScale, Vector3.one, popInCurve.Evaluate(t / popInTime));

            yield return null;
        }

        healthUI.SpawnInFinish();
        t = 0f;

        while (t < centerFillTime)
        {
            t += Time.deltaTime;
            heartFill.transform.localScale = Vector3.LerpUnclamped(startScale, Vector3.one, centerFillCurve.Evaluate(t / centerFillTime));

            yield return null;
        }

        if (popParticle)
            SimplePool.Spawn(popParticle, transform.position, Quaternion.identity);

        
    }

    //private void Init(){
    // pop in anim of each heart, then inside fill up and small particle pop 
    //
    //
    //}
}
