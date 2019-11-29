import { AuthorModelItem } from '../author/author.model.item';

export class PrintingEditionModelItem {
    constructor(
        public id?: number,
        public title?: string,
        public description?: string,
        public authors?: Array<AuthorModelItem>,
        public price?: number,
        public currency?: string,
        public type?: string
    ) { }
}