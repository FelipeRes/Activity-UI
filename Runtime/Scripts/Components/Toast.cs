using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ActivityUI{
    public class Toast : MonoBehaviour{
       
        public Animator animator;
        public Text text;
        static private Toast instance;
    
        public void Awake(){
            if(instance == null){
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }else{
                Destroy(this.gameObject);
            }
        }
        static public void Show(string message, float time){
            instance.animator.Play("Show",0,0);
            instance.text.text = message;
            instance.CancelInvoke("Hide");
            instance.Invoke("Hide",time);
        }
        private void Hide(){
            instance.animator.Play("Hide");
        }
    }
}
