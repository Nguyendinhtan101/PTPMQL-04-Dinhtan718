using System;

namespace BAITAP2102.Models
{
    public class BMIModel
    {
        public double Weight { get; set; }
        public double Height { get; set; }
        public double BMI { get; set; }
        public string Result { get; set; }

        public void CalculateBMI()
        {
            BMI = Weight / (Height * Height);
            if (BMI < 18.5) Result = "Gầy";
            else if (BMI < 24.9) Result = "Bình thường";
            else if (BMI < 29.9) Result = "Thừa cân";
            else Result = "Béo phì";
        }
    }
}