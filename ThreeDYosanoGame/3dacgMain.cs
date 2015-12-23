using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace _3DActionGame__
{
    class _3dacgMain
    {
        public static void Main(string[] args)
        {
            DX.ChangeWindowMode(1);
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);//摩訶不思議!!なぜか重くなる現象回避
            DX.DxLib_Init();
            byte[] keys = new byte[256];
            int mouse = 0;
            CuttingBoard CB = new CuttingBoard(-1100, 1000, 0, -1100, 1000, 1500, -1000, -510, 1500, -1000, -510, 0, DX.GetColor(0, 93, 153));
            MyBullet mybullet = new MyBullet();
            /*
                        Target.x = 0;
            Target.y = 0;
            Target.z = 0;
            Pos.x = 0;
            Pos.y = 0;
            Pos.z = -800;
            */
            Camera camera = new Camera(0,0,0,0,0,-10000);
            DX.SetWriteZBuffer3D(1);
            DX.SetUseZBuffer3D(1);
            float Far = 3000;

            DX.SetBackgroundColor(100, 100, 100);
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);
            DX.ChangeLightTypeDir(DX.VGet(0,-500,100));
            float flame = 0;
            int GHandle;
            GHandle =  DX.LoadGraph("与謝野晶子.JPG");
            int ModelHandleYosano = DX.MV1LoadModel("YsanoFlameZiki.mv1");
            int ModelHandleDice = DX.MV1LoadModel("Dice6x3.mv1");
            int ModelHandleHozyo = DX.MV1LoadModel("Hozyo.mv1");
            DX.MV1SetScale(ModelHandleHozyo, DX.VGet(10,10,10));
            DX.MV1SetScale(ModelHandleYosano, DX.VGet(10, 10, 10));
            DX.VECTOR DiceRota = DX.VGet(100, 100, -10000);
            DX.MV1SetPosition(ModelHandleDice, DiceRota);
            DX.MV1SetPosition(ModelHandleYosano, DX.VGet(0, 0, -10000));
            DX.MV1SetRotationXYZ(ModelHandleYosano, DX.VGet(0, (float)1.62, 0));
            double hozyoflame = 0;
            while (true)
            {
                mouse = DX.GetMouseInput();
                DX.GetHitKeyStateAll(out keys[0]); //どのキーが入力されたか 
                DX.ClearDrawScreen();

                mybullet.update();

                if (keys[DX.KEY_INPUT_W] != 0) camera.Target.z++;
                if (keys[DX.KEY_INPUT_S] != 0) camera.Target.z--;
                if (keys[DX.KEY_INPUT_A] != 0) camera.Target.x--;
                if (keys[DX.KEY_INPUT_D] != 0) camera.Target.x++;
                if (keys[DX.KEY_INPUT_SPACE] != 0) camera.Target.y++;
                if (keys[DX.KEY_INPUT_LSHIFT] != 0) camera.Target.y--;
                if (mouse == DX.MOUSE_INPUT_LEFT) mybullet.DrawHozyo(camera.Pos);
                else mybullet.BFlag = true;
                //if (keys[DX.KEY_INPUT_LCONTROL] != 0) camera.Target.z = 0;

                //DX.SetMousePoint(320, 240);

                camera.SetCamera();
                DX.MV1DrawModel(ModelHandleDice);
                DX.MV1SetRotationXYZ(ModelHandleDice, DX.VAdd(DiceRota, DX.VGet(0, 0 + (float)hozyoflame, 0)));
                //DX.MV1DrawModel(ModelHandleHozyo);
                DX.MV1DrawModel(ModelHandleYosano);
                DX.MV1SetPosition(ModelHandleHozyo, DX.VGet(camera.Pos.x, camera.Pos.y, camera.Pos.z + 70));
                DX.MV1SetRotationXYZ(ModelHandleHozyo, DX.VGet((float)3.14, (float)1.57 + (float)hozyoflame, 0));
                hozyoflame += 0.05;


                if (keys[DX.KEY_INPUT_UP] != 0) Far += 50;
                if (keys[DX.KEY_INPUT_DOWN] != 0) Far -= 50;
                /*
                flame++;
                sin = (float)Math.Sin(flame / 2) * 200;
                for (int i2 = 0; i2 < 5;i2++)
                    for (int i = 0; i < 5; i++)
                    {
                        DX.DrawSphere3D(DX.VGet(400 - i2 * 200, 0, i * 400 + sin), 50, 250, DX.GetColor(255, 0, 0), DX.GetColor(255, 255, 255), 1);
                        DX.DrawLine3D(DX.VGet(400 - i2 * 200, 0, sin), DX.VGet(400 - i2 * 200, 0, 2000 + sin), DX.GetColor(255, 0, 0));
                    }
                */
                //XYZ軸を表示（テスト用）---------------------------------------------------
                DX.DrawLine3D(DX.VGet(-100000, 0, 0), DX.VGet(100000, 0, 0), DX.GetColor(255, 255, 255));
                DX.DrawLine3D(DX.VGet(0, -100000, 0), DX.VGet(0, 100000, 0),DX.GetColor(255,255,255));
                DX.DrawLine3D(DX.VGet(0, 0, -100000), DX.VGet(0, 0, 100000), DX.GetColor(255, 255, 255));
                //--------------------------------------------------------------------------
                DX.DrawRota2Graph3D(0, 300, 500, 0, 0, 1.5, 1, 0, GHandle, 1);
                //DX.DrawRotaGraph3D(-800, 500, 500, 1, 3.14, GrHandle, 1);

                DX.DrawCube3D(DX.VGet(-1000, -501, -11000), DX.VGet(1000, -500, 1500), DX.GetColor(50, 50, 50), DX.GetColor(255, 255, 255), 1);
                //DX.DrawCube3D(DX.VGet(-1000, -501, 1500), DX.VGet(1000, 1000, 1501), DX.GetColor(150, 150, 150), DX.GetColor(255, 255, 255), 1);
                //DrawString'S
                DX.DrawString(30, 60, Far.ToString(), DX.GetColor(255, 255, 255));
                DX.DrawString(30, 100, "X: " + camera.CPx.ToString(), DX.GetColor(255, 255, 255));
                DX.DrawString(30, 125, "Y: " + camera.CPy.ToString(), DX.GetColor(255, 255, 255));

                DX.DrawString(30, 150, "flame: " + flame.ToString(), DX.GetColor(255, 255, 255));

                DX.SetCameraNearFar(50, Far);
                DX.ScreenFlip();
                if (keys[DX.KEY_INPUT_RETURN] != 0 || keys[DX.KEY_INPUT_ESCAPE] != 0) break;
            }
            DX.DxLib_End();

        }
    }
}
