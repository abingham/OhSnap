// <reference path="../lib/angularjs/angular.d.ts"/>
/// <reference path="../Scripts/typings/angular-ui-bootstrap/angular-ui-bootstrap.d.ts"/>
/// <reference path="../Scripts/typings/underscore/underscore.d.ts"/>
/// <reference path="fracregControllers.ts"/>
'use strict';
var FracReg;
(function (FracReg) {
    var AOSelection;
    (function (AOSelection) {
        var aoSelectionControllers = angular.module('aoSelectionControllers', ['ui.bootstrap.modal',
            'fracregServices']);
        aoSelectionControllers.controller('AOSelectionInstanceCtrl', function ($scope, $modalInstance, AOCodes, prefix) {
            var all_codes = AOCodes.query({}, function () {
                // Find all ao-codes that match the current prefix
                $scope.prefixedAoCodes = _.filter(all_codes, function (c) {
                    return (c.id.substring(0, 2) == prefix);
                });
            });
            $scope.select = function (aoInfo) {
                $modalInstance.close(aoInfo);
            };
            $scope.cancel = function () {
                $modalInstance.dismiss('cancel');
            };
        });
    })(AOSelection = FracReg.AOSelection || (FracReg.AOSelection = {}));
})(FracReg || (FracReg = {}));
