/// <reference path="../Scripts/typings/angularjs/angular.d.ts"/>
/// <reference path="../Scripts/typings/angularjs/angular-resource.d.ts"/>
/// <reference path="../Scripts/typings/underscore/underscore.d.ts"/>
'use strict';
/* Controllers */
var OhSnap;
(function (OhSnap) {
    var Controller;
    (function (Controller) {
        var ohSnapControllers = angular.module('ohSnapControllers', [
            'ohSnapServices'
        ]);
        ;
        ;
        ;
        ;
        ;
        // The main controller for FracReg.
        //
        ohSnapControllers.controller('OhSnapCtrl', ['$scope',
            'Patients',
            function ($scope, Patients) {
                $scope.patients = [];
                $scope.selection = {
                    patient: null
                };
                // load the patients from the server
                var patients = Patients.query({}, function () {
                    $scope.patients = patients;
                });
                $scope.formatPatient = function (patient) {
                    return patient.LastName + ", " + patient.FirstName + " (" + patient.Age + ")";
                };
            }
        ]);
        // Coordinates the primary data in the application, and maintains
        // data retrieved from various providers.
        ohSnapControllers.controller('AddProcedureCtrl', ['$scope',
            'Patients',
            'Injuries',
            function ($scope, Patients, Injuries) {
                $scope.patients = [];
                $scope.injuries = [];
                var patients = Patients.query({}, function () {
                    $scope.patients = patients;
                });
                var injuries = Injuries.query({}, function () {
                    $scope.injuries = injuries;
                });
                $scope.addPatient = function (FirstName, LastName, Age) {
                    var patient = new Patients({
                        FirstName: FirstName,
                        LastName: LastName,
                        Age: Age
                    });
                    patient.$save(function (p) {
                        $scope.patients.push(patient);
                    });
                };
                $scope.deletePatient = function (id) {
                    Patients.delete({ id: id });
                    $scope.patients = _.filter($scope.patients, function (p) { return p.ID != id; });
                    $scope.injuries = _.filter($scope.injuries, function (i) { return i.PatientID != id; });
                };
                $scope.findPatient = function (id) {
                    return _.find(patients, function (p) { return p.ID == id; });
                };
                $scope.formatPatient = function (patient) {
                    return patient.LastName + ", " + patient.FirstName + " (" + patient.Age + ")";
                };
                $scope.addInjury = function (PatientID, AOCode, InjuryDate, InjuryHour) {
                    var injury = new Injuries({
                        PatientID: PatientID,
                        AOCode: AOCode,
                        InjuryDate: InjuryDate,
                        InjuryHour: InjuryHour,
                        ID: null
                    });
                    injury.$save(function (p) {
                        // It seems that the $resource machinery replaces the Date object at InjuryDate with a stringified version. I guess this is a side-effect of JSON encoding. So we have to translate it back.
                        injury.InjuryDate = new Date(p.InjuryDate);
                        $scope.injuries.push(injury);
                    });
                };
                $scope.deleteInjury = function (id) {
                    Injuries.delete({ id: id });
                    $scope.injuries = _.filter($scope.injuries, function (i) { return i.ID != id; });
                };
            }]);
        ;
        ;
        // Controller for the new-patient form. Essentially just placeholders for the values on the form.
        ohSnapControllers.controller('NewPatientCtrl', ['$scope',
            function ($scope) {
                $scope.clear = function () {
                    $scope.newPatient = {
                        FirstName: "",
                        LastName: "",
                        Age: null
                    };
                };
                $scope.addPatientAndClear = function () {
                    $scope.addPatient($scope.newPatient.FirstName, $scope.newPatient.LastName, $scope.newPatient.Age);
                    $scope.clear();
                };
                $scope.clear();
            }
        ]);
        // Controller for the new-patient form. Essentially just placeholders for the values on the form.
        ohSnapControllers.controller('NewInjuryCtrl', ['$scope',
            function ($scope) {
                $scope.clear = function () {
                    $scope.newInjury = {
                        AOCode: "",
                        InjuryDate: null,
                        InjuryHour: null,
                        Patient: null
                    };
                };
                $scope.clear();
                $scope.addInjuryAndClear = function () {
                    $scope.addInjury($scope.newInjury.Patient.ID, $scope.newInjury.AOCode, $scope.newInjury.InjuryDate, $scope.newInjury.InjuryHour);
                    $scope.clear();
                };
            }
        ]);
    })(Controller = OhSnap.Controller || (OhSnap.Controller = {}));
})(OhSnap || (OhSnap = {}));
//# sourceMappingURL=controllers.js.map