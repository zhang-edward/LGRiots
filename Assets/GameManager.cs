using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private float[] rhythm = new float[] {8, 8, 3, 1, 2, 2, 8};
	private int rhythmIndex = 0;
	private float time;
	private bool timeRunning;

	public AudioClip[] slamSounds;
	public AudioClip hey;
	private bool heying;
	private AudioSource audioSource;

	public int combo = 0;
	public Shake locker;
	public Shake cameraShake;

	public Transform indicator;

	public Text scoreText;

	//private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		//sr = this.GetComponent<SpriteRenderer> ();
		audioSource = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		/* DEBUG
		if (rhythmIndex < rhythm.Length)
			scoreText.text = "" + (time - rhythm [rhythmIndex]);
		DEBUG */
		if (combo == 0)
			scoreText.enabled = false;
		else
		{
			scoreText.enabled = true;
			scoreText.text = "" + combo;
		}

		if (timeRunning)
		{
			time += Time.deltaTime * 8;
			float scale = Mathf.Sqrt(Mathf.Abs (time - rhythm [rhythmIndex])) + .5f;
			indicator.localScale = new Vector3 (scale, scale);
		}
		else
		{
			indicator.localScale = new Vector3 (1, 1);
		}
		
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
		{
			if (rhythmIndex == 0 && !timeRunning)
			{
				Beat ();
				timeRunning = true;
			}
			// Player hit key on rhythm
			else if (timeRunning && Mathf.Abs(time - rhythm[rhythmIndex]) <= 1.0f)
			{
				Beat ();
				rhythmIndex++;
				Debug.Log (rhythmIndex);
				if (rhythmIndex > rhythm.Length - 1)
				{
					combo++;
					rhythmIndex = 0;
					heying = false;
				}
			}
			// player did not hit the key on rhythm
			else
			{
				// Reset all variables
				rhythmIndex = 0;
				combo = 0;
				timeRunning = false;
			}
			time = 0;
		}

		if (rhythmIndex == 6 && !heying)
		{
			Invoke ("Hey", rhythm [rhythmIndex] / 16.0f);
			heying = true;
		}
		/*if (time >= rhythm[rhythmIndex])
		{
			Beat ();
			time = 0;
			rhythmIndex++;
			if (rhythmIndex > rhythm.Length - 1)
			{
				rhythmIndex = 0;
			}
		}*/
	}

	private void Beat()
	{
		locker.Activate (0.25f, 0.05f);
		cameraShake.Activate (0.1f, 0.10f);
		RandomizeSFX (0.5f, slamSounds [Random.Range (0, slamSounds.Length)]);

		StartCoroutine ("BeatCR");
	}

	private void Hey()
	{
		RandomizeSFX (0.1f, hey);
	}

	private IEnumerator BeatCR()
	{
		//sr.color = Color.black;
		yield return new WaitForSeconds (0.05f);
		//sr.color = Color.white;
	}

	private void RandomizeSFX(float pitchOffset, AudioClip clip)
	{
		audioSource.clip = clip;
		audioSource.pitch = Random.Range (-pitchOffset, pitchOffset) + 1.0f;
		audioSource.Play ();
	}
}
