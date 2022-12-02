using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WishForGift : MonoBehaviour
{
    public GameObject giftSpawnSpot;
    public GameObject watch;
    public GameObject iPhone;
    public GameObject laptop;
    public GameObject wine;
    public GameObject cybertruck;
    public GameObject alexa;
    public TextMeshPro giftContentText;
    //public Animator bagAnimator;
 

    public void GenerateNewGift(string[] values)
    {
        Debug.Log("Wished Item is " + values[0]);
        var wishedItem = values[0];
        giftContentText.text = wishedItem;
        

        if(wishedItem == "watch"){
        
        Instantiate(watch, giftSpawnSpot.transform.position, giftSpawnSpot.transform.rotation);
        // giftObject.GetComponent<Rigidbody>().AddForce(transform.up * 200);
        
        }
        else if(wishedItem == "iphone"){
        Instantiate(iPhone, giftSpawnSpot.transform.position, giftSpawnSpot.transform.rotation);
        // giftObject.GetComponent<Rigidbody>().AddForce(transform.up * 200);
        }
        else if(wishedItem == "laptop"){
        Instantiate(laptop, giftSpawnSpot.transform.position, giftSpawnSpot.transform.rotation);
        // giftObject.GetComponent<Rigidbody>().AddForce(transform.up * 200);
        }else if(wishedItem == "wine"){
        Instantiate(wine, giftSpawnSpot.transform.position, giftSpawnSpot.transform.rotation);
        // giftObject.GetComponent<Rigidbody>().AddForce(transform.up * 200);
        }else if(wishedItem == "cybertruck"){
        Instantiate(cybertruck, giftSpawnSpot.transform.position, giftSpawnSpot.transform.rotation);
        // giftObject.GetComponent<Rigidbody>().AddForce(transform.up * 200);
        }else if(wishedItem == "alexa"){
        Instantiate(alexa, giftSpawnSpot.transform.position, giftSpawnSpot.transform.rotation);
        // giftObject.GetComponent<Rigidbody>().AddForce(transform.up * 200);
        }

        // if(wishedItem == "watch" || wishedItem == "iphone" || wishedItem == "laptop")
        // {
        //   bagAnimator.SetBool("shallPop", true);
        // }

     }
    }

