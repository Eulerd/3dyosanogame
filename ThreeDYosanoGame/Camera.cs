using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace _3DActionGame__
{
    class Camera
    {

        public DX.VECTOR Pos;
        public DX.VECTOR Target;
        public int CPx = 0;
        public int CPy = 0;
        public Camera(float tx,float ty,float tz,float posx,float posy,float posz)
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
            DX.GetMousePoint(out CPx, out CPy);
            Pos.x = CPx;
            Pos.y = CPy;
            Pos = DX.VAdd(Target, Pos);
            DX.SetCameraPositionAndTarget_UpVecY(Pos, Target);
            Target.z = 0;
        }
    }
}
