using System;
using UnityEngine;

public abstract class PlayerStats : MonoBehaviour{
	[HideInInspector] public float deathForce = 8f;
	[HideInInspector] public float speed = 5f;
	[HideInInspector] public float jumpForce = 10f;
	[HideInInspector] public float acceleration = 1f;
	[HideInInspector] public float jumpCancellationMultiplier = .5f;
	[HideInInspector] public float coyoteTime = .1f;
	[HideInInspector] public float bufferTime = .1f;
	[HideInInspector] public int maxBullets = 2;
	
	public Rigidbody2D Rb { get; private set; }

	void Start() {
		Rb = GetComponent<Rigidbody2D>();
		
		BalanceCanvas.instance.sliderChangedCallback += RefreshStats;
		RefreshStats();
	}

	void OnDestroy() {
		BalanceCanvas.instance.sliderChangedCallback -= RefreshStats;
	}

	void RefreshStats() {
		deathForce = PlayerPrefs.GetFloat(nameof(deathForce), deathForce);
		speed = PlayerPrefs.GetFloat(nameof(speed), speed);
		jumpForce = PlayerPrefs.GetFloat(nameof(jumpForce), jumpForce);
		acceleration = PlayerPrefs.GetFloat(nameof(acceleration), acceleration);
		jumpCancellationMultiplier = PlayerPrefs.GetFloat(nameof(jumpCancellationMultiplier), jumpCancellationMultiplier);
		Rb.gravityScale = PlayerPrefs.GetFloat(nameof(Rb.gravityScale), Rb.gravityScale);
		coyoteTime = PlayerPrefs.GetFloat(nameof(coyoteTime), coyoteTime);
		bufferTime = PlayerPrefs.GetFloat(nameof(bufferTime), bufferTime);
		maxBullets = Mathf.RoundToInt(PlayerPrefs.GetFloat(nameof(maxBullets), maxBullets));
	}
}