/// <reference path="../Scripts/typings/angularjs/angular.d.ts"/>
/// <reference path="../Scripts/typings/angularjs/angular-resource.d.ts"/>
/// <reference path="../Scripts/typings/underscore/underscore.d.ts"/>

'use strict';

/* Controllers */

module OhSnap.Controller {

    var ohSnapControllers: angular.IModule = angular.module(
            'ohSnapControllers',
            [
                'ohSnapServices'
            ]);

    export interface Patient {
                ID: string;
                FirstName: string;
                LastName: string;
                Age: number;
    };

    export interface Injury {
        ID: string;
        PatientID: string;
        AOCode: string;
        InjuryDate: Date;
        InjuryHour: number;
    };

    export interface AddProcedureScope extends angular.IScope {
        patients: Patient[];
        injuries: Injury[];

        addPatient: (FirstName: string, LastName: string, Age: number) => void;
        deletePatient: (id: string) => void;
        findPatient: (id: string) => Patient;
        formatPatient: (patient: Patient) => string;

        addInjury: (PatientID: string, AOCode: string, InjuryDate: Date, InjuryHour: number) => void;
        deleteInjury: (id: string) => void;
    };

    // The main controller for FracReg.
    //
    // Coordinates the primary data in the application, and maintains
    // data retrieved from various providers.
    ohSnapControllers.controller(
        'AddProcedureCtrl',
        ['$scope',
         'Patients',
         'Injuries',
         ($scope: AddProcedureScope,
          Patients,
          Injuries) => {
                $scope.patients = [];
                $scope.injuries = [];

                var patients = Patients.query({}, () => {
                  $scope.patients = patients;
        });

        var injuries = Injuries.query({}, () => {
                $scope.injuries = injuries;
        });

        $scope.addPatient = (FirstName: string, LastName: string, Age: number) => {
                var patient = new Patients({
                        FirstName: FirstName,
                                LastName: LastName,
                                Age: Age
                        });

                        patient.$save((p) => {
                                $scope.patients.push(patient);
                        });
        };

        $scope.deletePatient = (id: string) => {
                Patients.delete({id: id});
                $scope.patients = _.filter($scope.patients, function(p){ return p.ID != id; });
                $scope.injuries = _.filter($scope.injuries, function(i){ return i.PatientID != id; });
        };

        $scope.findPatient = (id: string) => {
                return _.find<Patient>(patients, (p) => { return p.ID == id; });
        };

        $scope.formatPatient = (patient: Patient) => {
                return patient.LastName + ", " + patient.FirstName + " (" + patient.Age + ")";
        };

        $scope.addInjury = (PatientID: string, AOCode: string, InjuryDate: Date, InjuryHour: number) => {
                var injury = new Injuries({
                        PatientID: PatientID,
                        AOCode: AOCode,
                        InjuryDate: InjuryDate,
                        InjuryHour: InjuryHour,
                        ID: null
                });

                        injury.$save((p) => {
                                // It seems that the $resource machinery replaces the Date object at InjuryDate with a stringified version. I guess this is a side-effect of JSON encoding. So we have to translate it back.
                                injury.InjuryDate = new Date(p.InjuryDate);
                                $scope.injuries.push(injury);
                        });
        };

        $scope.deleteInjury = (id: string) => {
                Injuries.delete({id: id});
                $scope.injuries = _.filter($scope.injuries, function(i){ return i.ID != id; });
        };
          }]);

        export interface NewPatientData {
                FirstName: string;
        LastName: string;
        Age: number;
        };

    export interface NewPatientScope extends AddProcedureScope {
                newPatient: NewPatientData;
        clear: () => void;
        addPatientAndClear: () => void;
    };

    // Controller for the new-patient form. Essentially just placeholders for the values on the form.
        ohSnapControllers.controller(
                'NewPatientCtrl',
                ['$scope',
                 ($scope: NewPatientScope) => {
                        $scope.clear = () => {
                                $scope.newPatient = {
                                        FirstName: "",
                                        LastName: "",
                                        Age: null
                                }
                        }

                        $scope.addPatientAndClear = () => {
                                $scope.addPatient($scope.newPatient.FirstName,
                                                                  $scope.newPatient.LastName,
                                                                  $scope.newPatient.Age);
                                $scope.clear();
                        };

                        $scope.clear();
                 }
                ]);

        export interface NewInjuryData {
                AOCode: string;
                InjuryDate: Date;
                InjuryHour: number;
                Patient: Patient;
        }

        export interface NewInjuryScope extends AddProcedureScope {
                newInjury: NewInjuryData;
                clear: () => void;
                addInjuryAndClear: () => void;
        }

        // Controller for the new-patient form. Essentially just placeholders for the values on the form.
        ohSnapControllers.controller(
                'NewInjuryCtrl',
                ['$scope',
                 ($scope: NewInjuryScope) => {
                        $scope.clear = () => {
                                $scope.newInjury = {
                                        AOCode: "",
                                        InjuryDate: null,
                                        InjuryHour: null,
                                        Patient: null
                                }
                        }

                        $scope.clear();

                        $scope.addInjuryAndClear = () => {
                                $scope.addInjury($scope.newInjury.Patient.ID,
                                                             $scope.newInjury.AOCode,
                                                             $scope.newInjury.InjuryDate,
                                                             $scope.newInjury.InjuryHour);
                                $scope.clear();
                        }
                 }
                ]);
}
