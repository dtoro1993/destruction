﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
		score = 0;
    }

	void Start(){

	}

    void Update()
    {
        text.text = "SCORE "+score;
    }
}