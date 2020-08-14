import { ServiceSettings } from '../../service-settings';

export class LoopbackFilter {
    public where: { [key: string]: string; };
    public limit: number;
    public skip: number;
    public order: string;
    public ascending:boolean;
    public fields: string[];
    constructor(configuration: ServiceSettings) {
        this.where = {};
        this.limit = configuration.DefaultPageSize;
        this.skip=0;
    }
}
