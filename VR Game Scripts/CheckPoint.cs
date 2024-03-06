using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public List<GameObject> enemies;
    public bool requirePos = false;
    
    // Transform for respawning player at checkpoint start
    public Transform load_position;

    private bool isFinished = false;
    private bool enemyFinished = false;

    public bool getFinish()
    {
        return isFinished;
    }

	// Use this for initialization
	void Start () {
        // Enable all enemies
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if(this.gameObject.activeSelf)
        {
            bool tempFinish = true;
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    tempFinish = false;
                    break;
                }
            }
            enemyFinished = tempFinish;
            if(!requirePos)
            {
                isFinished = enemyFinished;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerModel")
        {
            if (enemyFinished)
            {
                isFinished = true;
            }
        }
    }

    private void OnEnable()
    {
        // Enable all enemies
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }
    }

    private void onDisable()
    {
        // Disable all enemies
        foreach (GameObject enemy in enemies)
        {
            if(enemy)
            {
                enemy.SetActive(false);
            }
        }
    }
}
