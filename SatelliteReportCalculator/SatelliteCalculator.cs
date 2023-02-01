using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteReportCalculator
{
    public class SatelliteCalculator
    {
        public SatelliteCalculator()
        {

        }

        public SatelliteCalculator(decimal mu, decimal ohm_e, decimal precision, int numberPrecision)
        {
            Mu = mu;
            Ohm_e = ohm_e;
            Precision = precision;
            NumberPrecision = numberPrecision;
        }

        #region 固定参数
        /// <summary>
        /// 地球引力场参数
        /// </summary>
        public decimal Mu { get; private set; } = 3.986005e14M;
        /// <summary>
        /// 地球自转角速度
        /// </summary>
        public decimal Ohm_e { get; private set; } = 7.2921151467e-5M;
        /// <summary>
        /// 开普勒方程迭代精度
        /// </summary>
        public decimal Precision { get; private set; } = 1e-6M;
        /// <summary>
        /// 输出小数的精度
        /// </summary>
        public int NumberPrecision { get; set; } = 2;
        #endregion

        /// <summary>
        /// 计算ECEF坐标（单位：km）
        /// </summary>
        /// <param name="report">卫星报文</param>
        /// <returns>ECEF的三维坐标</returns>
        public Point3D Calculate(Report report, ref string details)
        {
            string FormarString = "0.";
            for (int j = 0; j < NumberPrecision; j++)
                FormarString += "0";
            decimal dt = report.A0 + report.A1 * (report.T1 - report.Toc) +
                report.A2 * (report.T1 - report.Toc) * (report.T1 - report.Toc);
            decimal tk = report.T1 - dt - report.Toe;
            decimal n = DecimalSqrt(Mu / DecimalPow(report.Sqrta, 6)) + report.delta_N;
            decimal Mk = report.M0 + n * tk;
            decimal Ek = GetEk(Mk, report.E, out int num);
            decimal Vk = (decimal)Math.Acos((double)(DecimalCos(Ek - report.E) / (1 - report.E * DecimalCos(Ek))));
            decimal phi_k = Vk + report.Omega;
            decimal dr = report.Crc * DecimalCos(2 * phi_k) + report.Crs * DecimalSin(2 * phi_k);
            decimal r = (report.Sqrta) * (report.Sqrta) * (1 - report.E * DecimalCos(Ek)) + dr;
            decimal du_ = report.Cus * DecimalSin(2 * phi_k) + report.Cuc * DecimalCos(2 * phi_k);
            decimal u_ = phi_k + du_;
            decimal di = report.Cic * DecimalCos(2 * phi_k) + report.Cis * DecimalSin(2 * phi_k);
            decimal i = report.dI_dT * tk + report.I0 + di;
            decimal ohm = report.Ohm0 + (report.Ohm_dot - Ohm_e) * tk - report.Omega * report.Toe;
            decimal x = r * DecimalCos(u_);
            decimal y = r * DecimalSin(u_);
            details += "改正后的卫星平均角速度 n = " + n.ToString(FormarString) + "\n";
            details += "卫星钟差 Δt = " + dt.ToString(FormarString) + "\n";
            details += "归化时间 tk = " + tk.ToString(FormarString) + "\n";
            details += "观测时刻平近点角 Mk = " + Mk.ToString(FormarString) + "\n";
            details += "偏近点角 Ek = " + Ek.ToString(FormarString) + "，迭代次数: " + num + "\n";
            details += "真近点角 Vk = " + Vk.ToString(FormarString) + "\n";
            details += "升交距角 Φk = " + phi_k.ToString(FormarString) + "\n";
            details += "摄动改正项 Δr = " + dr.ToString(FormarString) + "\n";
            details += "摄动改正项 Δu = " + du_.ToString(FormarString) + "\n";
            details += "摄动改正项 Δi = " + di.ToString(FormarString) + "\n";
            details += "改正后的升交距角 U = " + u_.ToString(FormarString) + "\n";
            details += "改正后的向径 R = " + r.ToString(FormarString) + "\n";
            details += "改正后的轨道倾角 I = " + i.ToString(FormarString) + "\n";
            details += "轨道平面坐标系坐标: x = " + x.ToString(FormarString) + ", y = " + y.ToString(FormarString) + "\n";
            details += "观测时刻升交点经度 Ω = " + ohm.ToString(FormarString) + "\n";
            return new Point3D(x * DecimalCos(ohm) - y * DecimalCos(i) * DecimalSin(ohm),
                x * DecimalSin(ohm) + y * DecimalCos(i) * DecimalCos(ohm), y * DecimalSin(i));
        }

        /// <summary>
        /// 迭代开普勒方程
        /// </summary>
        /// <param name="m">参考时刻平近点角</param>
        /// <param name="e">偏心率</param>
        /// <returns>迭代结果</returns>
        private decimal GetEk(decimal m, decimal e, out int n)
        {
            n = 1;
            decimal Ekn = m;
            decimal Eknp1 = m + e * DecimalSin(Ekn);
            decimal temp;
            while (Math.Abs(Eknp1 - Ekn) > Precision)
            {
                temp = Eknp1;
                Eknp1 = m + e * DecimalSin(Ekn);
                Ekn = temp;
                n += 1;
            }
            return Eknp1;
        }

        static decimal DecimalSin(decimal x)
        {
            decimal x_long = x * 1000000000000000;
            long x_part1 = (long)x_long;
            if (x_long - x_part1 < 0)
                x_part1--;
            double a = (double)(((decimal)x_part1) / 1000000000000000);
            double b = (double)(x - (decimal)a);
            decimal result = ((decimal)Math.Sin(a)) * ((decimal)Math.Cos(b)) + ((decimal)Math.Cos(a)) * ((decimal)Math.Sin(b));
            return result;
        }

        static decimal DecimalCos(decimal x)
        {
            decimal x_long = x * 1000000000000;
            long x_part1 = (long)x_long;
            if (x_long - x_part1 < 0)
                x_part1--;
            double a = (double)(((decimal)x_part1) / 1000000000000);
            double b = (double)(x - (decimal)a);
            decimal result = ((decimal)Math.Cos(a)) * ((decimal)Math.Cos(b)) - ((decimal)Math.Sin(a)) * ((decimal)Math.Sin(b));
            return result;
        }

        static decimal DecimalPow(decimal x, int times)
        {
            decimal result = 1;
            for (int i = 0; i < times; i++)
            {
                result *= x;
            }
            return result;
        }

        static decimal DecimalSqrt(decimal d)
        {
            decimal x = d / 3;
            decimal lastX = 0m;
            for (int i = 0; i < 2000; i++)
            {
                x = (d / (x * 2)) + (x / 2);
                if (x == lastX) break;
                lastX = x;
            }
            return x;
        }


    }
}
