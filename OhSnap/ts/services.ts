/// <reference path="../Scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../Scripts/typings/angularjs/angular-resource.d.ts" />
/// <reference path="../Scripts/typings/underscore/underscore.d.ts" />

module OhSnap.Service {

    var ohSnapServices: angular.IModule = angular.module(
        'ohSnapServices',
        ['ngResource']);

    ohSnapServices.factory(
        'Patients',
        ['$resource',
         function($resource: angular.resource.IResourceService){
             return $resource('/api/PatientsAPI/:id');
         }]);

    var parseInjury = (injury) => {
        injury.InjuryDate = new Date(injury.InjuryDate);
        return injury;
    };

    ohSnapServices.factory(
        'Injuries',
        ['$resource',
         function($resource: angular.resource.IResourceService){
             return $resource(
                 '/api/InjuriesAPI/:id',
                 {},
                 { // This takes care of parsing date string representations into actual Date instances.
                     query: {
                         method: 'GET',
                         isArray: true,
                         transformResponse: (data, headersGetter) => {
                             return _.map<any, any>(
                                 JSON.parse(data),
                                 parseInjury);
                         }
                     },
                     byUser: {
                         method: 'GET',
                         isArray: true,
                         url: '/api/InjuriesAPI/ByUser/:id',
                         transformResponse: (data, headersGetter) => {
                             return _.map<any, any>(
                                 JSON.parse(data),
                                 parseInjury);
                         }
                     },
                     get: {
                         method: 'GET',
                         transformResponse: (data, headersGetter) => {
                             return parseInjury(JSON.parse(data));
                         }
                     }
                 });
            }]);

    ohSnapServices.factory(
        'Fractures',
        ['$resource',
            function ($resource: angular.resource.IResourceService) {
                return $resource(
                    '/api/FracturesAPI/:id',
                    {},
                    {                         
                        byInjury: {
                            method: 'GET',
                            isArray: true,
                            url: '/api/FracturesAPI/ByUser/:id',
                            transformResponse: (data, headersGetter) => {
                                return _.map<any, any>(
                                    JSON.parse(data),
                                    parseInjury);
                            }
                        }                        
                    });
            }]);
}
