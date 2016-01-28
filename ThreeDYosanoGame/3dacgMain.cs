using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DxLibDLL;
using System.Windows.Forms;

namespace ThreeDYosanoGame
{
    class _3dacgMain
    {
        public static void Main(string[] args)
        {
            //始まりの儀式
            DX.ChangeWindowMode(1);
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);//摩訶不思議!!なぜか重くなる現象回避
            DX.DxLib_Init(); //初期化
            byte[] keys = new byte[256]; //キー入力用配列
            int mouse = 0;
            DX.SetMousePoint(320, 240);
            //Z軸も使うよ
            DX.SetWriteZBuffer3D(1);
            DX.SetUseZBuffer3D(1);

            DX.SetBackgroundColor(100, 100, 100);
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);
            DX.ChangeLightTypeDir(DX.VGet(0, -500, 100)); //ｱｯｶﾘｰﾝの場所変更
            //クラス置いときますね
            CuttingBoard CB = new CuttingBoard();
            MyBullet mybullet = new MyBullet();
            Camera camera = new Camera(0, 0, -10200, 0, 0, -10100);
            TDModel tdmodel = new TDModel();

            /*
            Target.x = 0;
            Target.y = 0;
            Target.z = 0;
            Pos.x = 0;
            Pos.y = 0;
            Pos.z = -800;
            */



            float Far = 3000;
            int GHandle;
            GHandle = DX.LoadGraph("与謝野晶子.JPG");

            tdmodel.SetPos(3, 0, -50, -10000);
            tdmodel.SetPos(1, 50, -50, -10000);
            tdmodel.SetPos(0, -50, -50, -10000);
            tdmodel.SetPos(4, 0, 0, -10000);
            DX.MV1SetRotationXYZ(tdmodel.Handle[0], DX.VGet(0, (float)1.62, 0));
            double flame = 0;//何かと使う

            while (true)
            {
                mouse = DX.GetMouseInput();
                DX.GetHitKeyStateAll(out keys[0]); //どのキーが入力されたか 
                DX.ClearDrawScreen();

                DX.DrawCube3D(DX.VGet(-1000, -501, -11000), DX.VGet(1000, -500, 1500), DX.GetColor(50, 50, 50), DX.GetColor(255, 255, 255), 1); //地面


                mybullet.update();
                //camera.Move = DX.VGet(0, 0, 0);
                if (keys[DX.KEY_INPUT_W] != 0)
                {
                    camera.Pos.z++;
                    camera.Target.z++;
                }
                if (keys[DX.KEY_INPUT_S] != 0)
                {
                    camera.Pos.z--;
                    camera.Target.z--;
                }
                if (keys[DX.KEY_INPUT_A] != 0)
                {
                    camera.Pos.x--;
                    camera.Target.x--;
                }
                if (keys[DX.KEY_INPUT_D] != 0)
                {
                    camera.Pos.x++;
                    camera.Target.x++;
                }
                if (keys[DX.KEY_INPUT_SPACE] != 0)
                {
                    camera.Pos.y++;
                    camera.Target.y++;
                }
                if (keys[DX.KEY_INPUT_LSHIFT] != 0)
                {
                    camera.Pos.y--;
                    camera.Target.y--;
                }
                if (keys[DX.KEY_INPUT_UP] != 0) camera.Target.z++;
                if (keys[DX.KEY_INPUT_DOWN] != 0) camera.Target.z--;
                if (mouse == DX.MOUSE_INPUT_LEFT) mybullet.DrawHozyo(camera.Target);
                else mybullet.BFlag = true;

                if (keys[DX.KEY_INPUT_LEFT] != 0) camera.Roll -= (float)Math.PI / 60;
                if (keys[DX.KEY_INPUT_RIGHT] != 0) camera.Roll += (float)Math.PI / 60;

                //if (keys[DX.KEY_INPUT_LCONTROL] != 0) camera.Target.z = 0;

                //Class
                Mouse.mousePos();
                camera.SetCamera();
                tdmodel.Draw();

                DX.MV1SetRotationXYZ(tdmodel.Handle[1], DX.VGet((float)flame, (float)flame * 2, 0));
                DX.MV1SetRotationXYZ(tdmodel.Handle[4], DX.VGet((float)flame, (float)flame * 2, 0));
                DX.MV1SetRotationXYZ(tdmodel.Handle[3], DX.VGet((float)flame, (float)flame * 2, 0));
                DX.MV1SetPosition(tdmodel.Handle[2], camera.Target);
                DX.DrawLine3D(camera.Pos, DX.VGet(camera.Pos.x, camera.Pos.y, camera.Pos.z + 70), 0x000000);
                DX.MV1SetRotationXYZ(tdmodel.Handle[2], DX.VGet((float)flame, (float)1.57, 0));
                flame += 0.05f;


                //XYZ軸を表示（テスト用）---------------------------------------------------
                DX.DrawLine3D(DX.VGet(-100000, 0, 0), DX.VGet(100000, 0, 0), DX.GetColor(255, 255, 255));
                DX.DrawLine3D(DX.VGet(0, -100000, 0), DX.VGet(0, 100000, 0),DX.GetColor(255,255,255));
                DX.DrawLine3D(DX.VGet(0, 0, -100000), DX.VGet(0, 0, 100000), DX.GetColor(255, 255, 255));
                //--------------------------------------------------------------------------


                //DrawString'S--------------------------------------------------------------------
                DX.DrawString(30, 60, Far.ToString(), DX.GetColor(255, 255, 255));
                DX.DrawString(30, 100, "PosX: " + camera.Pos.x.ToString(), DX.GetColor(255, 255, 255));
                DX.DrawString(30, 125, "PosY: " + camera.Pos.y.ToString(), DX.GetColor(255, 255, 255));
                DX.DrawString(30, 150, "PosZ: " + camera.Pos.z.ToString(), DX.GetColor(255, 255, 255));
                //--------------------------------------------------------------------------------


                DX.SetCameraNearFar(50, Far);
                DX.ScreenFlip();
                if (keys[DX.KEY_INPUT_RETURN] != 0 || keys[DX.KEY_INPUT_ESCAPE] != 0) break;
            }
            DX.DxLib_End();

        }
    }
}
