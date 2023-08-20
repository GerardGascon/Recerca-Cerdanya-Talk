using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BalanceCanvas : MonoBehaviour {

	public static BalanceCanvas instance;
	
	[SerializeField] Canvas canvas;
	PlayerInput _playerInput;

	public Action sliderChangedCallback;

	[Header("Sliders")] 
	[SerializeField] Slider speedSlider;
	[SerializeField] Slider deathForceSlider;
	[SerializeField] Slider jumpSlider;
	[SerializeField] Slider accelerationSlider;
	[SerializeField] Slider jumpCancelSlider;
	[SerializeField] Slider gravitySlider;
	[SerializeField] Slider coyoteTime;
	[SerializeField] Slider bufferTime;
	[SerializeField] Slider bulletSpeed;
	[SerializeField] Slider maxYSpeed;
	[SerializeField] Slider maxBounces;
	[SerializeField] Slider maxBullets;
	
	// Start is called before the first frame update
	void Awake() {
		instance = this;
		
		_playerInput = new PlayerInput();
		_playerInput.Gameplay.Cancel.started += CanvasStateSwitch;
		
		SetupSlider(speedSlider, nameof(PlayerStats.speed));
		SetupSlider(deathForceSlider, nameof(PlayerStats.deathForce));
		SetupSlider(accelerationSlider, nameof(PlayerStats.acceleration));
		SetupSlider(jumpSlider, nameof(PlayerStats.jumpForce));
		SetupSlider(jumpCancelSlider, nameof(PlayerStats.jumpCancellationMultiplier));
		SetupSlider(gravitySlider, nameof(PlayerStats.Rb.gravityScale));
		SetupSlider(coyoteTime, nameof(PlayerStats.coyoteTime));
		SetupSlider(bufferTime, nameof(PlayerStats.bufferTime));
		SetupSlider(bulletSpeed, nameof(BulletStats.bulletSpeed));
		SetupSlider(maxYSpeed, nameof(BulletStats.maxYSpeed));
		SetupSlider(maxBounces, nameof(BulletStats.maxBounces));
		SetupSlider(maxBullets, nameof(PlayerStats.maxBullets));
	}

	void SliderChangedCallback(float arg0) => sliderChangedCallback?.Invoke();

	void SetupSlider(Slider slider, string valueName) {
		slider.onValueChanged.AddListener(value => {
			PlayerPrefs.SetFloat(valueName, value);
		});
		slider.onValueChanged.AddListener(SliderChangedCallback);
		slider.SetValueWithoutNotify(PlayerPrefs.GetFloat(valueName, slider.value));
	}
	
	void CanvasStateSwitch(InputAction.CallbackContext obj) {
		canvas.enabled ^= true;
	}

	void OnEnable() => _playerInput.Enable();
	void OnDisable() => _playerInput.Disable();
}