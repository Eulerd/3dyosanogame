using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;


namespace ThreeDYosanoGame
{
    class TDModel
    {
        public string[] modelname;
        public int[] Handle;
        public int[] HozyoHandle;
        private int i;
        public TDModel()
        {
            modelname = System.IO.File.ReadAllLines("ModelName.txt");
            HozyoHandle = new int[10];

            Handle = new int[modelname.Length];
            for (i = 0; i < modelname.Length; i++)
            {
                Handle[i] = DX.MV1LoadModel(modelname[i]);
                DX.MV1SetScale(Handle[i], DX.VGet(10, 10, 10));
            }

            for (i = 0;i < 10; i++)
            {
                HozyoHandle[i] = DX.MV1DuplicateModel(Handle[DX.GetRand(2) + 1]);
                DX.MV1SetScale(HozyoHandle[i], DX.VGet(10, 10, 10));
                //DX.MV1SetRotationXYZ(HozyoHandle[i], DX.VGet(0, (float)1.57, 0));
            }



        }


        public void SetPos(int Mnumber,int x,int y,int z)
        {
            DX.MV1SetPosition(Handle[Mnumber], DX.VGet(x, y, z));
        }


        public void Draw()
        {
            for (i = 0; i < modelname.Length; i++)
                DX.MV1DrawModel(Handle[i]);
        }
    }
}
