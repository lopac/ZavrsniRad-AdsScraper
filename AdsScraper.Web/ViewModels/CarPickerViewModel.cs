using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AdsScraper.DAL.Models;
using Microsoft.Ajax.Utilities;

namespace AdsScraper.Web.ViewModels
{
    public class CarPickerViewModel
    {
        private AppDbContext db = new AppDbContext();

        public IEnumerable<SelectListItem> Manufacturers
        {
            get
            {
                var manufacturers = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Value = String.Empty,
                        Text = String.Empty,
                        Disabled = true,
                        Selected = true
                    }
                };


                manufacturers.AddRange(db.Cars.DistinctBy(x => x.Model.Manufacturer).Select(x => new SelectListItem
                {
                    Value = x.Model.Manufacturer.Id.ToString(),
                    Text = x.Model.Manufacturer.Name
                }).OrderBy(x => x.Value));

                return manufacturers;
            }
        }

        public IEnumerable<SelectListItem> MakeYear
        {
            get
            {
                return Enumerable.Range(2000, 18).Select(x => new SelectListItem
                {
                    Value = x.ToString(),
                    Text = x.ToString()
                });
            }
        }

        public IEnumerable<SelectListItem> Gearboxes
        {
            get
            {
                return Enum.GetValues(typeof(GearboxType)).Cast<GearboxType>().Select(x => new SelectListItem
                {
                    Value = Convert.ToInt32(x).ToString(),
                    Text = x.ToString()
                });
            }
        }


        public IEnumerable<SelectListItem> GearsCount
        {
            get
            {
                return Enumerable.Range(4, 3).Select(x => new SelectListItem
                {
                    Value = x.ToString(),
                    Text = x.ToString()
                });
            }
        }

        public IEnumerable<SelectListItem> Fuels { get; set; }

        public IEnumerable<SelectListItem> EngineTypes
        {
            get
            {
                return db.Fuels.Select(fuel => new SelectListItem
                {
                    Value = fuel.Id.ToString(),
                    Text = fuel.Name.ToString()
                });
            }
        }

        public virtual IEnumerable<SelectListItem> Owners
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Jedan",
                        Value = "1",
                    },
                    new SelectListItem
                    {
                        Text = "Dva",
                        Value = "2",
                    },
                    new SelectListItem
                    {
                        Text = "Tri ili više",
                        Value = "-1",
                    }
                };
            }
        }
    }
}