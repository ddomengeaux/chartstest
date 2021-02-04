using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace chartstest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }

    public class ViewModel
    {
        public ObservableCollection<Model> Data { get; set; }

        private DateTime Date = DateTime.Now.AddDays(-10);

        public ViewModel()
        {
            Data = new ObservableCollection<Model>();

            Random random = new Random();

            for (int i = 0; i < 200; i++)
            {
                Data.Add(new Model() { XValue = Date, YValue = random.Next(1, 69) });
                Date = Date.AddMinutes(13);
            }
        }
    }

    public class Model
    {
        public DateTime XValue { get; set; }
        public double YValue { get; set; }
    }
}
