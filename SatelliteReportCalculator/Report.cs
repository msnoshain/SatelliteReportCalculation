using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteReportCalculator
{
    /// <summary>
    /// 报文
    /// </summary>
    public class Report
    {
        public Report()
        {

        }

        public Report(List<decimal> parameters)
        {
            Toc = parameters[0];
            Toe = parameters[1];
            T1 = parameters[2];
            A0 = parameters[3];
            A1 = parameters[4];
            A2 = parameters[5];
            Sqrta = parameters[6];
            E = parameters[7];
            I0 = parameters[8];
            Ohm0 = parameters[9];
            Omega = parameters[10];
            M0 = parameters[11];
            dI_dT = parameters[12];
            Ohm_dot = parameters[13];
            delta_N = parameters[14];
            Cuc = parameters[15];
            Cus = parameters[16];
            Cic = parameters[17];
            Cis = parameters[18];
            Crc = parameters[19];
            Crs = parameters[20];
        }

        public Report(List<double> parameters)
        {
            Toc = (decimal)parameters[0];
            Toe = (decimal)parameters[1];
            T1 = (decimal)parameters[2];
            A0 = (decimal)parameters[3];
            A1 = (decimal)parameters[4];
            A2 = (decimal)parameters[5];
            Sqrta = (decimal)parameters[6];
            E = (decimal)parameters[7];
            I0 = (decimal)parameters[8];
            Ohm0 = (decimal)parameters[9];
            Omega = (decimal)parameters[10];
            M0 = (decimal)parameters[11];
            dI_dT = (decimal)parameters[12];
            Ohm_dot = (decimal)parameters[13];
            delta_N = (decimal)parameters[14];
            Cuc = (decimal)parameters[15];
            Cus = (decimal)parameters[16];
            Cic = (decimal)parameters[17];
            Cis = (decimal)parameters[18];
            Crc = (decimal)parameters[19];
            Crs = (decimal)parameters[20];
        }

        public decimal Toc { get; set; }

        public decimal Toe { get; set; }

        public decimal T1 { get; set; }

        public decimal A0 { get; set; }

        public decimal A1 { get; set; }

        public decimal A2 { get; set; }

        public decimal Sqrta { get; set; }

        public decimal E { get; set; }

        public decimal I0 { get; set; }

        public decimal Ohm0 { get; set; }

        public decimal Omega { get; set; }

        public decimal M0 { get; set; }

        public decimal dI_dT { get; set; }

        public decimal Ohm_dot { get; set; }

        public decimal delta_N { get; set; }

        public decimal Cuc { get; set; }

        public decimal Cus { get; set; }

        public decimal Crc { get; set; }

        public decimal Crs { get; set; }

        public decimal Cic { get; set; }

        public decimal Cis { get; set; }

    }
}
