using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdsScraper.DAL.Models;
using AdsScraper.Desktop.Interfaces;
using AngleSharp;
using AngleSharp.Dom.Html;

namespace AdsScraper.Desktop.Logic
{
    public class AdsScraper : IAdsScraper
    {
        private static IAdsScraper _scraper;

        private readonly AppDbContext db = new AppDbContext();

        public static IAdsScraper Instance
        {
            get { return _scraper ?? (_scraper = new AdsScraper()); }
        }

        public void SaveCars(string url)
        {
            var stopwatch = new Stopwatch();


            EventLog.Clear();

            EventLog.WriteLine($"{DateTime.Now:HH:mm:ss} Starting links fetching...");


            var adLinks = GetLinks(url).ToList();

            EventLog.WriteLine($"{DateTime.Now:HH:mm:ss} {adLinks.Count()} Links fetched...");

            Task.Factory.StartNew(() =>
            {
                EventLog.WriteLine($"{DateTime.Now:HH:mm:ss} Starting ads fetching...");

                stopwatch.Start();

                Parallel.ForEach(adLinks, SaveCar);

                stopwatch.Stop();

                EventLog.WriteLine(
                    $"{DateTime.Now:HH:mm:ss} {adLinks.Count} Cars fetched, {stopwatch.Elapsed:g} elapsed.");
            });
        }

        private IEnumerable<string> GetLinks(string url)
        {
            var page = 1;

            do
            {
                var document = BrowsingContext.New(Configuration.Default.WithDefaultLoader())
                    .OpenAsync($"{url}&page={page}").Result;


                var articles = document.QuerySelectorAll(".EntityList-item--Regular");

                var vauArticles = document.QuerySelectorAll(".EntityList-item--VauVau");

                if (articles.Any() == false)
                {
                    break;
                }

                var ads = articles.Select(x => x).ToList();

                ads.AddRange(vauArticles.Select(x => x));

                foreach (var element in ads)
                {
                    var htmlAnchorElement =
                        element.Children.First().FirstElementChild.Children.First() as IHtmlAnchorElement;

                    if (htmlAnchorElement != null)
                    {
                        yield return htmlAnchorElement.Href;
                    }
                }

                page++;
            } while (true);
        }

        private void SaveCar(string url)
        {
            var document = BrowsingContext.New(Configuration.Default.WithDefaultLoader()).OpenAsync(url).Result;

            var summaryTable = document.QuerySelector("table.table-summary > tbody");

            var id = int.Parse(document.QuerySelector(".base-entity-id").TextContent);

            Car car;

            lock (db)
            {
                //Car already saved to database
                if (db.Cars.Any(x => x.Id == id))
                {
                    return;
                }

                car = db.Cars.Add(new Car
                {
                    Id = id,
                    PriceEuro = int.Parse(Regex.Replace(document.QuerySelector(".price--eur").TextContent, @"[^\d]",
                        string.Empty))
                });

                car.Engine = db.Engines.Add(new Engine());
            }

            Manufacturer manufacturer = null;

            foreach (var row in summaryTable.Children.Select(x => x as IHtmlTableRowElement))
            {
                if (row == null || row.Children.Any() == false)
                {
                    continue;
                }

                var th = row.Children.First() as IHtmlTableHeaderCellElement;
                var td = row.Children.Last() as IHtmlTableCellElement;

                if (th.TextContent.ToLower().Contains("marka"))
                {
                    manufacturer =
                        db.Manufacturers.FirstOrDefault(x => x.Name.Equals(td.TextContent,
                            StringComparison.InvariantCultureIgnoreCase))
                        ?? db.Manufacturers.Add(new Manufacturer
                        {
                            Name = td.TextContent
                        });
                }

                else if (th.TextContent.ToLower().Contains("model automobila"))
                {
                    car.Model = db.Models.FirstOrDefault(
                                    x => x.Name.Equals(td.TextContent, StringComparison.InvariantCultureIgnoreCase)) ??
                                db.Models.Add(new Model
                                {
                                    Name = td.TextContent,
                                    Manufacturer = manufacturer
                                });
                }

                else if (th.TextContent.ToLower().Contains("godina proizvodnje"))
                {
                    car.Year = int.Parse(Regex.Replace(td.TextContent, @"[^\d]", string.Empty));
                }

                else if (th.TextContent.ToLower().Contains("registriran do"))
                {
                    car.IsRegistred = true;
                }

                else if (th.TextContent.ToLower().Contains("kilometri"))
                {
                    car.Kilometers = int.Parse(Regex.Replace(td.TextContent, @"[^\d]", string.Empty));
                }
                else if (th.TextContent.ToLower().Contains("snaga motora"))
                {
                    car.Engine.Power = int.Parse(Regex.Replace(td.TextContent, @"[^\d]", string.Empty));
                }
                else if (th.TextContent.ToLower().Contains("motor"))
                {
                    var engine = td.TextContent.ToLower().Replace(" ", string.Empty);

                    if (engine.Contains("diesel"))
                    {
                        car.Engine.Fuel = db.Fuels.Find(1);
                    }
                    else if (engine.Contains("benzin"))
                    {
                        car.Engine.Fuel = db.Fuels.Find(2);
                    }
                    else if (engine.Contains("električni"))
                    {
                        car.Engine.Fuel = db.Fuels.Find(4);
                    }
                    else if (engine.Contains("hibrid"))
                    {
                        car.Engine.Fuel = db.Fuels.Find(5);
                    }
                }
                else if (th.TextContent.ToLower().Contains("radni obujam"))
                {
                    car.Engine.Volume = int.Parse(Regex.Replace(td.TextContent.Split(' ')[0], @"[^\d]", string.Empty));
                }

                else if (th.TextContent.ToLower().Contains("plin"))
                {
                    car.Engine.HasGasInstallation = true;
                }
                else if (th.TextContent.ToLower().Contains("mjenjač"))
                {
                    car.Engine.GearboxType = td.TextContent.ToLower().Contains("mehanički")
                        ? GearboxType.Manual
                        : GearboxType.Automatic;
                }
                else if (th.TextContent.ToLower().Contains("broj stupnjeva"))
                {
                    car.Engine.GearsCount = int.Parse(Regex.Replace(td.TextContent, @"[^\d]", string.Empty));
                }
                else if (th.TextContent.ToLower().Contains("vlasnik"))
                {
                    if (td.TextContent.ToLower().Contains("prvi"))
                    {
                        car.OwnersCount = 1;
                    }
                    else if (td.TextContent.ToLower().Contains("drugi"))
                    {
                        car.OwnersCount = 2;
                    }
                    else
                    {
                        car.OwnersCount = -1;
                    }
                }
            }

            //var equipment = (document.QuerySelector(".passage-standard--alpha").Children
            //    .FirstOrDefault(x => x.TextContent.ToLower().Contains("dodatna oprema"))
            //    ?.NextElementSibling)?.Children.Select(x => x.TextContent);

            //var safety = (document.QuerySelector(".passage-standard--alpha").Children
            //    .FirstOrDefault(x => x.TextContent.ToLower().Contains("sigurnost"))
            //    ?.NextElementSibling)?.Children.Select(x => x.TextContent);

            //var comfort = (document.QuerySelector(".passage-standard--alpha").Children
            //    .FirstOrDefault(x => x.TextContent.ToLower().Contains("udobnost"))
            //    ?.NextElementSibling)?.Children.Select(x => x.TextContent);

            //var carEquipment = new List<string>();

            //if (equipment != null)
            //{
            //    carEquipment.AddRange(equipment);
            //}
            //if (safety != null)
            //{
            //    carEquipment.AddRange(safety);
            //}
            //if (comfort != null)
            //{
            //    carEquipment.AddRange(comfort);
            //}

            //if (carEquipment.Any())
            //{
            //    car.Equipment = carEquipment;
            //}

            lock (db)
            {
                db.SaveChanges();

                //EventLog.WriteLine(
                //    $"{DateTime.Now:HH:mm:ss} Saved {car.Id} - {car.Manufacturer} {car.ModelName}...");
            }
        }
    }
}