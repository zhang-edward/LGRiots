using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour {

	private Vector3 origin;
	private Transform tr;

	private bool shaking;
	private float cooldown;
	private float magnitude;

	void Awake()
	{
		tr = transform;
		origin = transform.position;
	}

	public void Activate(float magnitude, float time)
	{
		shaking = true;
		cooldown = time;
		this.magnitude = magnitude;
	}

	void Update()
	{
		cooldown -= Time.deltaTime;
		if (cooldown <= 0)
			shaking = false;
		
		if (shaking)
		{
			float z = origin.z;
			float randX = Random.Range (-magnitude, magnitude);
			float randY = Random.Range (-magnitude, magnitude);
			tr.position = new Vector3 (randX, randY, z);
			magnitude -= 0.01f;
		}
		else
		{
			tr.position = origin;
		}
	}
}
