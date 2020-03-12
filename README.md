# Hololens-QRcodeSample


HoloLens 2 can detect QR codes in the environment facing the headset. When done the app tracks a coordinate system at each code's real-world location.

This project shows how to use [Microsoft.MixedReality.QR](https://docs.microsoft.com/en-us/windows/mixed-reality/qr-code-tracking) package in a HoloLens Application.
It is based on sample code from [chgatla-microsoft/QRTracking](https://github.com/chgatla-microsoft/QRTracking/tree/master/SampleQRCodes).


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

Open the Scene QRSamples/QRSamples : accept the import of TMP Essential (Text Mesh Pro).

Build in Unity3D

Deploy from Visual Studio.

The App will ask your permission to access your camera.

get so printed QRCodesin your environment (see the requirements) and experiment.

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
