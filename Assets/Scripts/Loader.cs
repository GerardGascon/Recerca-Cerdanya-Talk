using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {
	static int? GetArg(string name) {
		string[] args = Environment.GetCommandLineArgs();
		for (int i = 0; i < args.Length; i++) {
			if (args[i] == name && args.Length > i + 1) {
				return int.Parse(args[i + 1]);
			}
		}

		return null;
	}
    
	void Awake() {
		int? arg = GetArg("scene");
		if (arg.HasValue) {
			SceneManager.LoadScene(arg.Value);
		} else {
			Application.Quit();
		}
	}
}