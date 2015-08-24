/// <reference path="../../Scripts/typings/angularjs/angular.d.ts"/>
/// <reference path="../../Scripts/typings/angularjs/angular-resource.d.ts"/>
/// <reference path="../../Scripts/typings/underscore/underscore.d.ts"/>
/// <reference path="datatypes.ts"/>
/// <reference path="module.ts"/>

'use strict';

/* Controllers */

module OhSnap.Controller {

    export interface OhSnapSelection {
        patient: Patient;
        injury: Injury;
    };

    export interface OhSnapScope extends angular.IScope {
        patients: Patient[]; // all available patients
        injuries: Injury[];
        selection: OhSnapSelection;

        formatPatient: (patient: Patient) => string;
        formatInjury: (Injury: Injury) => string;
    };

    // The main controller for FracReg.
    //

    ohSnapControllers.controller(
        'OhSnapCtrl',
        ['$scope',
         'Patients',
         'Injuries',
         ($scope: OhSnapScope,
          Patients,
          Injuries) => {
                $scope.patients = [];
                $scope.injuries = [];
                $scope.selection = {
                    patient: null,
                    injury: null
                };

                // load the patients from the server
                var patients = Patients.query({}, () => {
                    $scope.patients = patients;
                });

                var injuries = Injuries.query({}, () => {
                    $scope.injuries = injuries;
                });

                $scope.formatPatient = (patient: Patient) => {
                    return patient.LastName + ", " + patient.FirstName + " (" + patient.Age + ")";
                };

                $scope.formatInjury = (injury: Injury) => {
                    return injury.InjuryDate + " @ " + injury.InjuryHour
                };
            }
        ]);
}
