using CoreApiPOC.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApiPOC.Models
{
    public class MasterPageItem
    {
        public MasterPageItem()
        {
            TargetType = typeof(LandingView);
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }
    }
}
