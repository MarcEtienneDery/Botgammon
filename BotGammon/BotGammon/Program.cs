using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
 * Vocabulaire:
 * Checkers: pion
 * Points: triangles
 * Bar: endroit où on met les checkers mangés (dans les moves, c'est le point #25)
 * ...
*/

namespace BotGammon
{
    class Program
    {
        static void Main(string[] args)
        {
			// TODO : faire un mode de test ou l'on joue 100 partie et on garde le nombre de victoire.( pour notre présentation)
			//	le fonctionement désiré serait : BotGammon --path <the path to gnubg-cli.exe> --test 
			// 
			ProcessStartInfo startInfo;
			String EXPORT_PATH;

			//check if linux
			int p = (int) Environment.OSVersion.Platform;
			if ((p == 4) || (p == 6) || (p == 128)) {
				//is linux
				startInfo = new ProcessStartInfo("gnubg", "-t");
				EXPORT_PATH = Directory.GetCurrentDirectory () + "/";
			} else {
				//is windows
				//startInfo = new ProcessStartInfo("D:\\Games\\gnubg\\gnubg-cli.exe", "-t");
                startInfo = new ProcessStartInfo(args[0], "-t");
				EXPORT_PATH = Directory.GetCurrentDirectory () + "\\";
			}

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
				string exportFile = EXPORT_PATH + "export" + coupCounter + ".txt";
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
                Grille grille = new Grille(line);
                sr.Close();
                File.Delete(exportFile);// remove the old file after being done with it.

                Move nextMove = player.GetNextMove(grille, 0);// we ask for the next move to make.

                process.StandardInput.WriteLine(nextMove.GetCmd());

                // TODO trouver comment une game fini. et changer le bool.
                gameFinished = true;
            }
			process.StandardInput.WriteLine("save game " + EXPORT_PATH + "tester.sgf");
        }
    }
}
