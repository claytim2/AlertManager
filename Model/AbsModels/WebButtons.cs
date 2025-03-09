using Localization.Web;

namespace Model.AbsModels
{
    public class WebButtons
    {
        public int ExtraArgumentsNumber { get; set; }
        public string ClassName { get; set; }
        private string ExtraArgumentsString
        {
            get
            {
                var returnArguments = "";
                for (var x = 1; x <= ExtraArgumentsNumber; x++)
                {
                    returnArguments += " data-extra" + x + "='{" + x + "}' ";
                }
                return returnArguments;
            }
        }

        public static string ViewSm => "<a class='table-detail btn btn-sm btn-info' data-id='{0}' href='javascript:void(0)' data-toggle='tooltip' title='" + ResLabels.View + "'><i class='fa fa-search'></i></a>";
        public string ViewSmExtra => "<a class='" + ClassName + "-detail btn btn-sm btn-info' data-id='{0}' " + ExtraArgumentsString + " href='javascript:void(0)' data-toggle='tooltip' title='" + ResLabels.View + "'><i class='fa fa-search'></i></a>";
        public static string ViewDrop => "<a class='table-detail' data-id='{0}' href='javascript:void(0)'><i class='fa fa-search'></i> " + ResLabels.View + "</a>";
        public string ViewDropExtra => "<a class='" + ClassName + "-detail' data-id='{0}' " + ExtraArgumentsString + " href='javascript:void(0)'><i class='fa fa-search'></i> " + ResLabels.View + "</a>";

        public static string EditSm => "<a class='table-edit btn btn-sm btn-primary' data-id='{0}' href='javascript:void(0)' data-toggle='tooltip' title='" + ResLabels.Edit + "'><i class='fa fa-edit'></i></a>";
        public string EditSmExtra => "<a class='" + ClassName + "-edit btn btn-sm btn-primary' data-id='{0}' " + ExtraArgumentsString + " href='javascript:void(0)' data-toggle='tooltip' title='" + ResLabels.Edit + "'><i class='fa fa-edit'></i></a>";
        public static string EditDrop => "<a class='table-edit' data-id='{0}' href='javascript:void(0)'><i class='fa fa-edit'></i> " + ResLabels.Edit + "</a>";
        public string EditDropExtra => "<a class='" + ClassName + "-edit' data-id='{0}' " + ExtraArgumentsString + " href='javascript:void(0)'><i class='fa fa-edit'></i> " + ResLabels.Edit + "</a>";

        public static string DeleteSm => "<a class='table-delete btn btn-sm btn-danger' data-id='{0}' href='javascript:void(0)' data-toggle='tooltip' title='" + ResLabels.Delete + "'><i class='fa fa-trash-o'></i></a>";
        public string DeleteSmExtra => "<a class='" + ClassName + "-delete btn btn-sm btn-danger' data-id='{0}' " + ExtraArgumentsString + " href='javascript:void(0)' data-toggle='tooltip' title='" + ResLabels.Delete + "'><i class='fa fa-trash-o'></i></a>";
        public static string DeleteDrop => "<a class='table-delete' data-id='{0}' href='javascript:void(0)'><i class='fa fa-trash-o'></i> " + ResLabels.Delete + "</a>";
        public string DeleteDropExtra => "<a class='" + ClassName + "-delete' data-id='{0}' " + ExtraArgumentsString + " href='javascript:void(0)'><i class='fa fa-trash-o'></i> " + ResLabels.Delete + "</a>";

        public static string CancelSm => "<a class='table-cancel btn btn-sm btn-danger' data-id='{0}' href='javascript:void(0)' data-toggle='tooltip' title='" + ResLabels.Cancel + "'><i class='fa fa-close'></i></a>";
        public string CancelSmExtra => "<a class='" + ClassName + "-cancel btn btn-sm btn-danger' data-id='{0}' " + ExtraArgumentsString + " href='javascript:void(0)' data-toggle='tooltip' title='" + ResLabels.Cancel + "'><i class='fa fa-close'></i></a>";
        public static string CancelDrop => "<a class='table-cancel' data-id='{0}' href='javascript:void(0)'><i class='fa fa-close'></i> " + ResLabels.Cancel + "</a>";
        public string CancelExtra => "<a class='" + ClassName + "-cancel' data-id='{0}' " + ExtraArgumentsString + " href='javascript:void(0)'><i class='fa fa-close'></i> " + ResLabels.Cancel + "</a>";

        public static string ImportSm => "<a class='table-import btn btn-sm btn-warning' data-id='{0}' href='javascript:void(0)' data-toggle='tooltip' title='" + ResLabels.Import + "'><i class='fa fa-download'></i></a>";
        public string ImportSmExtra => "<a class='" + ClassName + "-import btn btn-sm btn-warning' data-id='{0}' " + ExtraArgumentsString + " href='javascript:void(0)' data-toggle='tooltip' title='" + ResLabels.Import + "'><i class='fa fa-download'></i></a>";

        public static string MakeGroup(params string[] buttons)
        {
            var hasButtons = false;
            foreach (var button in buttons)
            {
                if (!string.IsNullOrEmpty(button))
                {
                    hasButtons = true;
                    break;
                }
            }

            if (!hasButtons)
                return "";

            var group = "<div class = 'btn-group'>";
            foreach (var button in buttons)
            {
                if (!string.IsNullOrEmpty(button))
                {
                    group += button;
                }
            }
            group += "</div>";
            return group;
        }

        public static string MakeDropDown(params string[] buttons)
        {
            var hasButtons = false;
            foreach (var button in buttons)
            {
                if (!string.IsNullOrEmpty(button))
                {
                    hasButtons = true;
                    break;
                }
            }

            if (!hasButtons)
                return "";

            var dropdown = "";
            dropdown += "<div class='btn-group btn-group-sm' role='group'>";
            dropdown += "        <button type='button' class='btn btn-default dropdown-toggle' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>";
            dropdown += "            <i class='fa fa-gear'></i>";
            dropdown += "            <span class='caret'></span>";
            dropdown += "        </button>";
            dropdown += "        <ul class='dropdown-menu dropdown-menu-right'>";

            foreach (var button in buttons)
            {
                if (!string.IsNullOrEmpty(button))
                {
                    dropdown += "<li>";
                    dropdown += button;
                    dropdown += "</li>";
                }
            }
            dropdown += "        </ul>";
            dropdown += "    </div>";
            return dropdown;
        }


        public WebButtons()
        {
            ExtraArgumentsNumber = 0;
            ClassName = "table";
        }
    }
}
