using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance;
    
    private int luigiScore = 0;
    private int marioScore = 0;
    public TextMeshProUGUI topScoreText;
    public TextMeshProUGUI marioScoreText;
    public TextMeshProUGUI luigiScoreText;
    public int luigiLives = 3;
    public int marioLives = 3;
    public PlayerController mario;
    public PlayerController luigi;
    public GameObject marioLives_UI;
    public GameObject luigiLives_UI;

    public bool marioInvicible = false;
    public bool luigiInvicible = false;

    private bool paused = false;
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Singleton violation"); return;
        }

        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.AudioController.PlayCommand(AudioManager.AudioController.startLevel);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(1);
            Debug.Log("restart");
        }
        if (Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene(2);
            Debug.Log("endGame");
        }

        if (Input.GetKey(KeyCode.M))
        {
           changeMarioInvi(); 
        }

        if (Input.GetKey(KeyCode.L))
        {
            changeLuigiInvi();
        }
    }

    public void lose_life(string loseTag)
    {
        Transform[] MariochildTransforms = marioLives_UI.GetComponentsInChildren<Transform>(true);
        Transform[] LuigichildTransforms = luigiLives_UI.GetComponentsInChildren<Transform>(true);
        if (loseTag == "mario")
        {
            marioScore -= 300;
            if (marioScore<0)
            {
                marioScore = 0;
            }
            marioScoreText.text = (""+marioScore);
            
            print(marioLives);
            if (marioLives>1)
            {
                if (MariochildTransforms.Length > marioLives)
                {
                    GameObject image = MariochildTransforms[marioLives].gameObject;
                    image.SetActive(false);
                }
                marioLives -= 1;
                AudioManager.AudioController.PlayCommand(AudioManager.AudioController.loseLife);
            }
            else
            {
                GameObject image = MariochildTransforms[0].gameObject;
                image.SetActive(false);
                AudioManager.AudioController.PlayCommand(AudioManager.AudioController.PlayerDeath);
                Invoke("GameOver", 0.1f);
            } 
        }
        else
        {
            luigiScore -= 300;
            if (luigiScore<0)
            {
                luigiScore = 0;
            }
            luigiScoreText.text = (""+luigiScore);
            if (luigiLives>1)
            {
                if (LuigichildTransforms.Length > luigiLives)
                {
                    GameObject image =LuigichildTransforms[luigiLives].gameObject;
                    image.SetActive(false);
                }
                luigiLives -= 1;
                AudioManager.AudioController.PlayCommand(AudioManager.AudioController.loseLife);
            }
            else
            {
                GameObject image = LuigichildTransforms[0].gameObject;
                image.SetActive(false);
                AudioManager.AudioController.PlayCommand(AudioManager.AudioController.PlayerDeath);
                Invoke("GameOver", 0.1f);
            } 
        }
        updateTopScore();
        
    }
    
    public void UpScore(string playertag, string hit)
    {
        int scoreToAdd;
        if (hit == "wave")
        {
            scoreToAdd = 10;
        }
        else
        {
            AudioManager.AudioController.PlayCommand(AudioManager.AudioController.collect);
            scoreToAdd = 800;
        }
        
        if (playertag == "mario")
        {
            marioScore += scoreToAdd;
            marioScoreText.text = (""+marioScore);
        }
        else
        {
            luigiScore += scoreToAdd;
            luigiScoreText.text = (""+luigiScore);
        }

        updateTopScore();
    }

    private void updateTopScore()
    {
        if (luigiScore>marioScore)
        {
            topScoreText.text = ("" + luigiScore);
        }
        else
        {
            topScoreText.text = ("" + marioScore);
        }
    }
    
    public void GameOver()
    {
        PlayerPrefs.SetInt("marioScore", marioScore);
        PlayerPrefs.SetInt("luigiScore", luigiScore);
        Time.timeScale = 0;

        // Load the scene
        // SceneManager.LoadScene(sceneName);
        StartCoroutine(LoadSceneAfterDelay(2));
        
    }
    IEnumerator LoadSceneAfterDelay(int i)
    {
        // Wait for one second
        yield return new WaitForSecondsRealtime(1f);

        // Load the scene
        SceneManager.LoadScene(i);
    }


    // Update is called once per frame
    public void changeMarioInvi()
    {
        print( "marioInvicible"+marioInvicible+"change");
        marioInvicible = true;
        print( "marioInvicible"+marioInvicible+"change");
        Invoke("changeBackInvinciMario", 4f);
    }
    public void changeLuigiInvi()
    {
        luigiInvicible = true;
        print( "LuigiInvicible"+luigiInvicible);
        Invoke("changeBackInvinciLuigi", 4f);
    }

    public void changeBackInvinciMario()
    {
        marioInvicible = false;
        print( "marioInvicible"+marioInvicible+"back");
    }
    
    public void changeBackInvinciLuigi()
    {
        luigiInvicible = false;
        print( "LuigiInvicible"+luigiInvicible);
    }
}
