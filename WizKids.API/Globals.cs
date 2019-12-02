using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using WizKids.API.Model;

namespace WizKids.API
{
    public class Globals
    {
        public static Globals _globals { get; set; } = new Globals();
        public static SQLiteConnection db;
        public static string dbPaths = AppDomain.CurrentDomain.BaseDirectory;
        //private static string dbPath = Path.Combine(
        //        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Dictionary.db");
        public static string dbPath = Directory.GetCurrentDirectory() + "\\Dictionary.db";

        public static string dbUrlPath = @"URI=file:C:\WinKidsDemoProject\WizKids.API\WizKids.API\Dictionary.db";
        static Globals()
        {
            db = new SQLiteConnection(dbPath);
            Console.WriteLine("Connected to DB");
        }
        public List<CustomDictionary> GetAllRows(string query)
        {
            List<CustomDictionary> list = new List<CustomDictionary>();
            using var con = new SQLiteConnection(dbUrlPath);
            con.Open();
            string stm = "SELECT * FROM Words where Value like '" + query + "%' order by value desc";
            using SQLiteCommand cmd = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            return ReturnRows(list, rdr);
        }
        private static List<CustomDictionary> ReturnRows(List<CustomDictionary> list, SQLiteDataReader rdr)
        {
            while (rdr.Read())
            {
                list.Add(new CustomDictionary { Id = rdr.GetInt32(0), Name = rdr.GetString(1) });

            }
            return list;
        }
    }


}
