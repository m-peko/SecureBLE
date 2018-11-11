using System;
using System.Collections.Generic;
using System.Text;

namespace ArduinoApp.Serivces.Interfaces
{
    public interface ICommunicationService
    {
        void InitializeCommunicationWithArduino();
        void ReadMessage(string message);
    }
}
