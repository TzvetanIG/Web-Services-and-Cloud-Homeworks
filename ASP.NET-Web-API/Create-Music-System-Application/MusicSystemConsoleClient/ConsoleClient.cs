namespace MusicSystemConsoleClient
{
    using System;
    using System.Collections.Generic;
    using Data;

    class ConsoleClient
    {
        private static Resourses resourses = new Resourses();
        static void Main()
        {
            Console.Clear();
            var userChoice = string.Empty;
            while (userChoice != "7")
            {
                userChoice = Menu();
                ExecuteComands(userChoice);
            }

            var data = new Resourses();
            var artists = data.Artists;
            //var result = artists.Add(new
            //{
            //    Name = "Mihal",
            //    Country = "Okeania",
            //    DateOfBirth = new DateTime(1978, 8, 12)
            //});

            //var result = artists.AddSongs(new
            //{
            //    Name = "Mihal",
            //    Songs = new string[] {"Song3"}
            //});


            //result = data.Songs.Add(new
            //{
            //    Title = "Misho",
            //    Year = 2000,
            //    Ganre = "rock"
            //});

            //var allSongsAsJson = data.Songs.All.Json;
            //Console.WriteLine(allSongsAsJson.Content);

            //Console.WriteLine(result.Json.StatusCode);

            //var allArtistsAsJson = artists.All.Json;
            //Console.WriteLine(allArtistsAsJson.Content);

            //Console.WriteLine();
            //var allArtistsAsXml = artists.All.Xml;
            //Console.WriteLine(allArtistsAsXml.Content);

        }

        private static void ExecuteComands(string userChoice)
        {
            IRequestContentType result = null;
            string mes = null;

            switch (userChoice)
            {
                case "1":
                    mes = "Add Artist";
                    result = AddArtist();
                    break;
                case "2":
                    mes = "All Artists";
                    result = GetAllArtist();
                    break;
                case "3":
                    mes = "Add Song";
                    result = AddSong();
                    break;
                case "4":
                    mes = "All Songs";
                    result = GetAllSongs();
                    break;
                case "5":
                    mes = "Add Songs to Artist";
                    result = AddSongToArtist();
                    break;
                case "6":
                    mes = "Show Songs to Artist";
                    result = ShowSongToArtist();
                    break;
            }

            if (result != null)
            {
                PrintResults(result, mes);
            }

            Console.SetCursorPosition(0, 0);
        }

        private static string Menu()
        {
            Console.WriteLine("1. Add artist");
            Console.WriteLine("2. Show all artists");
            Console.WriteLine("3. Add song");
            Console.WriteLine("4. Show all artists");
            Console.WriteLine("5. Add song to srtist");
            Console.WriteLine("6. Show song to srtist");
            Console.WriteLine("7. Exit");
            Console.WriteLine();
            Console.Write("Choose from 1 to 7: ");

            string n = Console.ReadLine();
            Console.WriteLine();
            return n;
        }

        private static IRequestContentType AddArtist()
        {
            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.Write("Country: ");
            var country = Console.ReadLine();

            Console.Write("Date /yyyy-mm-dd/: ");
            var date = DateTime.Parse(Console.ReadLine());

            var artists = resourses.Artists;
            var result = artists.Add(new
            {
                Name = name,
                Country = country,
                DateOfBirth = date
            });

            return result;
        }

        private static IRequestContentType AddSong()
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();

            Console.Write("Ganre: ");
            var ganre = Console.ReadLine();

            Console.Write("Year: ");
            var year = int.Parse(Console.ReadLine());

            var result = resourses.Songs.Add(new
            {
                Title = title,
                Year = year,
                Ganre = ganre
            });

            return result;
        }

        private static IRequestContentType AddSongToArtist()
        {
            Console.Write("Artist name: ");
            var artistName = Console.ReadLine();

            var songs = new List<string>();
            string songName;
            do
            {
                Console.Write("Song name: ");
                songName = Console.ReadLine();
                songs.Add(songName);
            } while (songName != String.Empty);

            var result = resourses.Artists.AddSongs(
                new
                {
                    Name = artistName,
                    Songs = songs
                });

            return result;
        }

        private static IRequestContentType GetAllSongs()
        {
            Console.WriteLine();
            var songs = resourses.Songs.All;
            return songs;
        }

        private static IRequestContentType GetAllArtist()
        {
            Console.WriteLine();
            var artists = resourses.Artists.All;
            return artists;
        }

        private static IRequestContentType ShowSongToArtist()
        {
            Console.Write("Artist name: ");
            var artistName = Console.ReadLine();
            var result = resourses.Artists.GetSongs(new 
            {
               Name = artistName
            });

            return result;
        }

        private static void PrintResults(IRequestContentType result, string mes)
        {
            Console.SetCursorPosition(0, 15);
            var choice = MenuFormat();
            Console.WriteLine("--------------- {0} -------------", mes);
            Console.WriteLine();
            Console.WriteLine();
            if (choice == "1")
            {
                Console.WriteLine("Status Code: {0}", result.Json.StatusCode);
                Console.WriteLine();
                Console.WriteLine(result.Json.Content);
            }
            else
            {
                Console.WriteLine("Status Code: {0}", result.Xml.StatusCode);
                Console.WriteLine();
                Console.WriteLine(result.Xml.Content);
            }

            Console.WriteLine("\nPress ENTER to continue ...");
            Console.ReadLine();
            Console.Clear();
        }

        private static string MenuFormat()
        {
            Console.WriteLine("1. Json");
            Console.WriteLine("2. Xml");
            Console.WriteLine();
            Console.Write("Choose from 1 or any char: ");

            string n = Console.ReadLine();

            return n;
        }
    }
}
