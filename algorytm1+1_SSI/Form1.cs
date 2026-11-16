using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace algorytm1_1_SSI
{
    public partial class Form1 : Form
    {
        public double x;
        public double y;
        public int minZakres = 0;
        public int maxZakres = 100;
        public double rozrzut = 10;
        public int l_iteracji = 100;
        public double wsp_przyrostu = 1.1;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PetlaGlowna();
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
            x = Losuj(minZakres, maxZakres);
            y = Wartosc(x);
            for(int i = 0; i<l_iteracji; i++)
            {
                double losowa = LosujRozrzut(-rozrzut, rozrzut);
                double x_pot = x + losowa;
                if(x_pot > maxZakres)
                {
                    x_pot = maxZakres;
                }
                if (x_pot < minZakres)
                {
                    x_pot = minZakres;
                }
                double y_pot = Wartosc(x_pot);
                if (y_pot >= y)
                {
                    x = x_pot;
                    y = y_pot;
                    rozrzut *= wsp_przyrostu;
                }
                else if (y_pot < y)
                {
                    rozrzut = (rozrzut/wsp_przyrostu);
                }
                WypiszDane(x, y, i, rozrzut);
            }
        }
        public void WypiszDane(double x, double y, int iteracja, double rozrzut)
        {
            RTB1.AppendText( "Iteracja nr: "+ iteracja + "\n");
            RTB1.AppendText("X =  " + x.ToString("F2") + "\n");
            RTB1.AppendText("Y =  " + y.ToString("F2") + "\n");
            RTB1.AppendText("Rozrzut =  " + rozrzut.ToString("F2") + "\n\n");
        }
    }
}
