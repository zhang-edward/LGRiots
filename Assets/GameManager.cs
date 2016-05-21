using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private float[] rhythm = new float[] {8, 8, 8, 3, 1, 2, 2};
	private int rhythmIndex = 0;
	private float time;

	public AudioClip[] slamSounds;
	private AudioSource audioSource;

	public int combo = 0;
	public Shake locker;
	public Shake cameraShake;

	public Text debugText;

	private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = this.GetComponent<SpriteRenderer> ();
		audioSource = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// DEBUG
		debugText.text = "" + (time - rhythm [rhythmIndex]);
		// DEBUG

		time += Time.deltaTime * 8;
		if (Input.GetKeyDown(KeyCode.Space))
		{
			// Player hit key on rhythm
			if (Mathf.Abs(time - rhythm[rhythmIndex]) <= 1.0f || rhythmIndex == 0)
			{
				Beat ();
				rhythmIndex++;
				Debug.Log (rhythmIndex);
				if (rhythmIndex > rhythm.Length - 1)
				{
					combo++;
					rhythmIndex = 0;
				}
			}
			// player did not hit the key on rhythm
			else
			{
				// Reset all variables
				rhythmIndex = 0;
				combo = 0;
			}
			time = 0;
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
		RandomizeSFX (0.5f);

		StartCoroutine ("BeatCR");
	}

	private IEnumerator BeatCR()
	{
		sr.color = Color.black;
		yield return new WaitForSeconds (0.05f);
		sr.color = Color.white;
	}

	private void RandomizeSFX(float pitchOffset)
	{
		audioSource.clip = slamSounds [Random.Range (0, slamSounds.Length)];
		audioSource.pitch = Random.Range (-pitchOffset, pitchOffset) + 1.0f;
		audioSource.Play ();
	}
}
