using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ActivityUI{
    public class Launcher : MonoBehaviour{

        private static Stack<Activity> backStack = new Stack<Activity>();
        public static Activity currentActivity => backStack.Peek();
        void Start(){
            backStack.Clear();
            NewActivity("Main");
        }
        void Update(){
            if(Input.GetKeyDown(KeyCode.Escape)){
                Back();
            }
        }
        public static void NewActivity(string activityName){
            NewActivity(activityName,null);
        }
        public static void NewActivity(string activityName, object context){
            GameObject prefab = Resources.Load($"Activities/{activityName}") as GameObject;
            if(prefab.GetComponent<Activity>().type == ActivityType.STANDARD){
                InstantiateActivity(activityName,context,prefab);
            }else if(prefab.GetComponent<Activity>().type == ActivityType.SINGLE_TOP){
                if(backStack.Count > 0 && activityName == backStack.Peek().name){
                    backStack.Peek().onCreate(context);
                }else{
                    if(backStack.Count > 0){
                        backStack.Peek().onPause();
                    }
                    InstantiateActivity(activityName,context,prefab);
                }
                
            }
            
        }

        static void InstantiateActivity(string activityName, object context, GameObject prefab){
            GameObject activityObject = Instantiate(prefab);
            activityObject.transform.SetAsLastSibling();
            activityObject.name = $"Activity{backStack.Count}({activityName})";
            Activity activity = activityObject.GetComponent<Activity>();
            activity.canvas.sortingOrder = backStack.Count;
            activity.name = activityName;
            backStack.Push(activity);
            activity.onCreate(context);
        }

        public static void Back(){
            if(backStack.Count > 1){
                Activity pop = backStack.Pop();
                pop.onDestroy();
                backStack.Peek().onResume();
            }
        }

        void OnApplicationFocus (bool paused) { 
            if(backStack.Count > 0){
                if(!paused) {
                    backStack.Peek().onStop();
                }else{ 
                    backStack.Peek().onRestart();
                }
            }
        }
        
    }
}
