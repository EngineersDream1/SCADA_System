using MonitoringDashboard.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Streaming;
using System.Windows;
using System.Windows.Threading;
using System.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MonitoringDashboard.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<SensorsData> Sensors { get; set; } = new ObservableCollection<SensorsData>();

        private string dataBaseURL = "https://heatexchanger-ad719-default-rtdb.firebaseio.com/";

        private FirebaseClient firebaseClient = new FirebaseClient("https://heatexchanger-ad719-default-rtdb.firebaseio.com/");

        private SensorsData currentSensorData;
        public SensorsData CurrentSensorsData
        {
            get
            {
                return currentSensorData;
            }
            set
            {
                currentSensorData = value;
                OnPropertyChanged(nameof(CurrentSensorsData));
            }
        } 
                

        public MainViewModel()
        {
            Sensors.Clear();
            GetData();            
        }
        
        private void GetData()
        {
            var res = firebaseClient.Child("TestCase").AsObservable<SensorsData>();           
            
            res.Subscribe(data =>
            {
                var sensorData = new SensorsData()
                {
                    Flow1 = data.Object.Flow1,
                    Flow2 = data.Object.Flow2,
                    Temp1_In = data.Object.Temp1_In,
                    Temp1_Out = data.Object.Temp1_Out,
                    Temp2_In = data.Object.Temp2_In
                };

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {                    
                    Sensors.Add(sensorData);
                    CurrentSensorsData = sensorData;
                }));                              
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
