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
        public DX.VECTOR Move;
        public Camera(float posx, float posy, float posz, float tarx, float tary, float tarz)
        {
            Target.x = tarx;
            Target.y = tary;
            Target.z = tarz;
            Pos.x = posx;
            Pos.y = posy;
            Pos.z = posz;
        }
        public void SetCamera()
        {
            Pos.x = (float)((320 - Mouse.mousex) * 0.5) + Move.x;
            Pos.y = (float)(-(240 - Mouse.mousey) * 0.5) + Move.y;
            Pos.z += Move.z;
            //Target = Move;
            //Pos = DX.VAdd(Pos, Move);
            //Target = DX.VAdd(Target, Move);
            DX.SetCameraPositionAndTarget_UpVecY(Pos, Target);


            Move.z = 0;
            //for Debug

            DX.DrawString(550, 50, "Tarx:" + Target.x.ToString(), DX.GetColor(255, 255, 255));
            DX.DrawString(550, 75, "Tary:" + Target.y.ToString(), DX.GetColor(255, 255, 255));
            DX.DrawString(550, 100, "Tarz:" + Target.z.ToString(), DX.GetColor(255, 255, 255));
            //
        }
    }
}
