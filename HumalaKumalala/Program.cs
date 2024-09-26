using System;
using System.Reflection.Metadata.Ecma335;
namespace HumalaKumalala
{
    public class CProgram
    {
        const int A = 0;
        const int B = 1;
        const int C = 2;
        static int[] arreiPazzurdo;
        static bool onlyOnce = false;
        static int p = 1;

        static void Main()
        {
            // 1 -> 3 = ( 1 * 2 ) + 1 -> 7 = ((( 1 * 2 ) * 2 ) * 2) - 1 -> 15 -> 31 -> 63
            // 1    2    3    4     5     6
            arreiPazzurdo = new int[3];
            Console.WriteLine("Inizio");
            risolvi(10);

            /*
             [1] [ ] [ ]
             [2] [ ] [ ]
             [4] [ ] [ ]
             */
        }

        static void risolvi(int n)
        {
            if (!onlyOnce)
            {
                arreiPazzurdo[0] = (int)Math.Pow(2, n) - 1;
                onlyOnce = true;
            }

            //parte iterativa

            int posDiscoPiccolo = 0;

            for (int i = 0; i < 3; i++) 
            {
                if (arreiPazzurdo[i] % 2 == 1)
                    posDiscoPiccolo = i;
            }
            
            if (n % 2 == 0) 
            {
                switch (p) 
                {
                    case 1: //muove il disco più piccolo a dx
                        calcolaMossa(posDiscoPiccolo, posDiscoPiccolo + 1);
                        break;
                    case 2:
                        calcolaMossa(posDiscoPiccolo + 1, posDiscoPiccolo - 1);
                        break;
                }
            }
            else 
            {
                switch (p)
                {
                    case 1: //muove il disco più piccolo a sx
                        calcolaMossa(posDiscoPiccolo, posDiscoPiccolo - 1);
                        break;
                    case 2:
                        calcolaMossa(posDiscoPiccolo + 1, posDiscoPiccolo - 1);
                        break;
                }
            }

            printArray(n);

            if (arreiPazzurdo[2] == (int)Math.Pow(2, n) - 1)
                return;

            if (p == 2)
                p = 0;

            p++;
            risolvi(n);
        }

        static void calcolaMossa(int p, int f) 
        {
            //si pensi all'array come se le due estremità fossero connesse
            if (f > 2) 
                f = 0;
            else if (f < 0) 
                f = 2;
            if (p > 2)
                p = 0;
            else if (p < 0)
                p = 2;

            int vPezzo = arreiPazzurdo[p];
            int vPezzof = arreiPazzurdo[f];
            int esponente;

            if (vPezzo != 0) //se la partenza è colonna vuota la mossa inversa è garantita
            {
                esponente = (int)Math.Truncate(Math.Log2(arreiPazzurdo[p]));

                while ((int)Math.Pow(2, esponente) != vPezzo) //calcolo del valore del primo pezzo
                {
                    if ((int)Math.Pow(2, esponente) <= vPezzo)
                    {
                        vPezzo -= (int)Math.Pow(2, esponente);
                    }
                    esponente--;
                }

                if (vPezzof != 0) 
                {
                    esponente = (int)Math.Truncate(Math.Log2(arreiPazzurdo[f]));

                    while ((int)Math.Pow(2, esponente) != vPezzof) //calcolo del valore del secondo pezzo
                    {
                        if ((int)Math.Pow(2, esponente) <= vPezzof)
                        {
                            vPezzof -= (int)Math.Pow(2, esponente);
                        }
                        esponente--;
                    }
                }

                if ((vPezzo < vPezzof || vPezzof == 0))
                {
                    arreiPazzurdo[p] -= vPezzo;
                    arreiPazzurdo[f] += vPezzo;
                    printMossa(p, f);
                    return;
                }
            }

            vPezzo = arreiPazzurdo[f];
            esponente = (int)Math.Truncate(Math.Log2(arreiPazzurdo[f]));

            while ((int)Math.Pow(2, esponente) != vPezzo) //calcolo del valore del primo pezzo
            {
                if ((int)Math.Pow(2, esponente) <= vPezzo)
                {
                    vPezzo -= (int)Math.Pow(2, esponente);
                }
                esponente--;
            }
            arreiPazzurdo[f] -= vPezzo;
            arreiPazzurdo[p] += vPezzo;
            printMossa(f, p);
        }

        static void printArray(int n) 
        {
            //string toPrint;
            for (int i = 0; i < 3; i++) 
            {
                Console.Write(arreiPazzurdo[i].ToString() + " ");
                /*for (int j = 0; j < n; j++)
                int vPezzo = arreiPazzurdo[i];
                int daDividere = (int)Math.Truncate(Math.Log2(arreiPazzurdo[i]));

                while ((int)Math.Pow(2, daDividere) != vPezzo)
                {
                    vPezzo -= (int)Math.Pow(2, daDividere);
                    daDividere--;
                }*/ //parte grafica idk :\
            }
            Console.WriteLine();
        }

        static void printMossa(int p, int f) 
        {
            char lettera1 = 'E', lettera2  = 'E';

            switch (p) 
            {
                case 0:
                    lettera1 = 'A';
                    break;
                case 1:
                    lettera1 = 'B';
                    break;
                case 2:
                    lettera1 = 'C';
                    break;
            }

            switch (f)
            {
                case 0:
                    lettera2 = 'A';
                    break;
                case 1:
                    lettera2 = 'B';
                    break;
                case 2:
                    lettera2 = 'C';
                    break;
            }
            try 
            {
                Console.WriteLine($"Mossa accuratamente calcolata: {lettera1} - {lettera2}");
            }
            catch
            { 
                Console.WriteLine("Overflow!"); 
            }
        }
    }
}