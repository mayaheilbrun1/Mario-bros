using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endGame : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI MarioScore;
    private int mario;
    private int luigi;
    public TextMeshProUGUI LuigiScore;
    public GameObject luigiWins;
    public GameObject MarioWins;
    public GameObject bothWins;
    void Start()
    
    {
        mario = PlayerPrefs.GetInt("marioScore");
        luigi = PlayerPrefs.GetInt("luigiScore");
        MarioScore.text = ("" + mario);
        LuigiScore.text = ("" + luigi);
        if (mario>luigi)
        {
            MarioWins.SetActive(true);
            luigiWins.SetActive(false);
            bothWins.SetActive(false);
        }
        else if (mario<luigi)
        {
            MarioWins.SetActive(false);
            luigiWins.SetActive(true);
            bothWins.SetActive(false);
        }
        else
        {
            MarioWins.SetActive(false);
            luigiWins.SetActive(false);
            bothWins.SetActive(true);
        }
    }
    
    public void OnClick()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClickOnSpace()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        // If running in the Unity Editor, stop playing
            UnityEditor.EditorApplication.isPlaying = false;
        #else
                    // If running as a built game, quit the application
                    Application.Quit();
        #endif
    }
}
