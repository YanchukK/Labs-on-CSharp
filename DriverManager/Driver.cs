using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class Driver
    {
        /*        ДАННЫЕ
         *        - поддержка ОС
         *        - тип устройства
         */


        private string driverName;
        private int driverVersion;
        private string driverType;

        public Driver(int DriverVersion, string DriverName, string DriverType)
        {
            driverVersion = DriverVersion;
            driverName = DriverName;
            driverType = DriverType;
        }

        public int DriverVersion
        {
            get
            {
                return driverVersion;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Отрицательных значений не должно быть!");
                }
                else
                    driverVersion = value;
            }
        }
        
        public string DriverName
        {
            get
            {
                return driverName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentOutOfRangeException("Пустых строк не должно быть!");
                }
                else
                    driverName = value;
            }
        }
        
        public override string ToString()
        {
              return "\nDriver: " + DriverName;
        }

        public bool Compare(Driver obj1, Driver obj2)
        {
             if (obj1.DriverVersion > obj2.DriverVersion)
                return true;
            return false;
        }
    }
}
