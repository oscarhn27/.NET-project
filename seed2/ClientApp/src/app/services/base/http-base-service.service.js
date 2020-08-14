"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var http_1 = require("@angular/common/http");
var HttpBaseServiceService = /** @class */ (function () {
    function HttpBaseServiceService(httpClient, endpoint, endpointFilterAction, configuration) {
        this.httpClient = httpClient;
        this.headers = new http_1.HttpHeaders();
        this.apiUrl = "";
        this.apiUrl = configuration.baseURL;
        this.endpoint = endpoint;
        this.configuration = configuration;
        this.endpointFilterAction = endpointFilterAction;
        this.headers = this.headers.set('Content-Type', 'application/json');
        this.headers = this.headers.set('Accept', 'application/json');
        // this.headers = this.headers.set('Authorization', 'Bearer ' + this.getJWToken());
    }
    /*getUrl(): Observable<string> {
        this.httpClient.get(window.location.origin + '/api/ServiceSettings').subscribe(res => {
            var aux: any = JSON.parse(JSON.stringify(res));
            this.apiUrl = aux.baseURL;
            return of(this.apiUrl);
        });
    }*/
    HttpBaseServiceService.prototype.getAll = function () {
        /*if (!this.apiUrl || this.apiUrl == "") {
            console.log('entro al subscribe de getAll');
            this.httpClient.get(window.location.origin + '/api/ServiceSettings').subscribe(res => {
                var aux: any = JSON.parse(JSON.stringify(res));
                this.apiUrl = aux.baseURL;
                if (!this.endpoint || this.endpoint == "" || !this.endpointFilterAction || this.endpointFilterAction == "") {
                    console.error("HttpBaseServiceService error. No se ha establecido el actioncontroller.");
                    return;
                }
                const mergedUrl = this.apiUrl + this.endpoint + "/" + this.endpointFilterAction + "?loopback=" + JSON.stringify(this.filter);
                return this.httpClient.get<T>(mergedUrl, { headers: this.headers, observe: 'response' });
            });
        }
        else {
            if (!this.endpoint || this.endpoint == "" || !this.endpointFilterAction || this.endpointFilterAction == "") {
                console.error("HttpBaseServiceService error. No se ha establecido el actioncontroller.");
                return;
            }
            const mergedUrl = this.apiUrl + this.endpoint + "/" + this.endpointFilterAction + "?loopback=" + JSON.stringify(this.filter);
            return this.httpClient.get<T>(mergedUrl, { headers: this.headers, observe: 'response' });
        }*/
        if (!this.endpoint || this.endpoint == "" || !this.endpointFilterAction || this.endpointFilterAction == "") {
            console.error("HttpBaseServiceService error. No se ha establecido el actioncontroller.");
            return;
        }
        var mergedUrl = this.apiUrl + this.endpoint + "/" + this.endpointFilterAction + "?loopback=" + JSON.stringify(this.filter);
        return this.httpClient.get(mergedUrl, { headers: this.headers, observe: 'response' });
    };
    HttpBaseServiceService.prototype.getSingle = function (id) {
        return this.httpClient.get(this.apiendpoint() + "/" + id);
    };
    HttpBaseServiceService.prototype.add = function (toAdd) {
        return this.httpClient.post(this.endpoint, toAdd, { headers: this.headers });
    };
    HttpBaseServiceService.prototype.update = function (url, toUpdate) {
        return this.httpClient.put(url, toUpdate, { headers: this.headers });
    };
    HttpBaseServiceService.prototype.delete = function (url) {
        return this.httpClient.delete(url);
    };
    HttpBaseServiceService.prototype.deleteBody = function (url) {
        // return this.httpClient.delete(url);
        return this.httpClient.request('delete', url, { body: this.filter });
    };
    HttpBaseServiceService.prototype.prepareGetAllFilter = function () {
        this.filter.limit = this.configuration.DefaultPageSize;
    };
    HttpBaseServiceService.prototype.setPagination = function (skip, limit) {
        this.filter.skip = skip;
        this.filter.limit = limit;
    };
    HttpBaseServiceService.prototype.apiendpoint = function () {
        var _this = this;
        if (!this.apiUrl || this.apiUrl == "") {
            this.httpClient.get(window.location.origin + '/api/ServiceSettings').subscribe(function (res) {
                var aux = JSON.parse(JSON.stringify(res));
                _this.apiUrl = aux.baseURL;
                return _this.apiUrl + _this.endpoint;
            });
        }
        else {
            return this.apiUrl + this.endpoint;
        }
    };
    HttpBaseServiceService.prototype.getJWToken = function () { return localStorage.getItem('jwt'); };
    HttpBaseServiceService.prototype.getSortingValue = function (previousorder, currentorder, newValue) {
        if (currentorder == previousorder && currentorder == newValue) {
            return undefined;
        }
        if (!previousorder && currentorder == newValue) {
            return newValue;
        }
        if (previousorder != newValue || currentorder != newValue) {
            return newValue;
        }
        if (currentorder == newValue) {
            return newValue;
        }
    };
    HttpBaseServiceService.prototype.getSortingAscending = function (previousorder, currentorder, newValue) {
        if (currentorder == previousorder && currentorder == newValue) {
            return undefined;
        }
        if (!previousorder && currentorder == newValue) {
            return false;
        }
        if (previousorder != newValue || currentorder != newValue) {
            return true;
        }
        if (currentorder == newValue) {
            return false;
        }
    };
    HttpBaseServiceService.prototype.getPrevious = function (previousorder, currentorder, newValue) {
        if (currentorder == previousorder && currentorder == newValue) {
            return undefined;
        }
        if (!previousorder && currentorder == newValue) {
            return currentorder;
        }
        if (previousorder != newValue || currentorder != newValue) {
            return currentorder;
        }
        if (currentorder == newValue) {
            return currentorder;
        }
    };
    HttpBaseServiceService.prototype.getCurrent = function (previousorder, currentorder, newValue) {
        if (currentorder == previousorder && currentorder == newValue) {
            return undefined;
        }
        if (!previousorder && currentorder == newValue) {
            return newValue;
        }
        if (previousorder != newValue || currentorder != newValue) {
            return newValue;
        }
        if (currentorder == newValue) {
            return newValue;
        }
    };
    return HttpBaseServiceService;
}());
exports.HttpBaseServiceService = HttpBaseServiceService;
//# sourceMappingURL=http-base-service.service.js.map