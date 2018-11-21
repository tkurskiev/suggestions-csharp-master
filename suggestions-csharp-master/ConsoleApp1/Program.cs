using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using suggestionscsharp;

namespace ConsoleApp1
{
    class Program
    {
        SuggestClient abc = new SuggestClient();
        static void Main(string[] args)
        {
            //var api = new CleanClient("REPLACE_WITH_YOUR_API_KEY", "REPLACE_WITH_YOUR_SECRET_KEY", "dadata.ru", "https");
            //var inputs = new string[] { "Москва Милютинский 13", "Питер Восстания 1" };
            //var cleaned = api.Clean<AddressData>(inputs);
            //foreach (AddressData entity in cleaned)
            //{
            //    Console.WriteLine(entity);
            //}

            var token = "278acbb5adacb6e1c7ba3aad25e802c2c1e9952a";
            var url = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";
            var api = new SuggestClient(token, url);
            var query = "москва серпухов";
            var response = api.QueryAddress(query);
            Console.WriteLine(string.Join("\n", response.suggestions));

            Console.ReadKey();
        }
    }
}
