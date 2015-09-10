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
             return $resource('/api/Patients/:id');
         }]);
        
    ohSnapServices.factory(
        'Injuries',
        ['$resource',
         function($resource: angular.resource.IResourceService){
             return $resource(
                 '/api/Injuries/:id',
                 {},
                 { 
                     query: {
                         method: 'GET',
                         isArray: true,
                         transformResponse: (data, headersGetter) => { 
                             return JSON.parse(data);
                         }
                     },
                     save: {
                         method: 'POST',
                         transformRequest: (data, headersGetter) => {
                             return JSON.stringify(data);
                         }
                     },
                     byUser: {
                         method: 'GET',
                         isArray: true,
                         url: '/api/Injuries/ByUser/:id',
                         transformResponse: (data, headersGetter) => {
                             return JSON.parse(data);
                         }
                     },
                     get: {
                         method: 'GET',
                         transformResponse: (data, headersGetter) => {
                             return JSON.parse(data);
                         }
                     }
                 });
            }]);

    ohSnapServices.factory(
        'Fractures',
        ['$resource',
            function ($resource: angular.resource.IResourceService) {
                return $resource(
                    '/api/Fractures/:id',
                    {},
                    {                         
                        byInjury: {
                            method: 'GET',
                            isArray: true,
                            url: '/api/Fractures/ByUser/:id'
                        }                        
                    });
            }]);

    ohSnapServices.factory(
        'AOCodes',
        ['$resource',
            function ($resource: angular.resource.IResourceService) {
                return $resource('/api/AOCodes/');
            }]);
}
