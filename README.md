# Create your first trivia app for iOS

[![Build your own Quiz App](https://img.youtube.com/vi/5py0nFKtoLU/0.jpg)](https://www.youtube.com/watch?v=5py0nFKtoLU)

1. Download [Unity](https://unity3d.com/get-unity/download/archive) if you haven't done it already. We recommend Unity 2019.2.3.
2. Download [Glowbom Quests 1.0.0](https://github.com/Glowbom/quests/releases) (**glowbom-quests-1.0.0.unitypackage**).
3. Create a new 2D project in **Unity**.
4. Double click on **glowbom-quests-1.0.0.unitypackage** to import **Glowbom Quests** into your project.
5. Open **DesignClassic** scene from **Project** window in Unity. You can find **DesignClassic** scene file at **Assets/Glowbom/Quests**.
6. Switch the platform to iOS by going to **File->Build Settings...** and clicking on **Switch Platform**.
7. Open **Glowbom Quests Editor** by going to **Window->Glowbom->Quests**.
8. Insinde **Quests** panel in the **All** you can add, remove and modify screens.
9. To run the project, click on **Play** button.
10. Click on the first screen item from the **All** tab.
11. Modify screen **Title** and text.
12. Bellow **Title** and text you can find the decision buttons section. Each button has **Title**, **Go To** section, and **Answer**. **Go To** represents a screen number where the button leeds to. **Answer** field can highlight the right and wrong answers (value 1 means *right answer*, 0 means *wrong*). 
13. To highlight the right and wrong answers, you need to send **Answers Count** to more than 0. More than 1 means more than one correct answer, 1 means only 1 correct answer. If **Answers Count** is 0, there will be no highlighting at all.
14. To show the results, put [correctAnswers] and [totalQuestionsCount] inside the screen text field. An example could be:

```
You got [correctAnswers] out of [totalQuestionsCount] questions correct.
```
The numbers will be replaced during the runtime.
15. To make some design changes, go to **Hierarchy**.
16. Select and expand **Canvas/Front** object to change the front screen of the app. **Canvas/Front/Title** and **Canvas/Front/Author** objects are usually used for displaying the app name and short description. You can also change text font and color from **Inspector**.
17. To change the main screen text select objects **Canvas/Panel/Title**, **Canvas/Panel/Text**, and for each button **Canvas/ButtonsPanel/Button/Text**, **Canvas/ButtonsPanel/Button1/Text**, etc. Each object can be easily moved around using the standard controls of Unity.

# Example App

Please download an example app for [iOS](https://apps.apple.com/us/app/6-countries-world-geography/id1497861140?ls=1) and [Android](https://play.google.com/store/apps/details?id=com.glowbom.quests) to explore what's possible to build with **Glowbom Quests**.

# Monetize your app using AdMob

[![Build your own Quiz App](https://img.youtube.com/vi/xvx6RtHJNKg/0.jpg)](https://www.youtube.com/watch?v=xvx6RtHJNKg)

# Deep dive into Glowbom Quests

Multiple quizzes, design themes, functional buttons, optimization tips.

Video is coming soon...

