using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace autocatsform
{
    public partial class Form1 : Form
    {
        private AutoCat a;
        private static RichTextBox label1__;
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
            this.TopMost = true;
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
            Form1.label1__ = this.richTextBox2;
            a =new AutoCat();
            AutoCat.Instance = a;
            a.log($"dataファイル:{path}");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.TopMost = true;
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
            //操作ループファイル
            var path = Application.UserAppDataPath + @"\loopfile.txt";
            if (!File.Exists(path))
            {
                File.Create(path).Close();
                var templatetxt =
@"
wait 3000
push 0
↑0番目のものをクリックします waitはミリ秒(1000=1秒)です
loop 3{
    wait 2000
    push 1
    loop 3{
        wait 1000
        log test
    }     
}
↑ループ処理です ちなみに{はいらないです

";
                File.WriteAllText(path, templatetxt);
            }
            System.Diagnostics.Process.Start("EXPLORER.EXE", path);

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }


    public class AutoCat
    {
        public static AutoCat Instance { get;set; }
        static List<MouseEventControl> m = new List<MouseEventControl>(){
        new MouseEventControl(1423,751),//戦闘開始
        new MouseEventControl(1735,98),//右上の地図
        new MouseEventControl(200,950),//お金レベル

        new MouseEventControl(587,761),//スロット1 3
        new MouseEventControl(793,760),//スロット2
        new MouseEventControl(992,760),//スロット3
        new MouseEventControl(1196,760),//スロット4 //okもクリックできます
        new MouseEventControl(1391,760),//スロット5
        new MouseEventControl(587,878),//スロット1
        new MouseEventControl(793,878),//スロット2
        new MouseEventControl(992,878),//スロット3
        new MouseEventControl(1196,878),//スロット4
        new MouseEventControl(1391,878),//スロット5
    };

        public async Task Main_(List<MouseEventControl> mec)
        {
            AutoCat.m = mec;
            if (Form1.b2022nenn)
            {
                log("始まり");
                while (true)
                {
                    await Task.Delay(3000);
                    var t = DateTime.Now;
                    log($"初期化&ただいま{t.ToLongTimeString()}");
                    if (t.Hour == 0 || t.Hour == 1)
                    {
                        log("0時なので報酬を受け取ってください");
                        break;
                    }
                    Loop();
                    await Task.Delay(1000 * 60 * 100);//100分
                }
            }
            else
            {
                ExecLoopFile();
            }
            
        }

        async void Loop()
        {
            //戦闘開始
            m[0].Run();
            await Fighting();
            await d(0.5f);
            m[1].Run();//地図
            await d(0.5f);
            m[1].Run();//地図
            await d(0.5f);
            m[1].Run();//地図
            log("100分待機します");

        }



        async Task ExecLoopFile()
        {
            var path = Application.UserAppDataPath + @"\loopfile.txt";
            var text = File.ReadAllText(path);
            text = text.ToLower();
            await ExecLoopTask(text);            
        }


        async Task ExecLoopTask(string text)
        {
            var aaa = text.Split("\n");
            List<string> a = new List<string>();
            a.AddRange(aaa);
            a.Select(a =>  a = DeleteFormerSpace(a) );
            int i = 0;
            while (i < a.Count)
            {
                var b = a[i].Trim();
                if (b.StartsWith("wait"))
                {
                    var c = b.Substring(5).Replace(" ", "");
                    int dd=0;
                    if(!int.TryParse(c, out dd)) { Error($"intが不適です\n{a[i ] ?? ""} <--- HERE "); break; }
                    log($"待つ:{dd}");
                    await Task.Delay(dd);
                }
                else if (b.StartsWith("push"))
                {
                    var c = b.Substring(5).Replace(" ", "");
                    int dd = 0;
                    if (!int.TryParse(c, out dd)) { Error($"intが不適です\n{a[i] ?? ""} <--- HERE");i++; break; }
                    await m[dd].Run();
                }
                else if (b.StartsWith("log"))
                {
                    var c = DeleteFormerSpace(b);
                    log(c.Substring(3));
                }
                else if(b.StartsWith("loop"))
                {
                    int startCount = i;
                    int endCount = 0;
                    
                    int loopCount= 1;
                    int.TryParse(b.Replace("loop", "").Trim().Trim('{'), out loopCount);
                    log(b.Replace("loop", "").Trim().Trim('{'));

                    int num = 0;
                    int loopBUNcount = 1;
                    int KAKKOcount = 0;
                    while (true)
                    {
                        num++;
                        var t = a[startCount+num];
                        if (t.Trim().StartsWith("loop")) {
                            loopBUNcount++;
                        }
                        else if (t.Contains("}"))
                        {
                            KAKKOcount++;
                        }
                        if (loopBUNcount == KAKKOcount) {
                            endCount = i + num;
                            break; 
                        }
                    }

                    i=endCount;

                    if(endCount == startCount + 1)
                    {
                        continue;
                    }

                    string result = "";
                    for (int j = startCount+1; j <= endCount -1; j++)
                    {
                        result+= (a[j]+"\n");
                    }
                    for( int j = 0; j < loopCount; j++)
                    {

                        await this.ExecLoopTask(result);
                    }
                }
                i++;
            }
        }

        string DeleteFormerSpace(string s)
        {
            int i = 0;
            while (i < s.Length)
            {
                if(s[i] == ' ')
                {
                    i++;
                }
                else
                {
                    return s.Substring(i);
                }
            }
            return "";
        }

        private void Error(string s)
        {
            MessageBox.Show(s);
        }


        public enum ProcessType { Wait,Loop,Push}

        async Task Fighting()
        {//戦闘中
            await d(4);//戦闘までの暗転

            await d(30);//みたまだすまで待機
            m[3].Run();//みたま
            //その他も連打
            List<int> l =new List<int>() { 3, 4, 5, 6, 7, 8, 9, 10 };
            for (int i = 0; i < 700; i++)
            {
                //3が一つ目
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
        public async Task Run()
        {
            MouseOperations.SetCursorPosition(p.X, p.Y);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            await d(0.1f);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            AutoCat.Instance.log($"クリック:{p.X},{p.Y}");
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
