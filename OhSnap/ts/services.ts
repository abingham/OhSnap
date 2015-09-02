/// <reference path="../Scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../Scripts/typings/angularjs/angular-resource.d.ts" />
/// <reference path="../Scripts/typings/underscore/underscore.d.ts" />

module OhSnap.Service {

    var ohSnapServices: angular.IModule = angular.module(
        'OhSnapServices',
        ['ngResource']);

    ohSnapServices.factory(
        'Patients',
        ['$resource',
         function($resource: angular.resource.IResourceService){
             return $resource('/api/PatientsAPI/:id');
         }]);

    var injuryFromJSON = (injury) => {
        // TODO: This is a bit dicey. We're just parsing out the date portion (YYYY-MM-DD) of a date-time string.
        injury.InjuryDate = injury.InjuryDate.substring(0, 10);
        return injury;
    };

    var injuryToJSON = (injury) => {
        injury.InjuryDate = injury.InjuryDate + "T00:00:00";
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
                                 injuryFromJSON);
                         }
                     },
                     save: {
                         method: 'POST',
                         transformRequest: (data, headersGetter) => {
                             return JSON.stringify(injuryToJSON(data));
                         }
                     },
                     byUser: {
                         method: 'GET',
                         isArray: true,
                         url: '/api/InjuriesAPI/ByUser/:id',
                         transformResponse: (data, headersGetter) => {
                             return _.map<any, any>(
                                 JSON.parse(data),
                                 injuryFromJSON);
                         }
                     },
                     get: {
                         method: 'GET',
                         transformResponse: (data, headersGetter) => {
                             return injuryFromJSON(JSON.parse(data));
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
                            url: '/api/FracturesAPI/ByUser/:id'
                        }                        
                    });
            }]);
}
