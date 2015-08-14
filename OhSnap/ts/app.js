/// <reference path="../Scripts/typings/angularjs/angular.d.ts"/>
'use string';
var OhSnap;
(function (OhSnap) {
    var App;
    (function (App) {
        var ohSnapApp = angular.module('OhSnapApp', [
            'ohSnapControllers',
            'ngSanitize',
            'ui.select'
        ]);
    })(App = OhSnap.App || (OhSnap.App = {}));
})(OhSnap || (OhSnap = {}));
//# sourceMappingURL=app.js.map