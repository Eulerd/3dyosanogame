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
        private int i;
        public TDModel()
        {
            modelname = System.IO.File.ReadAllLines("ModelName.txt");
            Handle = new int[modelname.Length];
            for(i = 0;i < modelname.Length; i++)
            {
                Handle[i] = DX.MV1LoadModel(modelname[i]);
                DX.MV1SetScale(Handle[i], DX.VGet(10, 10, 10));
            }
        }
        public void SetPos(int Mnumber,int x,int y,int z)
        {
            DX.MV1SetPosition(Handle[Mnumber], DX.VGet(x, y, z));
        }
    }
}
