using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace ActivityUI{
    public class Activity : MonoBehaviour{

        public string name;
        public ActivityType type;
        public Animator animator;
        public Canvas canvas;
        public GameObject body;
        public Controller controller;

        public void onCreate(object context){
            Animate("OnCreate");
            Interactable(false);
            controller?.OnCreate(context);
            onStart();
        }
        public void onRestart(object context){
            Interactable(false);
            controller?.OnCreate(context);
            onStart();
        }
        public void onStart(){
            Interactable(true);
            controller?.OnStart();
        }
        public void onResume(){
            Active();
            Interactable(true);
            controller?.OnResume();
        }
        public void onPause(){
            Interactable(false);
            Invoke("Disactive",0.5f);
            controller?.OnActive();
        }
        public void onDestroy(){
            Interactable(false);
            Animate("OnDestroy");
            Invoke("Destroy",0.5f);
            controller?.OnDestroy();
        }

        void Animate(string name){
            animator.Play(name);
        }
        void Interactable(bool value){
            body.GetComponent<CanvasGroup>().interactable = value;
        }
        void Disactive(){
            body.SetActive(false);
        }
        void Active(){
            body.SetActive(true);
        }
        void Destroy(){
            Destroy(this.gameObject);
        }
        
    }
}
