using sapr.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sapr.Utilities
{
    public static class FindHihest
    {
        public static int FindHeight()
        {
            int height = 0;
            foreach (var elm in SuportStore.Instance.GetUserData())
                if (elm.Model.Height > height)
                    height = (int)elm.Model.Height;
            return height;
        }
    }
}
