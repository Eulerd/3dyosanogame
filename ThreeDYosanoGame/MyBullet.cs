using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ThreeDYosanoGame
{
    class MyBullet : TDModel
    {
        public DX.VECTOR[] Position = new DX.VECTOR[10];
        public bool[] Flag = new bool[10];
        public DX.VECTOR[] Range = new DX.VECTOR[10];
        public int[] Time = new int[10];
        public bool BFlag;
        private int[] time = new int[10];
        private float[] U = new float[10];
        private float[] V = new float[10];
        private int i;
        private float speed = 0.5f;
        private Camera camera = new Camera();
        public void update()
        {
            DX.DrawString(30, 175, Range[1].z.ToString(), DX.GetColor(255, 255, 255));
            DX.DrawString(30, 200, Range[1].y.ToString(), DX.GetColor(255, 255, 255));
            DX.DrawString(30, 225, Range[1].x.ToString(), DX.GetColor(255, 255, 255));
            for (i = 0; i < 10; i++)
            {

                if (Time[i] > 200)
                {
                    Flag[i] = false;
                    Position[i] = DX.VGet(0, 0, 0);
                }

                if (Flag[i])
                    Time[i]++;
                else Time[i] = 0;
            }
            for (i = 0; i < 10; i++)
            {
                if (Flag[i] && Time[i] >= 0)
                {
                    Position[i].x += (float)(Math.Sin(U[i]) * Math.Sin(V[i]) * speed * Time[i]);
                    Position[i].z += (float)(Math.Cos(U[i]) * Math.Sin(V[i]) * speed * Time[i]);
                    Position[i].y += (float)(Math.Cos(V[i]) * speed * Time[i]);
                    DX.MV1SetPosition(HozyoHandle[i], Position[i]);
                    DX.MV1DrawModel(HozyoHandle[i]);
                    DX.MV1SetRotationXYZ(HozyoHandle[i], DX.VGet(Time[i] * (float)0.05, Time[i] * (float)0.08, 0));
                    //DX.DrawSphere3D(Position[i], 10, 150, DX.GetColor(255, 0, 0), DX.GetColor(255, 255, 255), 1);
                }

            }
        }
        public void DrawDice(DX.VECTOR CPos)
        {
            for (i = 0; i < 10; i++)
            {
                if (!Flag[i] && BFlag)
                {
                    Flag[i] = true;
                    Time[i] = 0;
                    BFlag = false;
                    U[i] = (float)Math.Atan2(Range[i].x, Range[i].z);
                    V[i] = (float)Math.Atan2(Range[i].z, Range[i].y);
                }

            }
            for (i = 0; i < 10; i++)
            {
                if (Flag[i] && Time[i] >= 0)
                {
                    if (Time[i] == 1)
                        Position[i] = CPos;
                }
            }
        }
    }
}
