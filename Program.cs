using System;
using System.IO;
using System.Net;
using Microsoft.Data.Analysis;
namespace StockProject
{
    class Program
    {
        static void Main(string[] args)
        {
            AVConnection conn = new AVConnection("0ERONKQAHR9X5938");
            Console.WriteLine("Enter a Stock");
            
            for(int i = 0; i < 10; i++)
            {
                
                String s = Console.ReadLine();
                Console.WriteLine("Stock for " +s);
                conn.saveCSVfromURL(s);

                DataFrame df = DataFrame.LoadCsv("stockdata.csv");
                Console.WriteLine(df);
            }
                

        }
    }
    
    public class AVConnection
    {
        private readonly string _apiKey;

        public AVConnection(string apiKey)
        {
            this._apiKey = apiKey;
        }

        public void saveCSVfromURL(string symbol)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://" +$@"www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol="+symbol +"&apikey={this._apiKey}&datatype=csv");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();
            File.WriteAllText(@"C:\Users\liama\VSCodeProjects\StockProject\stockdata.csv", results);
        }

    }
}
