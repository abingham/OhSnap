/// <reference path="../../Scripts/typings/angularjs/angular.d.ts"/>
/// <reference path="../services.ts"/>

'use strict';

/* Controllers */

module OhSnap.CreateFracture.Controllers {

    var createFractureControllers: angular.IModule = angular.module(
        'CreateFractureControllers',
        [
            'ui.bootstrap',
            'OhSnapServices'
        ]);

    interface CreateFractureScope extends angular.IScope {
        ao_code: string;
        aoPrefixClicked: (prefix: string) => void;
    };

    createFractureControllers.controller(
        'CreateFractureCtrl',
        ['$scope',
         '$modal',
         'AOCodes',
         ($scope: CreateFractureScope,
          $modal,
          AOCodes: angular.resource.IResourceClass<OhSnap.Service.AOInfo>) => {
                $scope.ao_code = null;

                $scope.aoPrefixClicked = (prefix: string) => {
                    var modalInstance = $modal.open({
                        templateUrl: '/aoSelectionDialog.html',
                        controller: 'AOSelectionInstanceCtrl',
                        resolve: {
                            prefix: () => { return prefix; },
                            AOCodes: () => { return AOCodes; }
                        }
                    });

                    modalInstance.result.then(function (ao_info: OhSnap.Service.AOInfo) {
                        $scope.ao_code = ao_info.Code;
                    });
                }
            }
        ]);

    // TODO: Put the typing back in place once things have settled down.
    export interface AOSelectionInstanceScope extends angular.IScope {
        // The list of AOInfo's whose code matches the prefix provided to the dialog.
        prefixedAoCodes: OhSnap.Service.AOInfo[];
        
        select: (aoInfo: OhSnap.Service.AOInfo) => void;
        cancel: () => void;
    }

    // Controller for AO Code selection modal dialog box.
    createFractureControllers.controller(
        'AOSelectionInstanceCtrl',
        ($scope: AOSelectionInstanceScope,
         $modalInstance: angular.ui.bootstrap.IModalServiceInstance,
         prefix: string,
         AOCodes: angular.resource.IResourceClass<OhSnap.Service.AOInfo>) => {
            var all_codes = AOCodes.query({}, () => {
                // Find all ao-codes that match the current prefix
                $scope.prefixedAoCodes = _.filter(
                    all_codes,
                    (c: OhSnap.Service.AOInfo) => {
                        return (c.Code.substring(0, 2) == prefix);
                    });
            });

            $scope.select = (aoInfo) => {
                $modalInstance.close(aoInfo);
            };

            $scope.cancel = () => {
                $modalInstance.dismiss('cancel');
            };
        });    
}