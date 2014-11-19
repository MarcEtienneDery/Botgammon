using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Botgammon
{
    class Program
    {
        static void Main(string[] args)
        {

            String gnubg = "D:\\Games\\gnubg\\gnubg-cli.exe"; //TODO get from arguments

            //Start gnubg-cli
            ProcessStartInfo startInfo = new ProcessStartInfo(gnubg);
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardInput = true;
            startInfo.UseShellExecute = false;

            Process process = Process.Start(startInfo);
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.OutputDataReceived += (s, e) => Console.WriteLine(e.Data);
            process.ErrorDataReceived += (s, e) => Console.WriteLine(e.Data);

            // start a new game
            process.StandardInput.WriteLine("new game");

            int coupCounter = 0;
            IPlayer player = new Player();
            bool gameFinished = false;

            while (!gameFinished)// boucle pour chaque coup qu'on doit jouer.
            {
                // on se prépare à jouer le prochain coup.
                process.StandardInput.WriteLine("roll"); // on roll les dés.
                string exportFile = Directory.GetCurrentDirectory() + "\\export" + coupCounter + ".txt";
                coupCounter ++;
                process.StandardInput.WriteLine("export position snowie " + exportFile);

                // on load le fichier snowie
                while (!File.Exists(exportFile)) // truc guetto pour attendre l'export du fichier.
                {
                    Thread.Sleep(10);
                }

                StreamReader sr = new StreamReader(exportFile);
                   
                String line = sr.ReadToEnd();
                // TODO parsing du fichier snowie.
                Position pos = new Position(line);
                sr.Close();
                File.Delete(exportFile);// remove the old file after being done with it.

                Move nextMove = player.GetNextMove(pos);// we ask for the next move to make.

                process.StandardInput.WriteLine(nextMove.getCmd());

                // TODO trouver comment une game fini. et changer le bool.
                gameFinished = true;
            }
            process.StandardInput.WriteLine("save game " + Directory.GetCurrentDirectory() + "\\tester.sgf");
        }
    }
}
