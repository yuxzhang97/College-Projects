using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour {

    public List<CheckPoint> checkpoints;

    public Character player;

    private int checkpoint_index = 0;

    public void restart_checkpoint(int index)
    {
        checkpoints[checkpoint_index].gameObject.SetActive(false);
        checkpoint_index = index;
        checkpoints[checkpoint_index].gameObject.SetActive(true);
    }

    public int get_checkpoint_index()
    {
        return checkpoint_index;
    }

    public void pause () {
		Time.timeScale = 0;
	}

	public void unpause () {
        if(!player.getDead())
        {
            Time.timeScale = 1.0f;
        }
	}

	public void restart () {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void switch_to_mainmenu() {
		SceneManager.LoadScene("Menu");
	}
	
	public void quit() {
		#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
	}

    void gameover()
    {
        Debug.Log("Finish");
    }

    void checkpoint_fsm()
    {
        if(checkpoints[checkpoint_index].getFinish())
        {
            checkpoints[checkpoint_index].gameObject.SetActive(false);
            if(checkpoint_index >= checkpoints.Count - 1)
            {
                gameover();
            }
            else
            {
                checkpoint_index++;
                checkpoints[checkpoint_index].gameObject.SetActive(true);
            }
        }
    }

    void player_condition_check()
    {
        if(player.getDead())
        {
            pause();
            Debug.Log("Player died.");
        }
    }

	// Use this for initialization
	void Start () {
        print(checkpoints[0].gameObject.name);
        checkpoints[0].gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		checkpoint_fsm();
        player_condition_check();
	}
}
