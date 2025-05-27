using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Logging;

namespace Umbraco.Web.UI
{
    [DataEditor(
       alias: "My.MarkdownEditor",
       name: "My markdown editor",
       view: "~/App_Plugins/MarkDownEditor/markdowneditor.html",
       Group = "Rich Content",
       Icon = "icon-code")]
    public class MarkdownEditor : DataEditor
    {
        public MarkdownEditor(ILogger logger)
            : base(logger)
        { }

    }
}