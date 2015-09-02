/// <reference path="../../Scripts/typings/angularjs/angular.d.ts"/>
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
        patientID: string; // The current patient-id.
        injuries: Injury[];
        fractures: Fracture[];

        injuryGridOptions: uiGrid.IGridOptions;
        fractureGridOptions: uiGrid.IGridOptions;
    };

    interface InjuryResource extends angular.resource.IResourceClass<Injury> {
        byUser: (params: { id: string }, success: Function) => Injury[];
    };

    interface FractureResource extends angular.resource.IResourceClass<Fracture> {
        byInjury: (params: { id: string }, success: Function) => Fracture[];
    };

    manageFormsControllers.controller(
        'ManageFormsCtrl',
        ['$scope',
         'patientID',
         'Patients',
         'Injuries',
         'Fractures',
         ($scope: ManageFormsScope,
          patientID: string,
          Patients: angular.resource.IResourceClass<Patient>,
          Injuries: InjuryResource,
          Fractures: FractureResource) => {
             $scope.injuryGridOptions = {
                 columnDefs: [
                     { name: 'Date', field: 'InjuryDate.substring(0, 10)' },
                     { name: 'Hour', field: 'InjuryHour' }
                 ],
                 enableFullRowSelection: true
             };
             
             $scope.patientID = patientID;

             var injuries = Injuries.byUser({ id: $scope.patientID }, () => {
                 $scope.injuries = injuries;
                 $scope.injuryGridOptions.data = $scope.injuries;
             });                          

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

    interface AddInjuryScope extends ManageFormsScope {
        date: any; // TODO: Correct type?
        hour: number;

        submissionDisabled: () => boolean;
        submitInjury: () => void;
    };

    manageFormsControllers.controller(
        'AddInjuryCtrl',
        ['$scope',
         '$location',
         'Injuries',
         ($scope: AddInjuryScope,
          $location: angular.ILocationService,
          Injuries: InjuryResource) => {
                $scope.date = null;
                $scope.hour = null;

                $scope.submissionDisabled = () => {
                    return $scope.date == null || $scope.hour == null;
                };

                $scope.submitInjury = () => {
                    console.log($scope.date);
                    console.log($scope.hour);

                    var newInjury = new Injuries({
                        InjuryDate: $scope.date,
                        InjuryHour: $scope.hour,
                        PatientID: $scope.patientID
                    });

                    // TODO: It's at least in principle possible that injuries doesn't yet exist. Do we need to deal with that?
                    $scope.injuries.push(newInjury);
                    Injuries.save(newInjury);

                    $location.path('#/');
                };

            }
        ]);
    }
