using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Games
{
    class Program
    {
        struct GameStatus
        {
            public string name;
            public string platform;
            public Int16 year;
            public string genre;
            public string publisher;
            public double na, eu, jp, ot, gl;
        }
        static void bd(List<GameStatus> gamestatus)
        {
            try
            {
                string stringconexao = @"Data Source=DESKTOP-CBE2K3G\SQLEXPRESS;Initial Catalog=games;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(stringconexao))
                {

                    String sql = "SELECT name, platform, year, genre, publisher, NA_Sales, eu_Sales,jp_Sales,other_Sales,global_Sales FROM gamesales";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GameStatus gm;
                                gm.name = reader.GetString(0);
                                gm.platform = reader.GetString(1);
                                try
                                {
                                    gm.year = reader.GetInt16(2);
                                }
                                catch { gm.year = 0; }
                                gm.genre = reader.GetString(3);
                                gm.publisher = reader.GetString(4);
                                gm.na = reader.GetDouble(5);
                                gm.eu = reader.GetDouble(6);
                                gm.jp = reader.GetDouble(7);
                                gm.ot = reader.GetDouble(8);
                                gm.gl = reader.GetDouble(9);
                                gamestatus.Add(gm);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        static void Main(string[] args)
        {
            List<GameStatus> gamestatus = new List<GameStatus>();
            bd(gamestatus);

            // question time
            // how many shootes gender is on list?
            // where the game sold the most?
            int total = 0;
            string name="";
            double place = 0;
            foreach (var game in gamestatus)
            {
                
                if(game.genre == "Shooter")
                {
                    total++;
                    if (place < game.na)
                    {
                        place = game.na;
                        name = game.name;
                    }
                }
            }
            Console.WriteLine("The game most sold was {0} with {1}$", name, place);
            Console.WriteLine("total shooters game is {0}", total);
        }
    }
}
