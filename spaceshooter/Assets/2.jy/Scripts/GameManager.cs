using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //UI관련
    public GameObject life_0;
    public GameObject life_1;
    public GameObject life_2;
    public GameObject boomLife_0;
    public GameObject boomLife_1;
    public GameObject boomLife_2;
    public GameObject retryButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    public int score = 0;
    public GameObject player;
    public GameObject enemy;
    public GameObject playerBullet;
    // public GameObject EnemyBullet;
    public bool isGameOver = false;

    void Start()
    {
        life_0.SetActive(true);
        life_1.SetActive(true);
        life_2.SetActive(true);
        boomLife_0.SetActive(true);
        boomLife_1.SetActive(true);
        boomLife_2.SetActive(true);
        retryButton.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        gameOverText = GameObject.Find("GameOverText").GetComponent<TextMeshProUGUI>();
    }

    
    void Update()
    {
        
    }

     // 플레이어 HP 0 → 라이프 1 감소, 모두 소진 시 GameOver
    public void PlayerDied()
    {
        if (life_2 != null && life_2.activeSelf)       life_2.SetActive(false);
        else if (life_1 != null && life_1.activeSelf)  life_1.SetActive(false);
        else if (life_0 != null && life_0.activeSelf)  life_0.SetActive(false);

        if (!life_0.activeSelf && !life_1.activeSelf && !life_2.activeSelf)
        {
            Destroy(player);
            GameOver();
        }
    }

    // 씬의 모든 적·총알 제거 후 게임 오버 UI 표시
    public void GameOver()
    {
        isGameOver = true;

        // 씬에 남아있는 모든 적 제거
        foreach (Enemy e in FindObjectsByType<Enemy>())
            Destroy(e.gameObject);

        // 씬에 날아다니는 모든 적 총알 제거
        // foreach (EnemyBullet b in FindObjectsByType<EnemyBullet>())
            // Destroy(b.gameObject);

        gameOverText.gameObject.SetActive(true);
        retryButton.SetActive(true);
        scoreText.gameObject.SetActive(false);
    }

    // 리트라이 버튼 클릭 시 씬 리로드 → 라이프·HP 모두 초기화
    public void onRetryButtonClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    // 적 처치 시 점수 누적 및 UI 갱신
    public void AddScore(int exp)
    {
        score += exp;
        Debug.Log($"Score: {score}");
        if (scoreText != null)
            scoreText.text = score.ToString("#,##0");
    }

    // boomCount에 맞게 boomLife UI 아이콘 갱신
    // 사용 시: 좌→우 소멸 (boomLife_0 먼저), 획득 시: 우→좌 생성 (boomLife_2 먼저)
    public void SyncBoomUI(int boomCount)
    {
        if (boomLife_0 != null) boomLife_0.SetActive(boomCount >= 3); // 좌 (마지막 소멸)
        if (boomLife_1 != null) boomLife_1.SetActive(boomCount >= 2);
        if (boomLife_2 != null) boomLife_2.SetActive(boomCount >= 1); // 우 (첫 번째 소멸)
    }
}
