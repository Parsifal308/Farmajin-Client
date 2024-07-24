using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Playerdown : MonoBehaviour {

	public float jumpForce = 10f;

	public Rigidbody2D rb;
	public SpriteRenderer sr;

	public string currentColor;

	public Color colorCyan;
	public Color colorYellow;
	public Color colorMagenta;
	public Color colorPink;

	

	public GameObject gameOverPanel;
	public GameObject victoryPanel; // Panel to show when the player reaches the maximum score
	public bool gamePoused = false;

	public Text ScoreText;
	public Text FinalScore;
	public int pointsPerColorChange = 10; // Number of points to add per color change

	public int maxScore = 20; // Maximum score that ends the game


	// Add a variable for the coin count
    private int coins = 0;

    // Add a Text object to display the coin count
    public Text CoinText;

	// Add a Text object to display the total coin count
    public Text TotalCoinText;




	private int score = 0;

	public GameObject Scoretext;

	public AudioSource audioSource;
    public AudioClip jumpSound;






	void Start ()
	{
		SetRandomColor();
		timeWorld(0);
		// Initialize the coin text
        CoinText.text = "0";
		TotalCoinText.text = "0";
	}
	
	// Update is called once per frame
	void Update () {




		
		if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
		{
			if (gamePoused== false)
            {
				timeWorld(1);
				gamePoused = true;
			}
			rb.velocity = Vector2.up * jumpForce;
			audioSource.PlayOneShot(jumpSound);

		}

		// Check if the score has reached the maximum
        if (score >= maxScore) 
		{
            EndGame(true);
        }

		
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "ColorChanger")
		{
			SetRandomColor();
			Destroy(col.gameObject);
			AddPoints(pointsPerColorChange);

			// Increment the coin count when a color change occurs
            coins += 1;

            // Update the coin text
            CoinText.text = "" + coins;

			// Update the total coin text
            TotalCoinText.text = "" + coins;

			return;
		}

		if (col.tag != currentColor)
		{
			EndGame(false);

		}
	}
	void AddPoints(int points)
	{
		score += points;
		Debug.Log("" + score);
		ScoreText.text = "" + score; // Update the text component with the updated score
		if (score >= maxScore) 
		{
            EndGame(true);
        }
	}


	void timeWorld(int value) {
			Time.timeScale = value;
	}
	public void gameOverBack()
	{
				
		// Load the currently active scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

		gameOverPanel.SetActive(false);
		Time.timeScale = 1;
	}


	public void gameOver()
	{
		SceneManager.LoadScene("main");
		gameOverPanel.SetActive(false);
		
	}

	void EndGame(bool victory) {
        if (victory) {
            victoryPanel.SetActive(true);
        } else {
            gameOverPanel.SetActive(true);
        }

        // Update the score display
        ScoreText.text = score.ToString();

        // Update the final score display
        FinalScore.text = "" + score.ToString();

		TotalCoinText.text = "+ " + coins.ToString();


        Scoretext.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

	void SetRandomColor ()
	{
		int index = Random.Range(0, 4);

		switch (index)
		{
			case 0:
				currentColor = "Cyan";
				sr.color = colorCyan;
				break;
			case 1:
				currentColor = "Yellow";
				sr.color = colorYellow;
				break;
			case 2:
				currentColor = "Magenta";
				sr.color = colorMagenta;
				break;
			case 3:
				currentColor = "Pink";
				sr.color = colorPink;
				break;
		}
	}
}
