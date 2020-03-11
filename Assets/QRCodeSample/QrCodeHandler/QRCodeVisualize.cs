using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.QR;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;
using QRTracking;

namespace QRCodeTracking
{
    public class QRCodeVisualize: QRCodeListener
    {
       
        public GameObject qrCodePrefab;
        
        void Awake()
        {

        }

        // Use this for initialization
        public override void Start()
        {
            Debug.Log("QRCodesVisualizer start");
         

          
            if (qrCodePrefab == null)
            {
                throw new System.Exception("Prefab not assigned");
            }
            base.Start();
        }

        public override void HandleQRCodeAdded(Microsoft.MixedReality.QR.QRCode qrCode)
        {
            // new QR code detected
            if (info)
            {
                
                info.text = "\nNew QRCode : " + qrCode.Data;
            }

            GameObject qrCodeObject = Instantiate(qrCodePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            qrCodeObject.GetComponent<SpatialGraphCoordinateSystem>().Id = qrCode.SpatialGraphNodeId;
            qrCodeObject.GetComponent<QRCodeTracking.QRCodeVisualController>().qrCode = qrCode;
          
            LaunchUri(qrCode.Data);
            

        }
        private void TraceSceneObject()
        {
            // get root objects in scene
            List<GameObject> rootObjects = new List<GameObject>();
            Scene scene = SceneManager.GetActiveScene();
            scene.GetRootGameObjects(rootObjects);

            // iterate root objects and do something
            for (int i = 0; i < rootObjects.Count; ++i)
            {
                GameObject gameObject = rootObjects[i];
                info.text += "\n" + gameObject.name;
            }
        }
        public override void HandleQRCodeUpdated(Microsoft.MixedReality.QR.QRCode qrCode)
        {
            // ignore updates
        }
        public override void HandleQRCodeRemoved(Microsoft.MixedReality.QR.QRCode qrCode)
        {
           // ignore removed

        }
        void LaunchUri(string qrtext)
        {
            System.Uri uriResult;
            if (System.Uri.TryCreate(qrtext, System.UriKind.Absolute, out uriResult))
            {

#if WINDOWS_UWP
            // Launch the URI
            UnityEngine.WSA.Launcher.LaunchUri(uriResult.ToString(), true);
#endif
                TraceSceneObject();
            }

        }







    }

}