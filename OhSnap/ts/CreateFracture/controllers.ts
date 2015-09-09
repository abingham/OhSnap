/// <reference path="../../Scripts/typings/angularjs/angular.d.ts"/>

'use strict';

/* Controllers */

module OhSnap.CreateFracture.Controllers {

    var createFractureControllers: angular.IModule = angular.module(
        'CreateFractureControllers',
        [
            'ui.bootstrap'
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
        ['$scope', '$modal',

            ($scope: CreateFractureScope,
             $modal) => {
                $scope.ao_code = null;

                $scope.aoPrefixClicked = (prefix: string) => {
                    var modalInstance = $modal.open({
                        templateUrl: '/aoSelectionDialog.html',
                        controller: 'AOSelectionInstanceCtrl',
                        resolve: {
                            prefix: () => { return prefix; }
                        }
                    });
                }
            }
        ]);

    export interface AOSelectionInstanceScope extends angular.IScope {
        //prefixedAoCodes: FracReg.Controller.AOInfo[];

        //// Available dialog actions.
        //ok: () => void;
        //cancel: () => void;
    }

    createFractureControllers.controller(
        'AOSelectionInstanceCtrl',
        ($scope: any,
            $modalInstance: angular.ui.bootstrap.IModalServiceInstance,
            prefix: string) => {
            //var all_codes: [OhSnap..Controller.AOInfo] = AOCodes.query({}, () => {
            //    // Find all ao-codes that match the current prefix
            //    $scope.prefixedAoCodes = _.filter(
            //        all_codes,
            //        (c) => {
            //            return (c.id.substring(0, 2) == prefix);
            //        });
            //});

            //$scope.select = (aoInfo: FracReg.Controller.AOInfo) => {
            //    $modalInstance.close(aoInfo);
            //};

            //$scope.cancel = () => {
            //    $modalInstance.dismiss('cancel');
            //};
        });    
}