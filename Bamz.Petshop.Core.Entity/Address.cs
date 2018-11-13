using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.Entity
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Letter { get; set; }
        public int? Floor { get; set; }
        public string Side { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
    }
}
