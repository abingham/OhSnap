'use strict';

/* Controllers */

module OhSnap.Controller {

    export interface Patient {
        ID: string;
        FirstName: string;
        LastName: string;
        Age: number;
    };

    export interface Injury {
        ID: string;
        PatientID: string;
        InjuryDate: Date;
        InjuryHour: number;
    };

    export interface Fracture {
        ID: string;
        InjuryID: string;
        AOCode: string;
    };
}