# BoundaryDetector

BoundaryDetector is a sample Application which I developed while learning Xamarin Forms. I developed this project in MVVM architecture and can be able to run in both iOS and Android devices.I have used SQLite local Database and imlpemented many more features. I think this project will help lot of new developers who started learning Xamarin development.


<p align="center">
<img src="https://github.com/abdelhady-anka/BoundaryD/blob/master/Screenshots/1.jpg" width="30%" height="35%"/>
<img src="https://github.com/abdelhady-anka/BoundaryD/blob/master/Screenshots/2.jpg" width="30%" height="35%"/>
<img src="https://github.com/abdelhady-anka/BoundaryD/blob/master/Screenshots/3.jpg" width="30%" height="35%"/>
</p>

<p align="center">
<img src="https://github.com/abdelhady-anka/BoundaryD/blob/master/Screenshots/4.jpg" width="30%" height="35%"/>
<img src="https://github.com/abdelhady-anka/BoundaryD/blob/master/Screenshots/5.jpg" width="30%" height="35%"/>
<img src="https://github.com/abdelhady-anka/BoundaryD/blob/master/Screenshots/6.jpg" width="30%" height="35%"/>
</p>

## Architecture
The Model-View-ViewModel (MVVM) pattern helps to cleanly separate the business and presentation logic of an application from its user interface (UI). Maintaining a clean separation between application logic and the UI helps to address numerous development issues and can make an application easier to test, maintain, and evolve. It can also greatly improve code re-use opportunities and allows developers and UI designers to more easily collaborate when developing their respective parts of an app.
There are three core components in the MVVM pattern: the model, the view, and the view model. Each serves a distinct purpose.

<p align="center">
<img src="https://github.com/abdelhady-anka/BoundaryD/blob/master/Screenshots/mvvm.png"/>
</p>

## Publisher Subscriber Pattern
implemented by MessagingCenter

The Xamarin.Forms MessagingCenter class implements the publish-subscribe pattern, allowing message-based communication between components that are inconvenient to link by object and type references.

This mechanism allows publishers and subscribers to communicate without having a reference to each other, helping to reduce dependencies between them.

The MessagingCenter class provides multicast publish-subscribe functionality.

This means that there can be multiple publishers that publish a single message, and there can be multiple subscribers listening for the same message:
<p align="center">
<img src="https://github.com/abdelhady-anka/BoundaryD/blob/master/Screenshots/mc.png"/>
</p>

## References
1. [MVVM Pattern](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/mvvm)
2. [Getting started with Xamarin Forms](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/)
3. [Xamarin.Forms MVVM](https://medium.com/swlh/xamarin-forms-mvvm-how-to-work-with-sqlite-db-c-xaml-26fcae303edd)
