using sapr.Utilities;
using sapr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace sapr.Models
{
    public class SupportModelv2 : ModelBase
    {
        private double prPower;
        [JsonConverter(typeof(RectangleConverter))]
        private Rectangle model;
        private double admissibleStress;

        public double AdmissibleStress
        {
            get { return admissibleStress; }
            set
            {
                admissibleStress = value;
                OnPropertyChanged(nameof(AdmissibleStress));
            }
        }
        public double PrPower
        {
            get => prPower;
            set
            {
                prPower = value;
                PreProcessorViewModel.CnangeState(false);
            }
        }

        [JsonConverter(typeof(RectangleConverter))]
        public Rectangle Model
        {
            get => model;
            set
            {
                model = value;
            }
        }



        public SupportModelv2(int prp, Rectangle rectangle)
        {
            PrPower = prp;
            Model = rectangle;
        }
        public SupportModelv2()
        {
        }
    }
}
