/// <reference path="../../Scripts/typings/angularjs/angular.d.ts"/>

'use strict';

/* Controllers */

module OhSnap.CreateFracture.Controllers {

    var createFractureControllers: angular.IModule = angular.module(
        'CreateFractureControllers',
        [
            // 'OhSnapServices'
        ]);

    //interface Selection {
    //    injury: Injury;
    //    fracture: Fracture;
    //};

    interface CreateFractureScope extends angular.IScope {
        ao_code: string;
        aoPrefixClicked: (prefix: string) => void;
    };

    //interface InjuryResource extends angular.resource.IResourceClass<Injury> {
    //    byUser: (params: { id: string }, success: Function) => Injury[];
    //};

    //interface FractureResource extends angular.resource.IResourceClass<Fracture> {
    //    byInjury: (params: { id: string }, success: Function) => Fracture[];
    //};

    createFractureControllers.controller(
        'CreateFractureCtrl',
        ['$scope',

            ($scope: CreateFractureScope) => {
                $scope.ao_code = null;

                $scope.aoPrefixClicked = (prefix: string) => {
                    //var modalInstance = $modal.open({
                    //    templateUrl: 'aoSelectionDialog.html',
                    //    controller: 'AOSelectionInstanceCtrl',
                    //    resolve: {
                    //        prefix: () => { return prefix; }
                    //    }
                    //});
                    $scope.ao_code = prefix;
                    $scope.$apply();
                }
            }
        ]);
}