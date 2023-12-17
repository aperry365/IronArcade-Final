﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public ParticleSystem goal;
    public Text text;
    public AudioSource source;
    public GameObject shake;
    public static int points = 0;
    Vector3 startPos;
    public GameObject QuizPannel;

    private void Start()
    {
        startPos = shake.transform.position;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball" && Movement.counter == 3)
        {
            
            shake.transform.localPosition = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(9.5f, 10.5f), shake.transform.position.z);
            source.Play();
            goal.Play();
            StartCoroutine(ShowQuizPanelAfterDelay(8f));
        }

        if (other.gameObject.tag == "Ball" && Movement.counter == 2)
        {
            points += 2;
            shake.transform.localPosition = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(9.5f, 10.5f), shake.transform.position.z);
            text.text = "Points: " + points.ToString();
            source.Play();
            goal.Play();
            StartCoroutine(ShowQuizPanelAfterDelay(8f));
        }
    }

    IEnumerator ShowQuizPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        QuizPannel.SetActive(true);
    }
}
