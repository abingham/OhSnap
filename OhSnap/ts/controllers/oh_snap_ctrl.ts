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
    };

    export interface OhSnapScope extends angular.IScope {
        patients: Patient[]; // all available patients
        selection: OhSnapSelection;

        formatPatient: (patient: Patient) => string;
    };

    // The main controller for FracReg.
    //

    ohSnapControllers.controller(
        'OhSnapCtrl',
        ['$scope',
            'Patients',
            ($scope: OhSnapScope,
                Patients) => {
                $scope.patients = [];
                $scope.selection = {
                    patient: null
                };

                // load the patients from the server
                var patients = Patients.query({}, () => {
                    $scope.patients = patients;
                });

                $scope.formatPatient = (patient: Patient) => {
                    return patient.LastName + ", " + patient.FirstName + " (" + patient.Age + ")";
                };
            }
        ]);
}
