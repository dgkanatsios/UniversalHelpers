UniversalHelpers
================
[Also check my [blog post](http://dgkanatsios.com/2014/08/08/introducing-universal-helpers-for-windows-store-apps-3/) for this first release]

What's here?
First of all, there are some Blend Behaviors. I love behaviors! Easy just as a Drag’n’Drop, yet so powerful! What’s even more great is that Windows 8.1 SDK has a Behaviors SDK, which, to no one's surprise, works with Windows Phone 8.1 Store apps. A limitation right now is that you can’t (at least [easily](http://dotnet.dzone.com/articles/writing-behaviors-pcl-windows)) write Behaviors in a portable class library, that’s why I’ve implemented them in the Shared project. Let’s see them, one by one.

**DragElementBehavior**

This behavior allows any element with a CompositeTransform to be dragged. It has options for inertia plus a container, in order to never be out of bounds (either by dragging or inertia movement). Plus, it has Boolean options for rotation and multitouch scaling.

**FeedbackBehavior**

This behavior allows for simple feedback (opacity change) when an element is pressed. Moreover, with the flexibility the Pointer API provides us it increases the scale when the mouse is over the element.

**TapStoryboardBehavior**

This behavior executes an animation when the user taps an element. Optionally, when it’s completed, it can fire up a custom method (this one’s using Reflection, if you know a better way let me know!).

**TextBox related Behaviors**

Here we have some custom validation behaviors. Basically, if you want a TextBox

- to have a value different than String.Empty

- Contain only Double values

- Contain only Integer values

- Contain e-mail string

- Contain a string of a minimum length

you just provide the textbox to validate plus a textbox to show the error message and you’re done! You can force the check either on TextChanged event, on LostFocus event or both.

I want to use them. How?

Well, since I haven’t created a Portable Class Library with them, I can’t upload them on Nuget. So, for now, you’ll just have to go with the classic copy and paste method. 

**Storyboard Utilities**

You can find a portable class library in the project that contains some Storyboard extension methods. In a nutshell, these methods allow you to

- Translate an element on X and Y axis

- Animate an element’s opacity

- Rotate an element

- Scale an element

- Skew an element

- Animate a color on either a Shape (Ellipse, Rectangle) or on a Panel’s Background (e.g. Grid, Stackpanel)

- Move an element to another element’s coordinates on screen

- Move an element to a series of elements coordinates

- Animate a simple, single row spritesheet

The element must have a CompositeTransform, all methods are async and provide an optional Action parameter, in case you want to run a method at the completion of the Storyboard.

