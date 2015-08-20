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
        AOCode: string;
        InjuryDate: Date;
        InjuryHour: number;
    };
}