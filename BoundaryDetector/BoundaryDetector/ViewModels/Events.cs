using System;
using System.Collections.Generic;
using System.Text;

namespace BoundaryDetector.ViewModels
{
    public static class Events
    {
        public static string GFiledAdded = "GFieldAdded";

        public static string GFiledUpdated = "GFiledUpdated";
        public static string MapClicked = "MapClicked";

        public static string ClearMapClicked = "ClearMapClicked";

        public static string FieldSelected = nameof(FieldSelected);

        public static string SearchBarTextChanged = nameof(SearchBarTextChanged);
    }
}
