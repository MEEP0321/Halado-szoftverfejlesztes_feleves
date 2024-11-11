using ConsoleTools;
using DJLWQB_HSZF_2024251.Application;
using DJLWQB_HSZF_2024251.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DJLWQB_HSZF_2024251.Console
{
    public class Display
    {
        IHost host;
        ICostumerService costumerService;
        IMovieService movieService;
        IRentalService rentalService;
        public Display(IHost host)
        {
            this.host = host;
            host.Start();

            costumerService = host.Services.GetRequiredService<ICostumerService>();
            movieService = host.Services.GetRequiredService<IMovieService>();
            rentalService = host.Services.GetRequiredService<IRentalService>();
        }

        public void StartApp()
        {
            ConsoleMenu m = MainMenu();
            m.Show();
        }

        private ConsoleMenu MainMenu()
        {
            var MainMenu = new ConsoleMenu()
                .Add("Rent a Movie", (thisMenu) =>
                {
                    thisMenu.CloseMenu();
                    ConsoleMenu m = RentalMenuCostumerChoser();
                    m.Show();
                })
                .Add("Costumers", (thisMenu)=> 
                {
                    thisMenu.CloseMenu();
                    ConsoleMenu m = EntityMenu(typeof(Costumer));
                    m.Show();
                })
                .Add("Movies", (thisMenu) =>
                {
                    thisMenu.CloseMenu();
                    ConsoleMenu m = EntityMenu(typeof(Movie));
                    m.Show();
                })
                .Add("Close app", () => Environment.Exit(0))
                .Configure(config =>
                {
                    config.Selector = "-->";
                    config.EnableFilter = false;
                    config.Title = "Main menu";
                    config.EnableWriteTitle = true;
                    config.WriteTitleAction = title => AnsiConsole.Write(new FigletText(title));
                    config.SelectedItemBackgroundColor = Color.Black;
                    config.SelectedItemForegroundColor = Color.Blue;
                    config.WriteItemAction = item => System.Console.Write(" {0}", item.Name);
                });

            return MainMenu;
        }

        //Basic CRUD with costumer and movies
        private ConsoleMenu EntityMenu(Type type)
        {
            var entityMenu = new ConsoleMenu()
                .Add($"Add {type.Name}", (thisMenu) => {
                    thisMenu.CloseMenu();
                    System.Console.Clear();
                    ConsoleMenu m;
                    switch (type.Name)
                    {
                        case "Costumer":
                            costumerService.AddCostumer(AddOrModifyCostumer(false, null));
                            m = EntityMenu(typeof(Costumer));
                            m.Show();
                            break;
                        case "Movie":
                            movieService.AddMovie(AddOrModifyMovie(false, null));
                            m = EntityMenu(typeof(Movie));
                            m.Show();
                            break;
                        default:
                            // code block
                            break;
                    }
                })
                .Add("Exit", (thisMenu) =>
                {
                    thisMenu.CloseMenu();
                    ConsoleMenu m = MainMenu();
                    m.Show();
                })
                .Configure(config =>
                {
                    config.Selector = "-->";
                    config.EnableFilter = true;
                    config.Title = $"{type.Name}s";
                    config.EnableWriteTitle = true;
                    config.WriteTitleAction = title => AnsiConsole.Write(new FigletText(title));
                    config.SelectedItemBackgroundColor = Color.Black;
                    config.SelectedItemForegroundColor = Color.Blue;
                    config.WriteItemAction = item => System.Console.Write(" {0}", item.Name);
                    config.FilterPrompt = $"Search for a {type.Name}:";
                    config.WriteHeaderAction = () => { };
                });

            switch (type.Name)
            {
                case "Costumer":
                    foreach (var item in costumerService.GetCostumers())
                    {
                        entityMenu.Add(item.ToString(), ModMenu(item.Id, item.Name, item.GetType()).Show);
                    }

                    break;
                case "Movie":
                    foreach (var item in movieService.GetMovies())
                    {
                        entityMenu.Add(item.ToString(), ModMenu(item.Id, item.Title, item.GetType()).Show);
                    }

                    break;
                default:
                    // code block
                    break;
            }
         

            return entityMenu;
        }

        private ConsoleMenu ModMenu(string id, string name, Type entityType)
        {
            ConsoleMenu m;
            var ModMenu = new ConsoleMenu()
                .Add($"Modify {entityType.Name}", (thisMenu) => {
                    thisMenu.CloseMenu();
                    System.Console.Clear();
                    switch (entityType.Name)
                    {
                        case "Costumer":
                            costumerService.ModifyCostumer(id, AddOrModifyCostumer(true, costumerService.GetCostumer(id)));
                            break;
                        case "Movie":
                            movieService.ModifyMovie(id, AddOrModifyMovie(true, movieService.GetMovie(id)));
                            break;
                        default:
                            // code block
                            break;
                    }
                    ConsoleMenu m = EntityMenu(entityType);
                    m.Show();
                })
                .Add($"Remove {entityType.Name}", (thisMenu) =>
                {
                    switch (entityType.Name)
                    {
                        case "Costumer":
                            costumerService.RemoveCostumer(id);
                            thisMenu.CloseMenu();
                            m = EntityMenu(typeof(Costumer));
                            m.Show();
                            break;
                        case "Movie":
                            movieService.RemoveMovie(id);
                            thisMenu.CloseMenu();
                            m = EntityMenu(typeof(Movie));
                            m.Show();
                            break;
                        default:

                            break;
                    }
                })
                .Add($"Exit", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "-->";
                    config.EnableFilter = false;
                    config.EnableWriteTitle = true;
                    config.Title = name;
                    config.WriteTitleAction = title => AnsiConsole.Write(new Rule($"[orange3]{title}[/]").LeftJustified());
                    config.SelectedItemBackgroundColor = Color.Black;
                    config.SelectedItemForegroundColor = Color.Blue;
                    config.WriteItemAction = item => System.Console.Write(" {0}", item.Name);
                }); 

            return ModMenu;
        }

        private Costumer AddOrModifyCostumer(bool isModify, Costumer c)
        {
            var title = isModify? new FigletText("Modify costumer") : new FigletText("Add costumer");
            AnsiConsole.Write(title);
            //Name
            var nameRule = isModify ? new Rule($"[orange3]Name[/] Original was: {c.Name}").LeftJustified() : new Rule($"[orange3]Name[/]").LeftJustified();
            AnsiConsole.Write(nameRule);

            string name = AnsiConsole.Ask<string>("Please enter the name! ");

            //Age
            var ageRule = isModify? new Rule($"[orange3]Age[/] Original was: {c.Age}").LeftJustified() : new Rule("[orange3]Age[/]").LeftJustified();
            System.Console.WriteLine();
            AnsiConsole.Write(ageRule);

            int age = AnsiConsole.Prompt(
                new TextPrompt<int>("Please enter the age! ")
                .Validate((x) => x switch
                {
                    <= 0 => ValidationResult.Error("[red]Age should be larger than 0![/]"),
                    > 0 => ValidationResult.Success()
                }));

            //Contact
            var contactRule =  isModify? new Rule($"[orange3]Contact[/] Original was: {c.Contact}").LeftJustified() : new Rule("[orange3]Contact[/]").LeftJustified();
            System.Console.WriteLine();
            AnsiConsole.Write(contactRule);

            string contact = AnsiConsole.Prompt(
                new TextPrompt<string>("[[Optional]] Please enter a contact! ")
                    .AllowEmpty());

            if (string.IsNullOrWhiteSpace(contact))
            {
                return new Costumer(name, age);
            }
            return new Costumer(name, age, contact);


        }

        private Movie AddOrModifyMovie(bool isModify, Movie a)
        {
            var title = isModify ? new FigletText("Modify movie") : new FigletText("Add movie");
            AnsiConsole.Write(title);

            //Title
            var titleRule = isModify? new Rule($"[orange3]Title[/] Original was: {a.Title}").LeftJustified() : new Rule("[orange3]Title[/]").LeftJustified();
            AnsiConsole.Write(titleRule);

            string movieTitle = AnsiConsole.Ask<string>("Please enter the title! ");

            //Genre
            var genreRule = isModify? new Rule($"[orange3]Genre[/] Original was: {a.Genre}").LeftJustified() : new Rule("[orange3]Genre[/]").LeftJustified();
            System.Console.WriteLine();
            AnsiConsole.Write(genreRule);

            string genre = AnsiConsole.Ask<string>("Please enter the genre! ");

            //Release
            var releaseRule = isModify? new Rule($"[orange3]Release[/] Original was: {a.Release.ToShortDateString()}").LeftJustified() : new Rule("[orange3]Release[/]").LeftJustified();
            System.Console.WriteLine();
            AnsiConsole.Write(releaseRule);

            DateTime release = AnsiConsole.Prompt(
                new TextPrompt<DateTime>("Please enter the release date! ")
                );

            //Price
            var priceRule = isModify? new Rule($"[orange3]Price[/] Original was: {a.Price}").LeftJustified() : new Rule("[orange3]Price[/]").LeftJustified();
            System.Console.WriteLine();
            AnsiConsole.Write(priceRule);

            int price = AnsiConsole.Prompt(
                new TextPrompt<int>("Please enter the price! ")
                .Validate((x) => x switch
                {
                    <= 0 => ValidationResult.Error("[red]Price should be larger than 0![/]"),
                    > 0 => ValidationResult.Success()
                })
            );

            //Pieces
            var pcsRule = isModify? new Rule($"[orange3]Pieces[/] Original was {a.Pcs}").LeftJustified() : new Rule("[orange3]Pieces[/]").LeftJustified();
            System.Console.WriteLine();
            AnsiConsole.Write(pcsRule);

            var pcs = AnsiConsole.Prompt(
                new TextPrompt<string>("[[Optional]] Please enter the number of pieces! ")
                .AllowEmpty()
                .Validate((x) =>
                {
                    if (!string.IsNullOrWhiteSpace(x) && !int.TryParse(x, out _))
                    {
                        return ValidationResult.Error("[red]Invalid type![/]");
                    }
                    else if (int.TryParse(x, out _) && int.Parse(x) < 0)
                    {
                        return ValidationResult.Error("[red]The number should be larger than 0![/]");
                    }
                    else
                    {
                        return ValidationResult.Success();
                    }
                })
            );

            if (string.IsNullOrWhiteSpace(pcs))
            {
                return new Movie(movieTitle, genre, release, price);
            }
            return new Movie(movieTitle, genre, release, price, int.Parse(pcs));
           
        }

        //Rental
        private ConsoleMenu RentalMenuCostumerChoser()
        {
            var menu = new ConsoleMenu()
                .Configure(config =>
                {
                    config.Selector = "-->";
                    config.EnableFilter = false;
                    config.Title = "Costumer";
                    config.EnableWriteTitle = true;
                    config.WriteTitleAction = title => AnsiConsole.Write(new Rule($"[orange3]{title}[/]").LeftJustified());
                    config.WriteHeaderAction = () => System.Console.WriteLine("Choose a costumer:");
                    config.SelectedItemBackgroundColor = Color.Black;
                    config.SelectedItemForegroundColor = Color.Blue;
                    config.WriteItemAction = item => System.Console.Write(" {0}", item.Name);
                });

            foreach (var item in costumerService.GetCostumers()) 
            {
                menu.Add(item.ToString(), (thisMenu) =>
                {
                    thisMenu.CloseMenu();
                    ConsoleMenu m = RentalMenuMovieChoser(item.Id);
                    m.Show();
                });
            }

            return menu;
        }

        private ConsoleMenu RentalMenuMovieChoser(string costumerId)
        {
            var menu = new ConsoleMenu()
                .Configure(config =>
                {
                    config.Selector = "-->";
                    config.EnableFilter = false;
                    config.Title = "Movie";
                    config.EnableWriteTitle = true;
                    config.WriteTitleAction = title => AnsiConsole.Write(new Rule($"[orange3]{title}[/]").LeftJustified());
                    config.WriteHeaderAction = () => System.Console.WriteLine("Choose a movie:");
                    config.SelectedItemBackgroundColor = Color.Black;
                    config.SelectedItemForegroundColor = Color.Blue;
                    config.WriteItemAction = item => System.Console.Write(" {0}", item.Name);
                });

            foreach (var item in movieService.GetMovies())
            {
                menu.Add(item.ToString(), (thisMenu) => {
                    thisMenu.CloseMenu();
                    MakeRental(costumerId, item.Id);
                });
            }

            return menu;
        }

        private void MakeRental(string costumerId, string movieId)
        { 
            System.Console.Clear();
            var costumerRule = new Rule("[orange3]Costumer name[/]").LeftJustified();
            AnsiConsole.Write(costumerRule);
            System.Console.WriteLine(costumerService.GetCostumer(costumerId).Name);
            System.Console.WriteLine();

            var movieRule = new Rule("[orange3]Movie title[/]").LeftJustified();
            AnsiConsole.Write(movieRule);
            System.Console.WriteLine(movieService.GetMovie(movieId).Title);
            System.Console.WriteLine();

            var dateRule = new Rule("[orange3]Rental date[/] (Default value is the current date)").LeftJustified();
            AnsiConsole.Write(dateRule);

            DateTime rentalDate = AnsiConsole.Prompt(
                new TextPrompt<DateTime>("[[Optional]] Please enter the rental date! ")
                .AllowEmpty()
            );

            System.Console.WriteLine();
            var priceRule = new Rule($"[orange3]Price[/] (Default value is: {movieService.GetMovie(movieId).Price})").LeftJustified();
            AnsiConsole.Write(priceRule);

            var price = AnsiConsole.Prompt(
                new TextPrompt<string>("[[Optional]] Please enter the price! ")
                .AllowEmpty()
                .Validate((x) =>
                {
                    if (!string.IsNullOrWhiteSpace(x) && !int.TryParse(x, out _))
                    {
                        return ValidationResult.Error("[red]Invalid type![/]");
                    }
                    else if (int.TryParse(x, out _) && int.Parse(x) < 0)
                    {
                        return ValidationResult.Error("[red]The price should be larger than 0![/]");
                    }
                    else
                    {
                        return ValidationResult.Success();
                    }
                })
            );

            rentalService.AddRental(new Rental(costumerId, movieId, 
                rentalDate == default(DateTime) ? DateTime.Now : rentalDate,
                string.IsNullOrWhiteSpace(price) ? movieService.GetMovie(movieId).Price : int.Parse(price)
                )
            );

            ConsoleMenu m = MainMenu();
            m.Show();
            
        }
    }
}
