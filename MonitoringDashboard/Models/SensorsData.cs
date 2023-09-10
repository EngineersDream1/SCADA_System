using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringDashboard.Models
{
    public class SensorsData : INotifyPropertyChanged
    {
        private double flow1;
        public double Flow1
        {
            get
            {
                return flow1;
            }
            set
            {
                flow1 = value;
                OnPropertyChanged();
            }
        }

        private double flow2;
        public double Flow2
        {
            get
            {
                return flow2;
            }
            set
            {
                flow2 = value;
                OnPropertyChanged();
            }
        }

        private double temp1_in;
        public double Temp1_In
        {
            get
            {
                return temp1_in;
            }
            set
            {
                temp1_in = value;
                OnPropertyChanged();
            }
        }

        private double temp1_out;
        public double Temp1_Out
        {
            get
            {
                return temp1_out;
            }
            set
            {
                temp1_out = value;
                OnPropertyChanged();
            }
        }

        private double temp2_in;
        public double Temp2_In
        {
            get
            {
                return temp2_in;
            }
            set
            {
                temp2_in = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
