using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private bool isGamePaused = false;

    [SerializeField] GameObject playerPrefab;
    private GameObject player;
    private GameObject checkpointFlower;

    private Camera camera;

    private GameObject canvas;
    [SerializeField] private int lifeTime;
    private int lifeTimer;
    private TextMeshProUGUI lifeTimeUI;
    private GameObject helpTextUI;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        instance = this;
        SceneManager.sceneLoaded += OnSceneLoad;
        isGamePaused = true;
        //InitAll();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (!isGamePaused && lifeTimer <= 0)
        {
            KillFly();
        }
    }

    IEnumerator UpdateTime()
    {
        while (true)
        {
            while (!isGamePaused)
            {
                UpdateLifeTimer();
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void KillFly()
    {
        isGamePaused = true;
        CurrentFlyDied();
    }

    private void CurrentFlyDied()
    {
        player.GetComponent<CharacterMovement>().Die();
        if (checkpointFlower == null)
        {
            GameOver();
        }
        else
        {
            StartCoroutine(WaitAndStartGameFromCheckPoint());
        }
    }

    IEnumerator WaitAndStartGameFromCheckPoint()
    {
        MatingSystem.KillMatedMates();

        yield return new WaitForSeconds(1.1f);

        SpawnPlayerAt(checkpointFlower);
        camera.GetComponent<CameraFollow>().target = player.transform;

        checkpointFlower = null;
        MatingSystem.GetInstance().Reset();

        ResetLifeTimer();
        isGamePaused = false;
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
    }

    private void SpawnPlayerAt(GameObject flower)
    {
        GameObject newPlayer = Instantiate(playerPrefab, flower.transform.position, Quaternion.identity);
        player = newPlayer;
    }

    private void UpdateLifeTimer()
    {
        lifeTimer -= 1;
        string lifeTimerString = string.Format("Life Time: {0}:{1:00}", lifeTimer / 60, lifeTimer % 60);
        lifeTimeUI.text = lifeTimerString;

        if (lifeTimer > 25)
            lifeTimeUI.color = Color.cyan;
        else if (lifeTimer > 10)
            lifeTimeUI.color = Color.yellow;
        else
            lifeTimeUI.color = Color.red;
    }

    public void PauseMainGame()
    {
        isGamePaused = true;
    }

    public void UnpauseMainGame()
    {
        isGamePaused = false;
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public void SetCheckpointFlower(GameObject flower)
    {
        checkpointFlower = flower;
    }

    public void StartNewGame()
    {
        LoadScene(1);
    }

    public void LoadScene(int sceneBuildIndex)
    {
        StartCoroutine(AsyncLoadScene(sceneBuildIndex));
    }

    private IEnumerator AsyncLoadScene(int sceneBuildIndex)
    {
        //loadingScreen.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        //loadingScreen.SetActive(true);
        isGamePaused = true;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        // main menu
        if (scene.buildIndex != 0)
        {
            InitAll();
            StartCoroutine(UpdateTime());
            isGamePaused = false;
        }
        //loadingScreen.SetActive(false);
    }
    public GameObject GetPlayer()
    {
        return player;
    }

    public GameObject GetHelpText()
    {
        return helpTextUI;
    }

    private void InitAll()
    {
        ResetLifeTimer();
        player = GameObject.Find("Player");
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        InitCanvas();
        MatingSystem.GetInstance().Init();
        TaskController.GetInstance().Init();
    }

    private void ResetLifeTimer()
    {
        lifeTimer = lifeTime;
    }

    private void InitCanvas()
    {
        canvas = GameObject.Find("Canvas");
        lifeTimeUI = canvas.transform.Find("LifeTimer").GetComponent<TextMeshProUGUI>();
        helpTextUI = canvas.transform.Find("HelpText").gameObject;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
