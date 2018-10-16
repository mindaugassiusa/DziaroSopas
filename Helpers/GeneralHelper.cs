using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DziaroSopas.Helpers
{
    public static class GeneralHelper
    {
        public static List<SelectListItem> StringsToSelList(List<string> data)
        {
            var result = new List<SelectListItem>();
            foreach (var entry in data)
            {

                result.Add(new SelectListItem
                {
                    Text = entry,
                    Value = entry
                });
            }
            return result;
        }
    }
}