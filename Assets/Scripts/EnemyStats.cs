using System;
using UnityEngine;

public abstract class EnemyStats : MonoBehaviour{
	[HideInInspector] public float throwForce = 5f;
	protected Rigidbody2D Rb { get; private set; }
	protected Collider2D Collider { get; private set; }

	protected virtual void Start() {
		Rb = GetComponent<Rigidbody2D>();
		Collider = GetComponent<Collider2D>();
		
		BalanceCanvas.instance.sliderChangedCallback += RefreshStats;
		RefreshStats();
	}

	void OnDestroy() {
		BalanceCanvas.instance.sliderChangedCallback -= RefreshStats;
	}

	void RefreshStats() {
		throwForce = PlayerPrefs.GetFloat(nameof(throwForce), throwForce);
	}
}