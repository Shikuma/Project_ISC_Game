﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {
	public bool paused, pseudoPaused, qInProgress;
	public GameObject pausePanel, quizPanel;

	GameObject player, environmentParent;
	PlayerInput pi;
	EnvironmentController ec;
	PlayerStats ps;

	void Start() {
		player = GameObject.FindWithTag("Player");
		environmentParent = GameObject.FindWithTag("EnvParent");
		pi = player.GetComponent<PlayerInput>();
		ec = gameObject.GetComponent<EnvironmentController>();
		ps = player.GetComponent<PlayerStats>();
		pausePanel.SetActive(false);
	}

	public void PauseGame() {
		paused = !paused;
		Time.timeScale = paused ? 0 : 1;
		
	}

	public void PauseMenu() {
		PauseGame();
		pausePanel.SetActive(!pausePanel.activeSelf);
		if(paused && ps.timer.IsRunning) ps.timer.Stop();
		else ps.timer.Start();
		//pausePanel.SetActive(qInProgress ? false : paused);
		if (qInProgress) {
			quizPanel.SetActive(!paused);
		}
	}

	public void Quitting() {
		qInProgress = false;
		PauseGame();
	}
}
