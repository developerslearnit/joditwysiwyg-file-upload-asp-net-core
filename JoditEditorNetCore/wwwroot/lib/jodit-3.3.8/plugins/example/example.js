"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
Jodit.defaultOptions.controls.example = {
    iconURL: '{basePath}plugins/example/icon.svg',
    tooltip: 'Example',
    popup: function () {
        return "<div class=\"jodit_example\">Example plugin</div>";
    }
};
Jodit.defaultOptions.controls.example2 = {
    iconURL: '{basePath}plugins/example/icon.svg',
    tooltip: 'Example2',
    exec: function (editor) {
        editor.selection.insertHTML('Hello world');
    }
};
var example = (function () {
    function example() {
        this.hasStyle = true;
    }
    example.prototype.init = function (jodit) {
        alert('Example plugin');
    };
    example.prototype.destruct = function () { };
    return example;
}());
Jodit.plugins.add('example', example);
Jodit.plugins.add('example2', {
    init: function () {
        alert('Example2 plugin');
    },
    destruct: function () { }
});
//# sourceMappingURL=example.js.map