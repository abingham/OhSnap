/// <reference path="../../Scripts/typings/angularjs/angular.d.ts"/>
/// <reference path="../../Scripts/typings/ui-grid/ui-grid.d.ts" />
/// <reference path="datatypes.ts"/>

'use strict';

/* Controllers */

module OhSnap.Controller {

    var manageFormsControllers: angular.IModule = angular.module(
        'manageFormsControllers',
        [
            'ohSnapServices'
        ]);

    interface Selection {
        injury: Injury;
    };

    interface ManageFormsScope extends angular.IScope {
        patient: Patient; // The current patient.
        injuries: Injury[];
        fracture: Fracture[];

        injuryGridOptions: uiGrid.IGridOptions;
        fractureGridOptions: uiGrid.IGridOptions;
    };

    manageFormsControllers.controller(
        'ManageFormsCtrl',
        ['$scope',
         'patientID',
         'Patients',
         'Injuries',
         ($scope: ManageFormsScope,
          patientID,
          Patients,
          Injuries) => {
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
                 ]  
             };

             $scope.fractureGridOptions = {
                 columnDefs: [
                     { name: 'AO Code', field: 'AOCode' }                     
                 ]
             };
         }
        ]);
    }
