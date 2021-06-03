## Introduction
This package provides assets, animations and components to develop UI like the android system. I'm try mirror the activity android events and the concept of stack where each new activity will stay on top of the stack and you can back destroying the current activities.


## Get Started

### Setup the app scene
- Create a empty scene.
- Add a EventSystem to scene.
- Add a empty GameObject.
- Add the Launcher component to this GameObject.

### Setup a Activity

The **Main** activity is the first actitvity opened by the launcher and it is the root of back stack. All activities are a canvas with a body where you can customize by yourself with buttons, text, panels, toggles...

- Create prefab with a *Canvas* in its root.
- Add to prefab the *Activity* component.
- Add to prefab a *Animator* componet.
- Attach the *Activity* animator file in **Activity UI/Assets/Animations/Activity** packages folder to *Animator* component.
- Add a new panel game object to Canvas called Body as a Canvas child and setup its anchors to min to 0 and max to 1. Rename it to **Body**.
- Add a canvas group to **Body**.
- In the *Activity* component of prefab, setup all fields with the objects that you just created or added.
- Rename your prefab to **Main** and move it to **Assets/Resources/Activities/**

You can see the template of a **Main** prefab in **Activity UI/Assets** packages folder. You can create different activities with different names and launch its.

### Setup a Controller
The controler is where you code the behaviour of your activity. It can response the current events:
- **OnCreate**: Called when the Activity is created. Use this to setup first state of Activity behaviour.
- **OnStart**: Called after OnCreate. Use this to setup some behaviour that not use context and is necessary always the activity is restarted.
- **OnRestart**: Called when the app is on background and return to foreground.
- **OnPause**: Called when a new activity is created.
- **OnResume**: Called when the activity is paused and return.
- **OnStop**: Called when the app is move to background.
- **OnDestroy**: Called when the back buttton is pressed.

To create a controller, make a inheritance of Controler class in ActivityUI namespace. Theses events are implemented by virtual inheritance and you don't need to implement all of them. Look the example:

```csharp
using ActivityUI;

public class MainController: Controller{

    public override void OnCreate(object context){
        Toast.Show((string)context);
    }

    public override void OnStart(){
        Toast.Show("Starting");
    }

}
```

### Calling a new Activity
To call a new Activity, just code:
```csharp
Launcher.NewActivity("YourActivity", Context);
```
The first parameter is the name of prefab in **Assets/Resources/Activities/**. It will be instancied to foreground while the previous activity move to background.

The second parameter is the context. Is similar to **Intent** on android system, but here it is a simple **object**. If you send anything, the Activity who recive the context needs cast it to type what it expects.

The context is recived on **OnCreate** event from controller component:
```csharp

public class MainScreenController : Controller{

    override public void OnCreate(object context){
        name.text = (string)context;
    }

}
```
To return to preivous activity, you can only call Back:

```csharp
Launcher.Back();
```
Return to previous activity will destroy the current activity.
In Android system, you can push the back button. In iOS, you need to create a back button.

## Components
For while, I create 3 components similar to android components to UI:
### Toast
Toast only show a little message in the botton of screen.
```csharp
Toast.Show("Your message", timeInSeconds);
```
### Dialog
You can put the Dialog prefab in the scene and call its methods in the activity controller by direct reference. The dialog to show some informations and options to user. The left button is the default button and the right button is the alternative button. The dialog uses the builder pattern and you can customize your dialog with the methods:

**DialogBuilder AddMessage(string message)**
Is the first method to builder a dialog and you need to write the message as parameter.

**DialogBuilder AddHeader(string header)**
Add a header text to dialog.

**DialogBuilder AddActionToLeft(Action action)**
Define a action event to default button.

**DialogBuilder AddActionToRight(Action action)**
Define a action event to a alternative button. This button will not appear if you you don't define a action to it.

**DialogBuilder AddLeftButtonText(string text)**
Specify witch is the text of default button.

**DialogBuilder AddRightButtonText(string text)**
Specify witch is the text of second button. This button will appear with the default action to close the dialog.

**DialogBuilder AddLeftButtonColor(Color color)**
Change the color of the fisrt button text.

**DialogBuilder AddRightButtonColor(Color color)**
Change the color of the default button text. This button will appear with the default action to close the dialog.

**void Show()**
Show the dialog. Use this method only in the end of builder.

If you don't specify any action to dialog buttons so the default behaviour is close the dialog. If you specify any action to any button, after execute the action, it will close the dialog too. Example:

```csharp
public Dialog dialog;

public override void OnStart(){
    dialog.AddMessage("Do you want to use your real name as profile name?")
          .AddHeader("Waring")
          .AddLeftButtonColor(Color.blue);
          .AddRightButtonColor(Color.red);
          .AddLeftButtonText("Yes");
          .AddRightButtonText("No");
          .AddActionToLeft(()=>{
              profileName.text = user.name;
          })
          .Show();
}
```