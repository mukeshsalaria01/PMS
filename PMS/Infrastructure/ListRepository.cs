using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PMS.Infrastructure;
using PMS.Web.UI.Core.Interfaces;
using PMS.Web.UI.ViewModels;

namespace PMS.Infrastructure
{
    public class ListRepository : IListRepository
    {
        private readonly AppDbContext _appDbContext;

        public ListRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public TextValuePair[] GetAllMeasureOfPerformance()
        {
            return new List<TextValuePair>
            {
                new TextValuePair{Text = "Select MOP",Value = 0},
                new TextValuePair{Text = "Profit",Value = 1},
                new TextValuePair{Text = "Timelines",Value = 2},
                new TextValuePair{Text = "Timelines & accuracy",Value = 3},
                new TextValuePair{Text = "Rs lacs",Value = 4},
                new TextValuePair{Text = "Timeliness",Value = 5},
                new TextValuePair{Text = "Other",Value = 6}
            }.ToArray();
        }


    }
}
