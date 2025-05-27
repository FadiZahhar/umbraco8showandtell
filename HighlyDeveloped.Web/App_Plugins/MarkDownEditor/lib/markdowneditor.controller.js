angular.module("umbraco")
    .controller("My.MarkdownEditorController",
        // inject umbracos assetsService
        function ($scope, assetsService, $timeout) {

            // tell the assetsService to load the markdown.editor libs from the markdown editors
            // plugin folder
            assetsService
                .load([
                    "~/App_Plugins/MarkDownEditor/lib/markdown.converter.js",
                    "~/App_Plugins/MarkDownEditor/lib/markdown.sanitizer.js",
                    "~/App_Plugins/MarkDownEditor/lib/markdown.editor.js"
                ])
                .then(function () {
                    // this function will execute when all dependencies have loaded
                    $timeout(function () {
                        alert("editor dependencies loaded");
                    });
                });

            // load the separate css for the editor to avoid it blocking our JavaScript loading
            assetsService.loadCss("~/App_Plugins/MarkDownEditor/lib/markdown.editor.less");
        });