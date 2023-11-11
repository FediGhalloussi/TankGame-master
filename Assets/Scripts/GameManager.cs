using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public AudioClip engineSound;
    public AudioClip fireSound;
    public AudioClip hitSound;
    public AudioClip destroyedSound;
    public AudioClip victorySound;
    public AudioClip gameOverSound;

    public GameObject playerTankPrefab;
    public GameObject computerTankPrefab;
    public Transform fightingZone;
    public Transform playerSpawnPoint;

    private int playerHitCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SpawnPlayerTank();
        SpawnComputerTanks();
    }

    void SpawnPlayerTank()
    {
        Instantiate(playerTankPrefab, playerSpawnPoint.position, Quaternion.identity);
    }
    void SpawnComputerTanks()
    {
        int numberOfComputerTanks = 4;

        for (int i = 0; i < numberOfComputerTanks; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            Instantiate(computerTankPrefab, randomPosition, randomRotation);
        }
    }

    public Vector3 GetRandomPosition()
    {
        float x = Random.Range(-500,500);
        float z = Random.Range(-500,500);
        float y = GetGroundHeight(x, z) + 1f;

        return new Vector3(x, y, z);
    }
    
    public bool IsInsideFightingZone(Vector3 position)
    {
        float halfWidth = 500;
        float halfLength = 500;

        return Mathf.Abs(position.x) < halfWidth &&
               Mathf.Abs(position.z) < halfLength
               ;
    }

    float GetGroundHeight(float x, float z)
    {
        RaycastHit hit;
        Ray ray = new Ray(new Vector3(x, fightingZone.position.y + 100f, z), Vector3.down);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            return hit.point.y;
        }

        return 0f; // Default height if raycast doesn't hit anything
    }

    public void TankDestroyed(bool isPlayerTank)
    {
        if (isPlayerTank)
        {
            PlayGameOverSound();
        }
        else
        {
            PlayDestroyedSound();
            if (AreAllComputerTanksDestroyed())
            {
                PlayVictorySound();
                //Wait 2 seconds before restarting the game
                Invoke("RestartGame", 2f);
            }
        }
    }

    bool AreAllComputerTanksDestroyed()
    {
        return FindObjectsOfType<ComputerTankController>().Length == 0;
        
    }

    public void PlayVictorySound()
    {
        AudioSource.PlayClipAtPoint(victorySound, Camera.main.transform.position);
    }

    public void PlayGameOverSound()
    {
        AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position);
    }
    
    public void PlayDestroyedSound()
    {
        AudioSource.PlayClipAtPoint(destroyedSound, Camera.main.transform.position);
    }
    
    public void PlayHitSound()
    {
        AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);
    }
    
    public void PlayFireSound()
    {
        AudioSource.PlayClipAtPoint(fireSound, Camera.main.transform.position);
    }
    
    public void PlayEngineSound()
    {
        AudioSource.PlayClipAtPoint(engineSound, Camera.main.transform.position);
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
