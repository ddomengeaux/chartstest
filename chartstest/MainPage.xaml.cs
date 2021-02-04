using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.SfChart.XForms;
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
        public ChartSeriesCollection Series
        {
            get => series;
            set => series = value;
        }

        private DateTime Date = DateTime.Now.AddDays(-10);
        private ChartSeriesCollection series;

        public ViewModel()
        {
            Data = new ObservableCollection<Model>();
            Series = new ChartSeriesCollection();

            Random random = new Random();

            for (int i = 0; i < 200; i++)
            {
                Data.Add(new Model() { XValue = Date, YValue = random.Next(1, 69) });
                Date = Date.AddMinutes(13);
            }

            foreach (var batch in Data.Batch(100))
            {
                Series.Add(new FastLineSeries()
                {
                    ItemsSource = batch,
                    XBindingPath = "XValue",
                    YBindingPath = "YValue",
                    Color = GetRandomColor()
                });
            }
        }

        public static Color GetRandomColor()
        {
            var rnd = new Random();
            return Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }
    }

    public class Model
    {
        public DateTime XValue { get; set; }
        public double YValue { get; set; }
    }

    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> enumerable, int batchSize)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            if (batchSize <= 0) throw new ArgumentOutOfRangeException(nameof(batchSize));
            return enumerable.BatchCore(batchSize);
        }

        private static IEnumerable<IEnumerable<T>> BatchCore<T>(this IEnumerable<T> enumerable, int batchSize)
        {
            var c = 0;
            var batch = new List<T>();
            foreach (var item in enumerable)
            {
                batch.Add(item);
                if (++c % batchSize == 0)
                {
                    yield return batch;
                    batch = new List<T>();
                }
            }
            if (batch.Count != 0)
            {
                yield return batch;
            }
        }
    }
}
