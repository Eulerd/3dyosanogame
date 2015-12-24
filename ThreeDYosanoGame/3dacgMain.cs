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
            //始まりの儀式
            DX.ChangeWindowMode(1);
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);//摩訶不思議!!なぜか重くなる現象回避
            DX.DxLib_Init(); //初期化
            byte[] keys = new byte[256]; //キー入力用配列
            int mouse = 0;
            //Z軸も使うよ
            DX.SetWriteZBuffer3D(1);
            DX.SetUseZBuffer3D(1);

            DX.SetBackgroundColor(100, 100, 100);
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);
            DX.ChangeLightTypeDir(DX.VGet(0, -500, 100)); //ｱｯｶﾘｰﾝの場所変更

            //クラス置いときますね
            CuttingBoard CB = new CuttingBoard();
            MyBullet mybullet = new MyBullet();
            Camera camera = new Camera(0, 0, 0, 0, 0, -10200);

            /*
            Target.x = 0;
            Target.y = 0;
            Target.z = 0;
            Pos.x = 0;
            Pos.y = 0;
            Pos.z = -800;
            */



            float Far = 3000;

            int GraphHandleYosano =  DX.LoadGraph("与謝野晶子.JPG");
            int ModelHandleYosano = DX.MV1LoadModel("YsanoFlameZiki.mv1");
            int ModelHandleDice = DX.MV1LoadModel("Dice6x3.mv1");
            int ModelHandleHozyo = DX.MV1LoadModel("Hozyo.mv1");
            DX.MV1SetScale(ModelHandleHozyo, DX.VGet(10,10,10));
            DX.MV1SetScale(ModelHandleYosano, DX.VGet(10, 10, 10));
            DX.VECTOR DiceRota = DX.VGet(100, 100, -10000);
            DX.MV1SetPosition(ModelHandleDice, DiceRota);
            DX.MV1SetPosition(ModelHandleYosano, DX.VGet(0, 0, -10000));
            DX.MV1SetRotationXYZ(ModelHandleYosano, DX.VGet(0, (float)1.62, 0));
            float flame = 0; //何かと使う
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
                //サイコロ描写
                DX.MV1DrawModel(ModelHandleDice);
                DX.MV1SetRotationXYZ(ModelHandleDice, DX.VAdd(DiceRota, DX.VGet(0, 0 + flame, 0)));

                //DX.MV1DrawModel(ModelHandleHozyo);
                DX.MV1DrawModel(ModelHandleYosano); //与謝野描写
                DX.MV1SetRotationXYZ(ModelHandleYosano, DX.VGet(0, flame, 0));
                //北条描写用
                DX.MV1SetPosition(ModelHandleHozyo, DX.VGet(camera.Pos.x, camera.Pos.y, camera.Pos.z + 70));
                DX.MV1SetRotationXYZ(ModelHandleHozyo, DX.VGet((float)3.14, (float)1.57 + flame, 0));
                flame += 0.05f;

                DX.DrawRota2Graph3D(0, 100, -10000, 0, 0, 0.5, 0.5, 0, GraphHandleYosano, 1);


                if (keys[DX.KEY_INPUT_UP] != 0) Far += 50;
                if (keys[DX.KEY_INPUT_DOWN] != 0) Far -= 50;
                //XYZ軸を表示（テスト用）---------------------------------------------------
                DX.DrawLine3D(DX.VGet(-100000, 0, 0), DX.VGet(100000, 0, 0), DX.GetColor(255, 255, 255));
                DX.DrawLine3D(DX.VGet(0, -100000, 0), DX.VGet(0, 100000, 0),DX.GetColor(255,255,255));
                DX.DrawLine3D(DX.VGet(0, 0, -100000), DX.VGet(0, 0, 100000), DX.GetColor(255, 255, 255));
                //--------------------------------------------------------------------------

                DX.DrawCube3D(DX.VGet(-1000, -501, -11000), DX.VGet(1000, -500, 1500), DX.GetColor(50, 50, 50), DX.GetColor(255, 255, 255), 1); //地面

                //DrawString'S--------------------------------------------------------------------
                DX.DrawString(30, 60, Far.ToString(), DX.GetColor(255, 255, 255));
                DX.DrawString(30, 100, "X: " + camera.CPx.ToString(), DX.GetColor(255, 255, 255));
                DX.DrawString(30, 125, "Y: " + camera.CPy.ToString(), DX.GetColor(255, 255, 255));
                DX.DrawString(30, 150, "flame: " + flame.ToString(), DX.GetColor(255, 255, 255));
                //--------------------------------------------------------------------------------


                DX.SetCameraNearFar(50, Far);
                DX.ScreenFlip();
                if (keys[DX.KEY_INPUT_RETURN] != 0 || keys[DX.KEY_INPUT_ESCAPE] != 0) break;
            }
            DX.DxLib_End();

        }
    }
}
