import { Component, OnInit } from '@angular/core';
import { PrintingEditionService } from '../../../../shared/services';
import { Router, ActivatedRoute } from '@angular/router';
import { PrintingEditionModelItem } from '../../../../shared/models';
import { Constants } from '../../../../shared/constants/constants';

@Component({
    selector: 'app-details',
    templateUrl: './details.component.html',
    styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {
    private printingEditionModel: PrintingEditionModelItem;
    private types: Array<string>;

    constructor(private route: ActivatedRoute, private router: Router, private printingEditionService: PrintingEditionService) {
        this.printingEditionModel = new PrintingEditionModelItem();
        this.types = Constants.enumsAttributes.printingEditionTypes;
    }

    ngOnInit() {
        const id = this.route.snapshot.paramMap.get('id');

        this.printingEditionService.getById(parseInt(id, 10))
            .subscribe(data => {
                this.printingEditionModel = data;
            });
    }

}
