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
        static int CountGame = 0;
        static int CountWin = 0;
        static String Rawboard = null;
        static bool Ready = false;
        static void Main(string[] args)
        {
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
            //process.OutputDataReceived += (s, e) => Console.WriteLine(e.Data);
            process.OutputDataReceived += (s, e) => checkForEndGame(e.Data);
            process.OutputDataReceived += (s, e) => checkForBoard(e.Data);
            process.OutputDataReceived += (s, e) => checkForRolledDice(e.Data);
            process.OutputDataReceived += (s, e) => checkForResignation(e.Data, process);
            process.ErrorDataReceived += (s, e) => Console.WriteLine(e.Data);
            process.ErrorDataReceived += (s, e) => checkForError(e.Data);

            process.StandardInput.WriteLine("set matchlength 1001");
            process.StandardInput.WriteLine("set cube use off");
            process.StandardInput.WriteLine("set output rawboard on");

            // start a new game
            process.StandardInput.WriteLine("new game");

            IPlayer player = new Player();

            while (CountGame < 100)// boucle pour chaque coup qu'on doit jouer.
            {
                // on se prépare à jouer le prochain coup.
                Ready = false;
                process.StandardInput.WriteLine("roll"); // on roll les dés.

                int counterTime = 0;
                while (!Ready)
                {
                    Thread.Sleep(5);
                    counterTime++;
                    if (counterTime > 100)
                    {
                        process.StandardInput.WriteLine("roll"); // on roll les dés.
                        counterTime = 0;
                    }
                }

                Grille grille = new Grille(Rawboard);

                Move nextMove = player.GetNextMove(grille, 1);// we ask for the next move to make.

                process.StandardInput.WriteLine(nextMove.GetCmd());
            }
			process.StandardInput.WriteLine("save match " + EXPORT_PATH + "tester.sgf");

            Console.WriteLine("********** finished : " + CountWin + " games won ******************");
            Console.ReadLine();
        }

        static void checkForEndGame(String data)
        {
            if (data.Contains("wins a single game and "))
            {
                CountGame++;
                Console.WriteLine(CountGame);
                if (!data.Contains("gnubg"))
                {
                    CountWin++;
                    Console.WriteLine("WIN OMG");
                }
            }
        }

        static void checkForBoard(String data)
        {
            if (data.Contains("board:"))
            {
                Rawboard = data;
            }
        }

        static void checkForRolledDice(String data)
        {
            if (data.Contains("You have already rolled the dice"))
            {
                Ready = true;
            }
        }

        static void checkForError(String data)
        {
            if (data.Contains("Illegal"))
            {
                Console.WriteLine(Rawboard);
            }
        }

        static void checkForResignation(String data, Process process)
        {
            if (data.Contains("offers to resign"))
            {
                process.StandardInput.WriteLine("accept");
            }
        }
    }
}
