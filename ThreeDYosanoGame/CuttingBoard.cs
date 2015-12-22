using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace _3DActionGame__
{
    class CuttingBoard
    {
        public DX.VECTOR Point1 { get; set; }
        public DX.VECTOR Point2 { get; set; }
        public DX.VECTOR Point3 { get; set; }
        public DX.VECTOR Point4 { get; set; }
        public uint Color { get; set; }

        public CuttingBoard(int x1,int y1,int z1,int x2, int y2, int z2, int x3, int y3, int z3, int x4, int y4, int z4, uint color)
        {
            Point1 = DX.VGet(x1, y1, z1);
            Point2 = DX.VGet(x2, y2, z2);
            Point3 = DX.VGet(x3, y3, z3);
            Point4 = DX.VGet(x4, y4, z4);
            Color = color;
        }


        public void Draw()
        {
            DX.DrawTriangle3D(Point1, Point2, Point4, Color, 1);
            DX.DrawTriangle3D(Point2, Point3, Point4, Color, 1);
        }
    }
}
