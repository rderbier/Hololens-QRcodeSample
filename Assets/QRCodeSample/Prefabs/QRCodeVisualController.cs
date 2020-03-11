using System.Collections;

using System.Collections.Generic;
using UnityEngine;

using TMPro;

#if WINDOWS_UWP

using Windows.Perception.Spatial;

#endif
namespace QRCodeTracking
{
    [RequireComponent(typeof(QRTracking.SpatialGraphCoordinateSystem))]
    public class QRCodeVisualController : MonoBehaviour
    {
        // Size and set labels for qr code visual element
        public Microsoft.MixedReality.QR.QRCode qrCode;
        private GameObject qrCodeCube;

        public float PhysicalSize { get; private set; }
        private string CodeText;

        public TextMeshPro QRID;
       // public TextMesh QRNodeID;
        public TextMeshPro QRText;
        public TextMeshPro QRVersion;
        //private TextMesh QRTimeStamp;
       // private TextMesh QRSize;
        public GameObject QRInfoPanel;
        private bool validURI = false;
        private bool launch = false;
        
        private long lastTimeStamp = 0;

        // Use this for initialization
        void Start()
        {
            
            PhysicalSize = 0.1f;
            CodeText = "Dummy";
            if (qrCode == null)
            {
                throw new System.Exception("QR Code Empty");
            }

            PhysicalSize = qrCode.PhysicalSideLength;
            CodeText = qrCode.Data;
            Debug.Log("Id= " + qrCode.Id + "NodeId= " + qrCode.SpatialGraphNodeId + " PhysicalSize = " + PhysicalSize + " TimeStamp = " + qrCode.SystemRelativeLastDetectedTime.Ticks + " QRVersion = " + qrCode.Version + " QRData = " + CodeText);



            if (QRID) { QRID.text = "Id:" + qrCode.Id.ToString(); }
            //QRNodeID.text = "NodeId:" + qrCode.SpatialGraphNodeId.ToString();
            QRText.text = CodeText;


            if (QRVersion) { QRVersion.text = "Ver: " + qrCode.Version; }

            
        }

        void UpdatePropertiesDisplay()
        {
            // Update properties that change
            if (qrCode != null && lastTimeStamp != qrCode.SystemRelativeLastDetectedTime.Ticks)
            {
              // QRSize.text = "Size:" + qrCode.PhysicalSideLength.ToString("F04") + "m";

               // QRTimeStamp.text = "Time:" + qrCode.LastDetectedTime.ToString("MM/dd/yyyy HH:mm:ss.fff");
                
                PhysicalSize = qrCode.PhysicalSideLength;
                Debug.Log("Id= " + qrCode.Id + "NodeId= " + qrCode.SpatialGraphNodeId + " PhysicalSize = " + PhysicalSize + " TimeStamp = " + qrCode.SystemRelativeLastDetectedTime.Ticks + " Time = " + qrCode.LastDetectedTime.ToString("MM/dd/yyyy HH:mm:ss.fff"));

                // this object is placed at upper left corner of the QRcode detected by the SpatialGraphCoordinateSystem script
                // just place the info panel 
                QRInfoPanel.transform.localPosition = new Vector3(PhysicalSize / 2.0f, PhysicalSize / 2.0f, 0.0f);
                QRInfoPanel.transform.localScale = new Vector3(PhysicalSize, PhysicalSize, PhysicalSize);
                lastTimeStamp = qrCode.SystemRelativeLastDetectedTime.Ticks;
               // QRInfo.transform.localScale = new Vector3(PhysicalSize/0.2f, PhysicalSize / 0.2f, PhysicalSize / 0.2f);
            }
        }

        // Update is called once per frame
        void Update()
        {
            UpdatePropertiesDisplay();
 
        }



    }
}