﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lbManager : MonoBehaviour {

	public void LoadLevel(string sceneName) {
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}
}
