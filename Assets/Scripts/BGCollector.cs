﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCollector : MonoBehaviour {
    private GameObject[] backgrounds;
    private GameObject[] grounds;
    private float lastBGX;
    private float lastgroundX;

	// Use this for initialization
	void Awake () {

        backgrounds = GameObject.FindGameObjectsWithTag("Background");
        grounds = GameObject.FindGameObjectsWithTag("Ground");

        lastBGX = backgrounds[0].transform.position.x;
        lastgroundX = grounds[0].transform.position.x;

        for(int i=1;i<backgrounds.Length;i++)
        {
            if(lastBGX < backgrounds[i].transform.position.x)
            {
                lastBGX = backgrounds[i].transform.position.x;
            }
        }

        for (int i = 1; i < grounds.Length; i++)
        {
            if (lastgroundX < grounds[i].transform.position.x)
            {
                lastgroundX = grounds[i].transform.position.x;
            }
        }





    }


    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Background")
        {
            Vector3 temp = target.transform.position;
            float width = ((BoxCollider2D)target).size.x;

            temp.x = lastBGX + width;

            target.transform.position = temp;
            lastBGX = temp.x; 
        }

        else if (target.tag == "Ground")
        {
            Vector3 temp = target.transform.position;
            float width = ((BoxCollider2D)target).size.x;

            temp.x = lastgroundX+ width;

            target.transform.position = temp;
            lastgroundX = temp.x;
        }
    }
}
