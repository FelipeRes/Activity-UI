using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActivityUI{
    public abstract class Controller : MonoBehaviour{
        
        public virtual void OnCreate(object context){

        }
        public virtual void OnRestart(){
            
        }
        public virtual void OnStart(){
            
        }
        public virtual void OnResume(){
            
        }
        public virtual void OnPause(){
            
        }
        public virtual void OnStop(){
            
        }
        public virtual void OnDestroy(){
            
        }
    }
}
