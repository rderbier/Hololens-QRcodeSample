using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.QR;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;
using QRTracking;

namespace QRCodeTracking
{
    public class QRCodeListener : MonoBehaviour, IQRCodeListener
    {
        public Text info;

       

        

        private System.Collections.Generic.Queue<ActionData> pendingActions = new Queue<ActionData>();
        void Awake()
        {

        }

        // Use this for initialization
        public virtual void  Start()
        {
            Debug.Log("QRCodeListener start");
         

            QRCodesManager.Instance.QRCodesTrackingStateChanged += Instance_QRCodesTrackingStateChanged;
            QRCodesManager.Instance.OnQRCode += OnQRCode;

           
        }
        private void Instance_QRCodesTrackingStateChanged(object sender, bool status)
        {
            Debug.Log("QRCodesTrackingStateChanged  " + status); 
           
        }
        public virtual void OnQRCode(Microsoft.MixedReality.QR.QRCode code, QREventType eventType)
        {
            Debug.Log("QRCodeListener  "+eventType);
            lock (pendingActions)
            {
                pendingActions.Enqueue(new ActionData(eventType, code));
            }

        }
        public virtual  void HandleQRCodeAdded(Microsoft.MixedReality.QR.QRCode code)
        {

        }
        public virtual void HandleQRCodeUpdated(Microsoft.MixedReality.QR.QRCode code)
        {

        }
        public virtual void HandleQRCodeRemoved(Microsoft.MixedReality.QR.QRCode code)
        {

        }
        

        private void HandleEvents()
        {
            lock (pendingActions)
            {
                while (pendingActions.Count > 0)
                {
                    var action = pendingActions.Dequeue();

                    
                    switch (action.type)
                    {
                        case QREventType.Added :
                            
                            HandleQRCodeAdded(action.qrCode);
                            break;
                        case QREventType.Updated:
                            HandleQRCodeUpdated(action.qrCode);
                            break;
                        case QREventType.Removed:
                            HandleQRCodeRemoved(action.qrCode);
                            break;

                    }

                    
                    
                }
            }
           
        }

        // Update is called once per frame
        protected void Update()
        {
            HandleEvents();
        }
    }

}