/// <reference path="../Scripts/typings/angularjs/angular.d.ts"/>

'use string';

module OhSnap.App {

    var ohSnapApp: angular.IModule = angular.module(
    'OhSnapApp',
    [
        'ohSnapControllers',
        'ngSanitize',
        'ui.select'
    ]);
}
