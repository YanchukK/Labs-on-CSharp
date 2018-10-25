using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApplication2
{
    class Controller
    {
        // Блок работы с драйверами

        Dictionary<int, object> dictionaryOfDrivers = new Dictionary<int, object>();

        public void AddDriver(int version, object o)
        {
            dictionaryOfDrivers.Add(version, o);
        }

        public void RemoveDriver(int version)
        {
            dictionaryOfDrivers.Remove(version);
        }

        public int Lengtch()
        {
            return dictionaryOfDrivers.Count();
        }

        public void Sort()
        {
            dictionaryOfDrivers = dictionaryOfDrivers.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        // Блок работы с УСТРОЙСТВАМИ

        ArrayList listOfDevice = new ArrayList();
        ArrayList listOfDeviceName = new ArrayList();

        public void AddDevice(string s1, string s2)
        {
            Device device = new Device(s1, s2, null);
            listOfDevice.Add(device);
        }

        public int CountOfDevice()
        {
            return listOfDevice.Count;
        }

        public void RemoveDevice(int version)
        {
            listOfDevice.RemoveAt(version);
        }

        public string ObjectOfDeviceToString(int index)
        {
            string s = listOfDevice[index].ToString();
            return s;
        }

        public void AddDriverForDevice(int indexDevice, int indexDriver, string name, string type)
        {
            object driver = dictionaryOfDrivers[indexDriver];

            listOfDevice[indexDevice] = new Device(name, type, driver);
            listOfDeviceName.Add(name);
        }
        
        public bool ready()
        {
            if (listOfDeviceName.Contains("процессор") && listOfDeviceName.Contains("видеокарта")
                && listOfDeviceName.Contains("материнская_плата") && listOfDeviceName.Contains("оперативная_память")
                && listOfDeviceName.Contains("винчестер") && listOfDeviceName.Contains("клавиатура"))
            {
                return true;
            }
            else
                return false;
        }

        public void RemoveDriverFromDevice(int indexDevice, string name, string type)
        {
            listOfDevice[indexDevice] = new Device(name, type, null);
        }
    }
}
