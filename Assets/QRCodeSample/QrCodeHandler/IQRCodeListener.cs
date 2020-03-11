using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRCodeTracking
{
    public enum QREventType
    {
        Added,
        Updated,
        Removed
    };

    struct ActionData
    {
        
        public QREventType type;
        public Microsoft.MixedReality.QR.QRCode qrCode;

        public ActionData(QREventType type, Microsoft.MixedReality.QR.QRCode qRCode) : this()
        {
            this.type = type;
            qrCode = qRCode;
        }
    }
    public interface IQRCodeListener
    {
        void OnQRCode(Microsoft.MixedReality.QR.QRCode code, QREventType eventType);
        // event Type is create update remove
    }
}
