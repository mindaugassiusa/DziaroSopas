using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DziaroSopas.Models
{
    public class ActiveOderModel
    {
        public List<SelectListItem> Name { get; set; }

        public string SelectedName { get; set; }

        public string SelectedOrderName { get; set; }

        public bool IsOrdered { get; set; }

        public int Quantity { get; set; }
    }
}