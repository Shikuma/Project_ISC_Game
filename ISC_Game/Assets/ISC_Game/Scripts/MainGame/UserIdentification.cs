﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UserIdentification : MonoBehaviour {
	public GameObject idInput, submitBtn;
	public Text submitResponse, GOResponseText;
	public int user_id;
	public bool canSubmit;
	private string url;
	public string index;

	// Use this for initialization
	void Start () {
		canSubmit = false;
		user_id = 0;
		submitResponse.text = "If you would like to submit your results, press \"submit\" bellow.";
		idInput.SetActive(false);
	}

	public void TryFetchID() {
		try {
			//if (Application.isWebPlayer) {
				string url = Application.absoluteURL;
				user_id = int.Parse(url.Substring(url.IndexOf(index) + index.Length));
				canSubmit = true;
				submitBtn.SetActive(true);
			//}
			Debug.Log("Successfully Fetched User ID. " + user_id);
		}
		catch {
			//can't get id, do this
			idInput.SetActive(true);
			submitResponse.text += "\n ** Please input your ID below. **";
			Debug.Log("Failed to fetch user ID.");
		}
	}
	

	public void UpdateUserID() {
		InputField input = idInput.GetComponent<InputField>();
		//If left blank, set user id = 0
		if (input.text == "") {
			user_id = 0;
			canSubmit = true;
			submitBtn.SetActive(true);
			//Debug.Log("Setting user_id to 0");
		}
		else {
			//If number then continue
			try {
				user_id = int.Parse(input.text);
				GOResponseText.color = Color.black;
				GOResponseText.text = "";
				canSubmit = true;
				submitBtn.SetActive(true);

			//if not a number, don't continue
			}catch {
				canSubmit = false;
				GOResponseText.color = Color.red;
				GOResponseText.text = "Please insert a numerical value";
				submitBtn.SetActive(false);
			}
		}

	}

	private void GetUserID(string url) {
		
	}
}
