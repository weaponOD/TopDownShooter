using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject heartUIPrefab;
    [SerializeField] HealthSettings playerHealth;

    private int numHearts;
    List<HeartUI> hearts;

    void Start()
    {
        PopulateHearts();
    }

    private void PopulateHearts()
    {
        hearts = new List<HeartUI>();
        numHearts = playerHealth.maxHealth;

        for (int i = 0; i < numHearts; i++)
        {
            HeartUI go = Instantiate(heartUIPrefab, this.transform).GetComponent<HeartUI>();

            if (go)
                hearts.Add(go);
        }
        UpdateHearts();
    }

    private void UpdateHearts()
    {
        if (numHearts > playerHealth.maxHealth) numHearts = playerHealth.maxHealth; 

        for (int i = 0; i < hearts.Count; i++)
        {
            if(i < playerHealth.maxHealth)
            {
                hearts[i].heartFill.SetActive(true);
            }
            else
            {
                hearts[i].heartFill.SetActive(false);
            }
        }

    }

    //private void Init(){
    // pop in anim of each heart, then inside fill up and small particle pop 
    //
    //
    //}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //remove health --;
            UpdateHearts();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            //add health ++;
            UpdateHearts();
        }


    }
}
