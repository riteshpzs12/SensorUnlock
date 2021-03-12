using System;
using System.Windows.Input;
using SensorData.Models;
using SensorData.Services;
using Xamarin.Forms;

namespace SensorData.ViewModel.ScalePrecision
{
    public class ScalePrecisionViewModel : BaseViewModel
    {
        readonly INavService _navService;

        internal double ActualBound = 0;
        internal double FrameBound = 0;
        private double scalefactor;
        public double sliderFactor = 0;
        private double initial;
        LevelEnum level;
        public ScalePrecisionViewModel(INavService navService)
        {
            _navService = navService;
            ResultCommand = new Command(CheckResult);
            level = LevelEnum.Easy;
        }

        private async void CheckResult()
        {
            bool flag;
            await _navService.ShowDialog("Score", "Your Accuracy is " + Math.Round(GetResult(out flag), 2) + Environment.NewLine + "You drew " + (flag ? "more" : "less"));
            UpdatePlayGround();
        }

        private double GetResult(out bool val)
        {
            double error = BottomLine - TopLine * scalefactor / 2;
            val = error > 0;
            error = Math.Abs(error / TopLine * 100);
            if (error < 100)
                return 100 - error;
            else
                return 0;
        }

        internal void SlideIt(double value)
        {
            if (initial + (value - 150)*sliderFactor < 0)
                BottomLine = 0;
            else
                BottomLine = initial + (value-150);
        }

        internal void UpdatePlayGround()
        {
            Random random = new Random();
            if (sliderFactor == 0)
                sliderFactor = FrameBound / 300;
            scalefactor = random.Next(2, 8);
            ScaleInstruction = "scale to " + scalefactor / 2 + "x";
            ActualBound = (FrameBound / (scalefactor / 2) - 10);
            TopLine = GetLineTotest(random);
            BottomLine = FrameBound/2;
            initial = BottomLine;
        }

        private double GetLineTotest(Random random)
        {
            int t = 9999;
            while(t>ActualBound)
            {
                t = random.Next(25, 500);
            }
            return t;
        }

        private double _TopLine = 0;
        public double TopLine
        {
            get
            {
                return _TopLine;
            }
            set
            {
                _TopLine = value;
                OnPropertyChanged("TopLine");
            }
        }

        private double _BottomLine = 0;
        public double BottomLine
        {
            get
            {
                return _BottomLine;
            }
            set
            {
                _BottomLine = value;
                OnPropertyChanged("BottomLine");
            }
        }

        private string _ScaleInstruction = string.Empty;
        public string ScaleInstruction {
            get
            {
                return _ScaleInstruction;
            }
            set
            {
                _ScaleInstruction = value;
                OnPropertyChanged("ScaleInstruction");
            }
        }

        public ICommand ResultCommand { get; set; }
    }
}
