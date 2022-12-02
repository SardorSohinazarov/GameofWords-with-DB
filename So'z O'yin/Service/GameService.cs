using So_z_O_yin.Broker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So_z_O_yin.Service;

public class GameService
{
    public static async Task Game()
    {
        char currentwaiting = ' ';
        for (int i = 0; i < 1000; i++)
        {
            Console.Write("So'z kiriting:");
            string word = Console.ReadLine();
            List<string> list = await DBBroker.ListOfUsedWords(word);

            if (i > 0)
            {
                if (list.Contains(word))
                {
                    Console.WriteLine($"Uzur siz yutqazdingiz\nSababi : '{word}' oldin ham ishlatilgandi ");
                    await DBBroker.CleanDBFromWords();
                    return;
                }
                else if (word.StartsWith(currentwaiting))
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"Uzur siz yutqazdingiz\nSababi : kiritgan so'zingiz '{word}'\nAslida esa '{currentwaiting}' harfi bilan boshlanish kerak edi!");
                    await DBBroker.CleanDBFromWords();
                    return;
                }
            }
            await DBBroker.InsertToExtraDBUsedWords(word);
            string oneword = await DBBroker.OneWord(word);
            await DBBroker.InsertToExtraDBUsedWords(oneword);
            Console.WriteLine($">>>{oneword}");
            currentwaiting = oneword[oneword.Length - 1];
            if(currentwaiting == '\'')
            {
                currentwaiting = oneword[oneword.Length - 2];
            }
            Console.WriteLine($"Sizning so'zingiz '{currentwaiting}' harfi bilan boshlansin!");
        }
    }
}