namespace SatelliteReportCalculator
{
    public struct Point3D
    {
        public decimal X { get; set; }

        public decimal Y { get; set; }

        public decimal Z { get; set; }

        public Point3D()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Point3D(decimal x, decimal y, decimal z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// 设置或获取坐标输出的精度
        /// </summary>
        public int Precision { get; set; } = 2;

        public override string ToString()
        {
            string FormatString = "F" + Precision.ToString();
            return "X=" + X.ToString(FormatString) + ", Y=" + Y.ToString(FormatString) + ", Z=" + Z.ToString(FormatString);
        }
    }
}