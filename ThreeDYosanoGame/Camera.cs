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
        public float Roll = 0;
        public void SetCameraPos(float posx, float posy, float posz, float tarx, float tary, float tarz)
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
            Pos.x = (float)((320 - Mouse.mousex) * 0.5) + Target.x;
            Pos.y = (float)(-(240 - Mouse.mousey) * 0.5) + Target.y;
            DX.SetCameraPositionAndTargetAndUpVec(Pos, Target, DX.VGet((float)Math.Sin(Roll),(float)Math.Cos(Roll),0));
            //for Debug
            DX.DrawString(525, 50, "Tarx:" + Target.x.ToString(), DX.GetColor(255, 255, 255));
            DX.DrawString(525, 75, "Tary:" + Target.y.ToString(), DX.GetColor(255, 255, 255));
            DX.DrawString(525, 100, "Tarz:" + Target.z.ToString(), DX.GetColor(255, 255, 255));
            //
        }
    }
}
