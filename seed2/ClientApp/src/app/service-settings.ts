import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

@Injectable()
export class ServiceSettings {

    public baseURL: string;
    public Idle: number;
    public DefaultPageSize: number;
    public Version: string;
    public static get URLSeparator(): string { return "/"; }


    constructor(private httpClient: Http) { }

    async load() : Promise<any>{
        console.log('load');
        const promise = this.httpClient.get(window.location.origin + '/api/ServiceSettings')
            .toPromise()
            .then(settings => {
                console.log(`Settings from API: `, settings);

                return settings;
            });

        return promise;
    }
}
