using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Casinomania
{
    internal class CasinoCommons
    {
        //CITATION: used Claude - Loads the user coins score
        public static int GetCoins(string userName, string gameName)
        {
            string fileName = $"{userName}.casino";

            try
            {
                string[] lines = File.ReadAllLines(fileName);

                foreach (string line in lines)
                {
                    string[] columns = line.Split(',');

                    if (columns.Length == 3 && columns[0].Trim() == gameName)
                    {
                        string coinsStr = columns[2].Trim();

                        if (int.TryParse(coinsStr, out int coins) && coins >= 0)
                        {
                            return coins;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        //CITATION: used Claude
        public static void SetCoins(string userName, string gameName, int coins)
        {
            if (userName.Trim() != "")
            {
                string fileName = $"{userName}.casino";

                try
                {
                    string[] lines = File.Exists(fileName) ? File.ReadAllLines(fileName) : new string[0];

                    bool rowExists = false;
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] columns = lines[i].Split(',');

                        if (columns.Length == 3 && columns[0].Trim() == gameName)
                        {
                            lines[i] = $"{gameName},{columns[1]},{coins}";
                            rowExists = true;
                            break;
                        }
                    }

                    if (!rowExists)
                    {
                        string newLine = $"{gameName},,{coins}";
                        lines = lines.Concat(new[] { newLine }).ToArray();
                    }

                    File.WriteAllLines(fileName, lines);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error in SetCoins method!");
                }
            }
        }

        //CITATION: used Claude
        public static string GetAllCoins(string gameName)
        {
            StringBuilder result = new StringBuilder();

            try
            {
                string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.casino");

                foreach (string file in files)
                {
                    string userName = Path.GetFileNameWithoutExtension(file);
                    int coins = GetCoins(userName, gameName);

                    if (coins != -1)
                    {
                        result.AppendLine($"{userName} - {gameName} - {coins}");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error in GetAllCoins method!");
            }

            return result.ToString();
        }
    }
}