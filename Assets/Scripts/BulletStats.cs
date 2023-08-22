using System;
using UnityEngine;

public abstract class BulletStats : MonoBehaviour{
	[HideInInspector] public float bulletSpeed = 10f;
	[HideInInspector] public float maxYSpeed = 4f;

	[HideInInspector] public int maxBounces = 3;

	protected Rigidbody2D Rb { get; private set; }
	protected SpriteRenderer Sprite { get; private set; }

	protected virtual void Start() {
		Rb = GetComponent<Rigidbody2D>();
		Sprite = GetComponent<SpriteRenderer>();
		
		BalanceCanvas.instance.sliderChangedCallback += RefreshStats;
		RefreshStats();
	}

	void OnDestroy() {
		BalanceCanvas.instance.sliderChangedCallback -= RefreshStats;
	}

	void RefreshStats() {
		bulletSpeed = PlayerPrefs.GetFloat(nameof(bulletSpeed), bulletSpeed);
		maxYSpeed = PlayerPrefs.GetFloat(nameof(maxYSpeed), maxYSpeed);
		maxBounces = Mathf.RoundToInt(PlayerPrefs.GetFloat(nameof(maxBounces), maxBounces));
	}
}