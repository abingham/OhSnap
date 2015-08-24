﻿/// <reference path="../../Scripts/typings/angularjs/angular.d.ts"/>
/// <reference path="../../Scripts/typings/ui-grid/ui-grid.d.ts" />
/// <reference path="datatypes.ts"/>

'use strict';

/* Controllers */

module OhSnap.Controller {

    var manageFormsControllers: angular.IModule = angular.module(
        'ManageFormsControllers',
        [
            'OhSnapServices'
        ]);

    interface Selection {
        injury: Injury;
        fracture: Fracture;
    };

    interface ManageFormsScope extends angular.IScope {
        patient: Patient; // The current patient.
        injuries: Injury[];
        fractures: Fracture[];

        injuryGridOptions: uiGrid.IGridOptions;
        fractureGridOptions: uiGrid.IGridOptions;
    };

    manageFormsControllers.controller(
        'ManageFormsCtrl',
        ['$scope',
         'patientID',
         'Patients',
         'Injuries',
         'Fractures',
         ($scope: ManageFormsScope,
          patientID,
          Patients,
          Injuries,
          Fractures) => {
             // load the patient from the server
             var patient = Patients.get({id: patientID}, () => {
                 $scope.patient = patient;

                 var injuries = Injuries.byUser({ id: patient.ID }, () => {
                     $scope.injuries = injuries;
                     $scope.injuryGridOptions.data = injuries;
                 });
             });

             $scope.injuryGridOptions = {                 
                 columnDefs: [
                     { name: 'Date', field: 'InjuryDate' },
                     { name: 'Hour', field: 'InjuryHour' }                     
                 ],
                 enableFullRowSelection: true
             };

             $scope.injuryGridOptions.onRegisterApi = function (gridApi) {                                
                 gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                     var injuryID = row.entity.ID;
                     var fractures = Fractures.byInjury({ id: injuryID }, () => {
                         $scope.fractures = fractures;
                         $scope.fractureGridOptions.data = fractures;
                     });
                     
                 });                 
             };

             $scope.fractureGridOptions = {
                 columnDefs: [
                     { name: 'AO Code', field: 'AOCode' }                     
                 ]
             };
         }
        ]);
    }
