using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ThreeDYosanoGame
{
    class Camera
    {

        public DX.VECTOR Pos;
        public DX.VECTOR Target;
        public int CPx = 0;
        public int CPy = 0;
        public Camera(float tx, float ty, float tz, float posx, float posy, float posz)
        {
            Target.x = tx;
            Target.y = ty;
            Target.z = tz;
            Pos.x = posx;
            Pos.y = posy;
            Pos.z = posz;
        }
        public void SetCamera()
        {
            Pos.x = CPx;
            Pos.y = CPy;
            Pos = DX.VAdd(Target, Pos);
            DX.SetCameraPositionAndTarget_UpVecY(Pos, Target);

            Target.z = 0;
            //for Debug

            DX.DrawString(550, 125, Math.Sin((320 - Mouse.mousex) * 0.01).ToString(), DX.GetColor(255, 255, 255));
            DX.DrawString(550, 150, Math.Cos((240 - Mouse.mousey) * 0.01).ToString(), DX.GetColor(255, 255, 255));
            DX.DrawString(550, 50, Pos.x.ToString(), DX.GetColor(255, 255, 255));
            DX.DrawString(550, 75, Pos.y.ToString(), DX.GetColor(255, 255, 255));
            DX.DrawString(550, 100, Pos.z.ToString(), DX.GetColor(255, 255, 255));
            //
        }
    }
}
