using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{

    [SerializeField] private GameObject heartUIPrefab;
    [SerializeField] HealthSettings playerHealth;
    private int health;
    private int numHearts;

    List<HeartUI> hearts;

    private GameObject heartUI;

    // Use this for initialization
    void Start()
    {
        health = playerHealth.maxHealth;

        PopulateHearts();
    }

    void PopulateHearts()
    {
        numHearts = 0;

        for (int i = 0; i < health; i++)
        {
            hearts.Add(Instantiate(heartUIPrefab, this.transform).GetComponent<HeartUI>());
            numHearts++;
        }

        UpdateHearts();
    }

    private void UpdateHearts()
    {


        for (int i = 0; i < health; i++)
        {
            if (i < numHearts)
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            health--;
            UpdateHearts();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            health++;
            UpdateHearts();
        }


    }
}
