/// <reference path="../../Scripts/typings/angularjs/angular.d.ts"/>

'use strict';

/* Controllers */

module OhSnap.Controller {

    export var ohSnapControllers: angular.IModule = angular.module(
        'ohSnapControllers',
        [
            'ohSnapServices'
        ]);

}