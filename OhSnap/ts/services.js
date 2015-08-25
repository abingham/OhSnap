/// <reference path="../Scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../Scripts/typings/angularjs/angular-resource.d.ts" />
/// <reference path="../Scripts/typings/underscore/underscore.d.ts" />
var OhSnap;
(function (OhSnap) {
    var Service;
    (function (Service) {
        var ohSnapServices = angular.module('OhSnapServices', ['ngResource']);
        ohSnapServices.factory('Patients', ['$resource',
            function ($resource) {
                return $resource('/api/PatientsAPI/:id');
            }]);
        var parseInjury = function (injury) {
            injury.InjuryDate = new Date(injury.InjuryDate);
            return injury;
        };
        ohSnapServices.factory('Injuries', ['$resource',
            function ($resource) {
                return $resource('/api/InjuriesAPI/:id', {}, {
                    query: {
                        method: 'GET',
                        isArray: true,
                        transformResponse: function (data, headersGetter) {
                            return _.map(JSON.parse(data), parseInjury);
                        }
                    },
                    byUser: {
                        method: 'GET',
                        isArray: true,
                        url: '/api/InjuriesAPI/ByUser/:id',
                        transformResponse: function (data, headersGetter) {
                            return _.map(JSON.parse(data), parseInjury);
                        }
                    },
                    get: {
                        method: 'GET',
                        transformResponse: function (data, headersGetter) {
                            return parseInjury(JSON.parse(data));
                        }
                    }
                });
            }]);
        ohSnapServices.factory('Fractures', ['$resource',
            function ($resource) {
                return $resource('/api/FracturesAPI/:id', {}, {
                    byInjury: {
                        method: 'GET',
                        isArray: true,
                        url: '/api/FracturesAPI/ByUser/:id',
                        transformResponse: function (data, headersGetter) {
                            return _.map(JSON.parse(data), parseInjury);
                        }
                    }
                });
            }]);
    })(Service = OhSnap.Service || (OhSnap.Service = {}));
})(OhSnap || (OhSnap = {}));
//# sourceMappingURL=services.js.map