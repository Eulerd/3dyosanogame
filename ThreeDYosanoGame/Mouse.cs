using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ThreeDYosanoGame
{
    class Mouse
    {
        public static int mouse;
        public static int mousex;
        public static int mousey;
        public static void mousePos()
        {
            mouse = DX.GetMouseInput();
            DX.GetMousePoint(out mousex, out mousey);
            if (mousex < 0) mousex = 0;
            if (mousey < 0) mousey = 0;
            if (mousex > 640) mousex = 640;
            if (mousey > 480) mousey = 480;
            DX.SetMousePoint(mousex, mousey);
            //for Debug
            DX.DrawString(550, 0, mousex.ToString(), DX.GetColor(255, 255, 255));
            DX.DrawString(550, 25, mousey.ToString(), DX.GetColor(255, 255, 255));
            //
        }
    }
}