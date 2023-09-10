using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Firebase.Database.Query;

namespace SensorsImitator
{
    public class Sensors : INotifyPropertyChanged
    {
        private FirebaseClient _client;
        private string _path = "TestCase"; //Название категории в которую будут сохраняться данные
        private Random _random;

        private double firstTempValueIn;
        public double FirstTempValueIn
        {
            get
            {
                return firstTempValueIn;
            }
            set
            {
                firstTempValueIn = value;
                OnPropertyChanged(nameof(FirstTempValueIn));
            }
        }

        private double firstTempValueOut;
        public double FirstTempValueOut
        {
            get
            {
                return firstTempValueOut;
            }
            set
            {
                firstTempValueOut = value;
                OnPropertyChanged(nameof(FirstTempValueOut));
            }
        }

        private double secondTempValueIn;
        public double SecondTempValueIn
        {
            get
            {
                return secondTempValueIn;
            }
            set
            {
                secondTempValueIn = value;
                OnPropertyChanged(nameof(SecondTempValueIn));
            }
        }

        private double firstFlowValue;
        public double FirstFlowValue
        {
            get
            {
                return firstFlowValue;
            }
            set
            {
                firstFlowValue = value;
                OnPropertyChanged(nameof(FirstFlowValue));
            }
        }

        private double secondFlowValue;
        public double SecondFlowValue
        {
            get
            {
                return secondFlowValue;
            }
            set
            {
                secondFlowValue = value;
                OnPropertyChanged(nameof(SecondFlowValue));
            }
        }

        public Sensors()
        {
            _client = new FirebaseClient("https://heatexchanger-ad719-default-rtdb.firebaseio.com/"); //Здесь добавляется URL созданной базы данных Firebase
            _random = new Random();
        }


        //Генерация значений
        public async Task AddValuesInLoop()
        {
            while (true)
            {
                double temp1_In = GenerateValue(110.0, 114.0);
                double temp1_Out = GenerateValue(38.0, 41.5);
                double temp2_In = GenerateValue(19.0, 21.3);
                double flow1 = GenerateValue(5.7, 6.4);
                double flow2 = GenerateValue(21.0, 22.3);

                FirstTempValueIn = temp1_In;
                FirstTempValueOut = temp1_Out;
                SecondTempValueIn = temp2_In;
                FirstFlowValue= flow1;
                SecondFlowValue= flow2;

                var data = new
                {
                    Temp1_In = temp1_In,
                    Temp1_Out = temp1_Out,
                    Temp2_In = temp2_In,
                    Flow1= flow1,
                    Flow2= flow2
                };
                await _client.Child(_path).PostAsync(data);
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }

        private double GenerateValue(double minValue, double maxValue) // Вводятся границы в пределах которых будет сгенерированно значение
        {
            _random = new Random();
            _random.NextDouble();            
            return Math.Round((minValue + (maxValue - minValue) * _random.NextDouble()), 2);
        }
        

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
