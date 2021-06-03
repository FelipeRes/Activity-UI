using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace ActivityUI{
    public class Dialog : MonoBehaviour{
        
        public Animator animator;
        public Text header;
        public Text message;
        public Button leftButton;
        public Button rightButton;
        public Text leftButtonText;
        public Text rightButtonText;

        public void ClearButton(){
            header.text = "";
            header.gameObject.SetActive(false);
            leftButton.onClick.RemoveAllListeners();
            leftButton.onClick.AddListener(()=>{Hide();});
            rightButton.onClick.RemoveAllListeners();
            rightButton.onClick.AddListener(()=>{Hide();});
            leftButtonText.text = "OK";
            rightButtonText.text = "Cancel";
            leftButtonText.color = Color.blue;
            rightButtonText.color = Color.blue;
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(false);
            
        }
        public void Hide(){
            animator.Play("Hide");
            Invoke("HideDialog",0.5f);
        }
        void HideDialog(){
            animator.gameObject.SetActive(false);
        }
        public DialogBuilder AddMessage(string message){
            ClearButton();
            DialogBuilder builder = new DialogBuilder();
            builder.dialog = this;
            builder.AddMessage(message);
            return builder;
        }
        public class DialogBuilder{

            public Dialog dialog;
            public DialogBuilder AddMessage(string message){
                dialog.message.text = message;
                return this;
            }
            public DialogBuilder AddHeader(string header){
                dialog.header.gameObject.SetActive(true);
                dialog.header.text = header;
                return this;
            }
            public DialogBuilder AddActionToLeft(Action action){
                dialog.leftButton.gameObject.SetActive(true);
                dialog.leftButton.onClick.RemoveAllListeners();
                dialog.leftButton.onClick.AddListener(()=>{action();dialog.Hide();});
                return this;
            }
            public DialogBuilder AddActionToRight(Action action){
                dialog.rightButton.gameObject.SetActive(true);
                dialog.rightButton.onClick.RemoveAllListeners();
                dialog.rightButton.onClick.AddListener(()=>{action();dialog.Hide();});
                return this;
            }
            public DialogBuilder AddLeftButtonText(string text){
                dialog.leftButtonText.gameObject.SetActive(true);
                dialog.leftButtonText.text = text;
                return this;
            }
            public DialogBuilder AddRightButtonText(string text){
                dialog.rightButton.gameObject.SetActive(true);
                dialog.rightButtonText.text = text;
                return this;
            }
            public DialogBuilder AddLeftButtonColor(Color color){
                dialog.leftButtonText.gameObject.SetActive(true);
                dialog.leftButtonText.color = color;
                return this;
            }
            public DialogBuilder AddRightButtonColor(Color color){
                dialog.rightButton.gameObject.SetActive(true);
                dialog.rightButtonText.color = color;
                return this;
            }
            public void Show(){
                dialog.animator.Play("Show");
                dialog.animator.gameObject.SetActive(true);
            }
        }
    }
}
