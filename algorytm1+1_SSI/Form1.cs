using System.Drawing.Drawing2D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace algorytm1_1_SSI
{
    public partial class Form1 : Form
    {
        //public double x;
        //public double y;
        public int minZakres = 0;
        public int maxZakres = 100;
        public double rozrzut = 10;
        public int l_iteracji = 100;
        public double wsp_przyrostu = 1.1;
        //public List<(double x,double y)> punktyIteracji = new List<(double x, double y)> ();
        public List<Punkt> punktyIteracji = new List<Punkt>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PetlaGlowna();
            RysujWykres();
        }
        public double Wartosc(double x)
        {
            return Math.Sin(x/10.0)*Math.Sin(x/200.0);
        } 
        public double Losuj(int minPrzedzialu, int maxPrzedzialu)
        {
            Random rand = new Random();
            return rand.Next(minPrzedzialu, maxPrzedzialu);
        }
        public double LosujRozrzut(double minPrzedzialu, double maxPrzedzialu)
        {
            Random rand = new Random();
            return minPrzedzialu + rand.NextDouble() * (maxPrzedzialu - minPrzedzialu);
        }

        public void PetlaGlowna()
        {
            Punkt punkcik = new Punkt();
            punkcik.x = Losuj(minZakres, maxZakres);
            punkcik.y = Wartosc(punkcik.x);
            punktyIteracji.Add(new Punkt {x = punkcik.x, y = punkcik.y }); // musi być dodwany nowy punkt żeby każdy element listy nie wskazywał na jedno miejsce w pamięci
            for(int i = 0; i<l_iteracji; i++)
            {
                double losowa = LosujRozrzut(-rozrzut, rozrzut);
                double x_pot = punkcik.x + losowa;
                if(x_pot > maxZakres)
                {
                    x_pot = maxZakres;
                }
                if (x_pot < minZakres)
                {
                    x_pot = minZakres;
                }
                double y_pot = Wartosc(x_pot);
                if (y_pot >= punkcik.y)
                {
                    punkcik.x = x_pot;
                    punkcik.y = y_pot;
                    rozrzut *= wsp_przyrostu;
                }
                else if (y_pot < punkcik.y)
                {
                    rozrzut = (rozrzut/wsp_przyrostu);
                }
                punktyIteracji.Add(new Punkt { x = punkcik.x, y = punkcik.y });
                WypiszDane(punkcik.x, punkcik.y, i, rozrzut);
            }
        }
        public void WypiszDane(double x, double y, int iteracja, double rozrzut)
        {
            RTB1.AppendText( "Iteracja nr: "+ iteracja + "\n");
            RTB1.AppendText("X =  " + x.ToString("F2") + "\n");
            RTB1.AppendText("Y =  " + y.ToString("F2") + "\n");
            RTB1.AppendText("Rozrzut =  " + rozrzut.ToString("F2") + "\n\n");
        }


        private void RysujWykres()
        {
            if (PB1.Width <= 0 || PB1.Height <= 0) return;

            Bitmap bmp = new Bitmap(PB1.Width, PB1.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);

                int margines = 35;
                int szer = PB1.Width - 2 * margines;
                int wys = PB1.Height - 2 * margines;

                double minY = double.MaxValue;
                double maxY = double.MinValue;
                for (double px = minZakres; px <= maxZakres; px += 0.5)
                {
                    double val = Wartosc(px);
                    if (val < minY) minY = val;
                    if (val > maxY) maxY = val;
                }
                double zapasY = (maxY - minY) * 0.1;
                minY -= zapasY;
                maxY += zapasY;

                float MapX(double valX) => margines + (float)((valX - minZakres) / (maxZakres - minZakres) * szer);
                float MapY(double valY) => PB1.Height - margines - (float)((valY - minY) / (maxY - minY) * wys);

                // Osie układu współrzędnych
                using (Pen axisPen = new Pen(Color.Gray, 1))
                {
                    float yZero = MapY(0);
                    if (yZero >= margines && yZero <= PB1.Height - margines)
                        g.DrawLine(axisPen, margines, yZero, PB1.Width - margines, yZero); // oś X (y=0)

                    g.DrawLine(axisPen, margines, margines, margines, PB1.Height - margines); // oś Y
                }

                using (Font font = new Font("Segoe UI", 8))
                using (Brush textBrush = new SolidBrush(Color.Black))
                {
                    g.DrawString(minZakres.ToString(), font, textBrush, margines - 10, PB1.Height - margines + 5);
                    g.DrawString(maxZakres.ToString(), font, textBrush, PB1.Width - margines - 15, PB1.Height - margines + 5);
                    g.DrawString(maxY.ToString("F2"), font, textBrush, 2, margines - 5);
                    g.DrawString(minY.ToString("F2"), font, textBrush, 2, PB1.Height - margines - 10);
                }

                List<PointF> punktyFunkcji = new List<PointF>();
                for (double px = minZakres; px <= maxZakres; px += 0.5)
                {
                    punktyFunkcji.Add(new PointF(MapX(px), MapY(Wartosc(px))));
                }
                using (Pen funcPen = new Pen(Color.RoyalBlue, 2))
                {
                    g.DrawLines(funcPen, punktyFunkcji.ToArray());
                }

                using (Brush pointBrush = new SolidBrush(Color.FromArgb(160, Color.OrangeRed)))
                {
                    for (int i = 0; i < punktyIteracji.Count - 1; i++)
                    {
                        float cx = MapX(punktyIteracji[i].x);
                        float cy = MapY(punktyIteracji[i].y);
                        g.FillEllipse(pointBrush, cx - 3, cy - 3, 6, 6);
                    }
                }

                if (punktyIteracji.Count > 0)
                {
                    var ostatni = punktyIteracji[punktyIteracji.Count - 1];
                    float cx = MapX(ostatni.x);
                    float cy = MapY(ostatni.y);
                    using (Brush bestBrush = new SolidBrush(Color.LimeGreen))
                    using (Pen bestPen = new Pen(Color.DarkGreen, 1.5f))
                    {
                        g.FillEllipse(bestBrush, cx - 5, cy - 5, 10, 10);
                        g.DrawEllipse(bestPen, cx - 5, cy - 5, 10, 10);
                    }
                }
            }

            PB1.Image?.Dispose();
            PB1.Image = bmp;
        }
    }
}
