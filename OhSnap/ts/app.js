/// <reference path="../Scripts/typings/angularjs/angular.d.ts"/>
'use string';
var OhSnap;
(function (OhSnap) {
    var App;
    (function (App) {
        var ohSnapApp = angular.module('ManageFormsApp', [
            'ManageFormsControllers',
            'ngRoute',
            'ui.grid',
            'ui.grid.selection'
        ]);
        ohSnapApp.config(['$routeProvider',
            function ($routeProvider) {
                $routeProvider.
                    when('/manage', {
                    templateUrl: '/Static/ManageForms/Index.html'
                }).
                    when('/add_injury', {
                    templateUrl: '/Static/ManageForms/AddInjury.html'
                }).
                    when('/add_fracture', {
                    templateUrl: '/Static/ManageForms/AddFracture.html'
                }).
                    //when('/phones/:phoneId', {
                    //    templateUrl: 'partials/phone-detail.html',
                    //    controller: 'PhoneDetailCtrl'
                    //}).
                    otherwise({
                    redirectTo: '/manage'
                });
            }]);
    })(App = OhSnap.App || (OhSnap.App = {}));
})(OhSnap || (OhSnap = {}));
