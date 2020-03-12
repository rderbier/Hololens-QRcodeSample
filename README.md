# Hololens-QRcodeSample


With the help of [Microsoft.MixedReality.QR](https://docs.microsoft.com/en-us/windows/mixed-reality/qr-code-tracking) package, HoloLens 2 application can perform [QR code tracking] (https://docs.google.com/document/d/1etUpeEr5ulgMkZ4Go-treiiYqjXPVxl0XXYG4m5ai8I/edit)

This project shows how to use [Microsoft.MixedReality.QR](https://docs.microsoft.com/en-us/windows/mixed-reality/qr-code-tracking) package in a HoloLens 2 Application.
It is based on sample code from [chgatla-microsoft/QRTracking](https://github.com/chgatla-microsoft/QRTracking/tree/master/SampleQRCodes) with some tweaks.


I kept the original source code (QRTracking namespace) in QRTracking folder.
I only kept QRCode, Singleton and SpatialGraphCoordinateSystem
I created new scripts under QRCodeTracking namespace.

The sample Scene is using QRCodeVisualize to place a visual cue on QRcode detected and to launch an EdgeBrowser instance if the code is an URL. It also uses  QRCodeDetector to react to an expected code ("SAMPLE TAG 1").

QRCodeVisualize and QRCodeDetector both extend the QRCodeListener class.

## Environment
- Unity3D 2019.2.17f1
- MRTK V2.3
- Microsoft.MixedReality.QR 0.5.2092 installed with NuGet
- Visual Studio 2019

## Install
Download NugetForUnity package

Open the project in Unity3D. Microsoft.MixedReality.QR is handled by NuGet and will get installed.

Import MRTK Foundation package (2.3) : after some tests with Nuget I decided to install MRTK through the normal "import custom package".

The test scene is QRSamples/QRSamples and should be opened. Accept the import of TMP Essential (Text Mesh Pro) is requested.

Build in Unity3D (make sure to switch to UWP platform and follow [MRTK Build instructions](https://microsoft.github.io/MixedRealityToolkit-Unity/Documentation/BuildAndDeploy.html)  )

Deploy from Visual Studio.

The App will ask your permission to access your camera.

Get some printed QRCodes in your environment (see the requirements) and experiment (you need one with the expected label "SAMPLE TAG 1" if you have not changed the qrcode expected in the QR Code Detector script attached to the SceneManager object).

## Use Cases
QRCodes can help your application to better understand the environment.

A QRcode placed in a room could be used to initialize a correct AR scene and also place holograms (either relatively to the QR position or by restoring anchors once you get the room name).

They can also be used simply to reveal information (launch animations, open browser, ...)

## Implementation Notes
As the QR detection code is not running in a  MonoBehavior, we need a mechanism to communicate from the callback to the MonoBehavior class in order to act on scene objects. The initial code (Visualizer) is using a Queue of ActionData objects (the ActionData struct is defined in the Visualizer.cs). I simplified a bit the structure but kept the mechanism.

I introduce a use case where the app is looking for an expected QRcode value.

SceneManager object has a QRCodeDetector script which is configured to search for "SAMPLE TAG 1".
The Script has an "on QRCode" event. In the sample it is configured to play a sound.
The Visualizer is also Add spawning the Edge browser.

The low level framework keeps a list of QR codes detected between application launches. I could not find a way to "clear" the list. One way to workaround this issue is to use the timestamp of QR events and compare to the application launch time. I used another approach : I'm using only the 'Update' events and maintain my own list of detected code (this list got reset at each application start !). An low level Update event on a non known code is considered as a Create.
