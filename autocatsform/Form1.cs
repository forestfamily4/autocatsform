using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace autocatsform
{
    public partial class Form1 : Form
    {
        private AutoCat a;
        private static Label label1__;
        private static string logtext_="";
        public static bool b2022nenn=false;
        public static string logtext
        {
            get
            {
                try
                {
                    label1__.Text = logtext_;
                }
                catch (Exception e) { Debug.WriteLine(e); }
                return logtext_;
            }
            set
            {
                try
                {
                    label1__.Text = logtext_;
                }
                catch (Exception e) { Debug.WriteLine(e); }
                logtext_ = value;
            }
        }
        public Form1()
        {
            InitializeComponent();
        }
        public void thinking()
        {
            
            var path = Application.UserAppDataPath + @"\data";
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path);
                richTextBox1.Text = sr.ReadToEnd();
                sr.Close();
            }
            else
            {
                File.Create(path).Close();
            }
            
            


            this.TopMost = true;
            Form1.label1__ = label1;
            label1.Text = "thinking";
            a =new AutoCat();
            AutoCat.Instance = a;
            a.log($"data�t�@�C��:{path}");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = richTextBox1.Text;
            var b=a.Split("\n");
            for(int i= 0;i<b.Length;i++)
            {
                var d = b[i].IndexOf("=");
                b[i] = b[i].Substring(d+1);
            }
            List<MouseEventControl> ints = new List<MouseEventControl>();

            foreach(var f in b)
            {
                var g=f.Split(",");
                ints.Add(new MouseEventControl(int.Parse(g[0]), int.Parse(g[1])));
            }
            AutoCat.Instance.Main_(ints);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            var path = Application.UserAppDataPath + @"\data";
            StreamWriter sw=new StreamWriter(path);
            sw.Write(richTextBox1.Text);
            sw.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Form1.b2022nenn=checkBox1.Checked;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //���샋�[�v�t�@�C��
            var path = Application.UserAppDataPath + @"\loopfile.txt";
            if (!File.Exists(path))
            {
                File.Create(path).Close();
                var templatetxt =
@"
wait 3000;
push 0;
��0�Ԗڂ̂��̂��N���b�N���܂� wait�̓~���b(1000=1�b)�ł�;
loop 3;
    wait 2000;
    push 1;
    loop 3;
        wait 1000;
    };      
};
�����[�v�����ł� ;

";
                File.WriteAllText(path, templatetxt);
            }
            System.Diagnostics.Process.Start("EXPLORER.EXE", path);

        }
    }


    public class AutoCat
    {
        public static AutoCat Instance { get;set; }
        static List<MouseEventControl> m = new List<MouseEventControl>(){
        new MouseEventControl(1423,751),//�퓬�J�n
        new MouseEventControl(1735,98),//�E��̒n�}
        new MouseEventControl(200,950),//�������x��

        new MouseEventControl(587,761),//�X���b�g1 3
        new MouseEventControl(793,760),//�X���b�g2
        new MouseEventControl(992,760),//�X���b�g3
        new MouseEventControl(1196,760),//�X���b�g4 //ok���N���b�N�ł��܂�
        new MouseEventControl(1391,760),//�X���b�g5
        new MouseEventControl(587,878),//�X���b�g1
        new MouseEventControl(793,878),//�X���b�g2
        new MouseEventControl(992,878),//�X���b�g3
        new MouseEventControl(1196,878),//�X���b�g4
        new MouseEventControl(1391,878),//�X���b�g5
    };

        public async Task Main_(List<MouseEventControl> mec)
        {
            if (Form1.b2022nenn)
            {
                log("�n�܂�");
                while (true)
                {
                    await Task.Delay(3000);
                    var t = DateTime.Now;
                    log($"������&��������{t.ToLongTimeString()}");
                    if (t.Hour == 0 || t.Hour == 1)
                    {
                        log("0���Ȃ̂ŕ�V���󂯎���Ă�������");
                        break;
                    }
                    Loop();
                    await Task.Delay(1000 * 60 * 100);//100��
                }
            }

            //new MouseEventControl(705,1058).Run();
            AutoCat.m = mec;
        }

        async void Loop()
        {
            //�퓬�J�n
            m[0].Run();
            await Fighting();
            await d(0.5f);
            m[1].Run();//�n�}
            await d(0.5f);
            m[1].Run();//�n�}
            await d(0.5f);
            m[1].Run();//�n�}
            log("100���ҋ@���܂�");

        }



        async Task ExecLoopFile()
        {
            var path = Application.UserAppDataPath + @"\loopfile.txt";
            var text = File.ReadAllText(path);
            text = text.ToLower();
            var a = text.Split(";");
            int i = 0;
            while(i<a.Length)
            {
                var b = a[i];
                if (!(b.StartsWith("wait") || b.StartsWith("loop") || b.StartsWith("push"))) { continue; }
                if (b.StartsWith("wait"))
                {
                    await Task.Delay(int.Parse(b.Substring(5).Replace(" ","")));
                }
                else if (b.StartsWith("push"))
                {
                    m[int.Parse(b.Substring(5).Replace(" ", ""))].Run();
                }
                else
                {
                    //int num = a
                }
            }
        }

        async Task ExecLoopTask(AnalyzeResult aa)
        {
            if (aa.processType == ProcessType.Wait)
            {
                await Task.Delay(int.Parse(aa.data));
            }
            else if (aa.processType == ProcessType.Push)
            {
                m[int.Parse(aa.data)].Run();
            }
            else
            {
                int num=a
            }
        }

        private void Error(string s)
        {
            MessageBox.Show(s);
        }


        public struct AnalyzeResult
        {
            public ProcessType processType;
            public string data;
        }
        public enum ProcessType { Wait,Loop,Push}

        async Task Fighting()
        {//�퓬��
            await d(4);//�퓬�܂ł̈Ó]

            await d(30);//�݂��܂����܂őҋ@
            m[3].Run();//�݂���
            //���̑����A��
            List<int> l =new List<int>() { 3, 4, 5, 6, 7, 8, 9, 10 };
            for (int i = 0; i < 700; i++)
            {
                //3�����
                m[l[i%l.Count]].Run();
                await d(0.2f);
            }            
        }

        async Task d(float seconds)
        {
            int a = (int)Math.Floor(seconds * 1000);
            await Task.Delay(a);
        }

        public void log(object o)
        {
            Debug.WriteLine(o);
            Form1.logtext+=("\n" + o);
        }
    }

    public class MouseEventControl
    {
        public Point p { get; private set; }
        public MouseEventControl(Point p)
        {
            this.p = p;
        }
        public MouseEventControl(int X, int Y)
        {
            this.p = new Point(X, Y);
        }
        public async void Run()
        {
            MouseOperations.SetCursorPosition(p.X, p.Y);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            await d(0.1f);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            AutoCat.Instance.log($"�N���b�N:{p.X},{p.Y}");
            AutoCat.Instance.log($"�N���b�N:{p.X},{p.Y}");
        }

        static async Task d(float seconds)
        {
            int a = (int)Math.Floor(seconds * 1000);
            await Task.Delay(a);
        }
    }



    public class MouseOperations
    {
        [Flags]
        public enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010
        }

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static void SetCursorPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public static void SetCursorPosition(MousePoint point)
        {
            SetCursorPos(point.X, point.Y);
        }

        public static MousePoint GetCursorPosition()
        {
            MousePoint currentMousePoint;
            var gotPoint = GetCursorPos(out currentMousePoint);
            if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
            return currentMousePoint;
        }

        public static void MouseEvent(MouseEventFlags value)
        {
            MousePoint position = GetCursorPosition();

            mouse_event
                ((int)value,
                 position.X,
                 position.Y,
                 0,
                 0)
                ;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MousePoint
        {
            public int X;
            public int Y;

            public MousePoint(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
