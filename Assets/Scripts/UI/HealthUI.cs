using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject heartUIPrefab;

    private int playerHealth;
    private int numHearts;
    List<HeartUI> hearts;

    private bool waitingOnSpawn;

    public void Init(int playerHealth)
    {
        this.playerHealth = playerHealth;
        StartCoroutine(PopulateHearts());
    }

    public void SpawnInFinish()
    {
        waitingOnSpawn = false;
    }

    public void UpdateHearts(int currentHealth)
    {
        if (numHearts > playerHealth) numHearts = currentHealth;

        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].heartFill.SetActive(true);
            }
            else
            {
                hearts[i].heartFill.SetActive(false);
            }
        }
    }

    private IEnumerator PopulateHearts()
    {
        hearts = new List<HeartUI>();
        numHearts = playerHealth;

        int i = 0;
        for (i = 0; i < numHearts; i++)
        {
            HeartUI go = Instantiate(heartUIPrefab, transform).GetComponent<HeartUI>();

            if (go)
            {
                hearts.Add(go);
                go.Init();
            }
        }

        for (i = 0; i < hearts.Count; i++)
        {
            waitingOnSpawn = true;

            hearts[i].StartAnimation(this);
            while (waitingOnSpawn)
            {
                yield return null;
            }
        }

        UpdateHearts(playerHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //remove health --;
            UpdateHearts(playerHealth);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            //add health ++;
            UpdateHearts(playerHealth);
        }
    }
}
