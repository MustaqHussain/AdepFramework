using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dwp.Adep.Framework.Management.Web.Models
{
    public class CountryModel1
    {
        #region Primitive Properties

        public virtual System.Guid ID
        {
            get;
            set;
        }

        public virtual string Description
        {
            get;
            set;
        }

        [Display(Name = "Frozen Rate")]
        public virtual bool FrozenRate
        {
            get;
            set;
        }

        [Display(Name = "European Community")]
        public virtual bool EC
        {
            get;
            set;
        }

        [Display(Name = "Random Attribute")]
        public virtual bool RA
        {
            get;
            set;
        }

        [Display(Name = "ANZAC coutnry")]
        public virtual bool Anzac
        {
            get;
            set;
        }

        public virtual string Nationality
        {
            get;
            set;
        }

        public virtual string Code
        {
            get;
            set;
        }

        public virtual string Notes
        {
            get;
            set;
        }

        #endregion
    }

    public class Countries
    {
        public List<CountryModel1> _countryList = new List<CountryModel1>();

        public Countries()
        {
            _countryList.Add(new CountryModel1
                {
                    ID = Guid.NewGuid(),
                    Code = "123",
                    Anzac = false,
                    Description = "Description 1",
                    EC = true,
                    FrozenRate = false,
                    Nationality = "British",
                    Notes = "Test Notes",
                    RA = false
                });
            _countryList.Add(new CountryModel1
                {
                    ID = Guid.NewGuid(),
                    Code = "234",
                    Anzac = false,
                    Description = "Description 2",
                    EC = false,
                    FrozenRate = false,
                    Nationality = "Bigendian",
                    Notes = "Test Notes 12",
                    RA = false
                });
                   
        }

        public void Update(CountryModel1 toUpdate)
        {
            _countryList.Remove(_countryList.Find(x => x.ID == toUpdate.ID));
            _countryList.Add(toUpdate);
        }

        public void Create(CountryModel1 toCreate)
        {
            _countryList.Add(toCreate);
        }

        public CountryModel1 GetCountry(string id)
        {
            return _countryList.Find(x => x.ID.ToString() == id);
        }
    }
}