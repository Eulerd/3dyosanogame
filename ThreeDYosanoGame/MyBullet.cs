using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ThreeDYosanoGame
{
    class MyBullet
    {
        public DX.VECTOR[] Position = new DX.VECTOR[10];
        private int[] time = new int[10];
        public bool[] Flag = new bool[10];
        private int i;
        public int[] Time = new int[10];
        public bool BFlag;
        public void update()
        {
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
                    Position[i].z += 5;
                    DX.DrawSphere3D(Position[i], 10, 150, DX.GetColor(255, 0, 0), DX.GetColor(255, 255, 255), 1);
                }
            }

        }
        public void DrawHozyo(DX.VECTOR CPos)
        {
            for (i = 0; i < 10; i++)
            {
                if (!Flag[i] && BFlag)
                {
                    Flag[i] = true;
                    Time[i] = 0;
                    BFlag = false;
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
