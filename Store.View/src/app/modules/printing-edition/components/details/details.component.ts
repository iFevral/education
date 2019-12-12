import { Component, OnInit } from '@angular/core';
import { PrintingEditionService } from '../../../../shared/services';
import { Router, ActivatedRoute } from '@angular/router';
import { PrintingEditionModelItem } from '../../../../shared/models';

@Component({
    selector: 'app-details',
    templateUrl: './details.component.html',
    styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {
    private printingEditionModel: PrintingEditionModelItem ;
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private printingEditionService: PrintingEditionService) {
            this.printingEditionModel = new PrintingEditionModelItem();
         }

    ngOnInit() {
        const id = this.route.snapshot.paramMap.get('id');

        this.printingEditionService.getById(parseInt(id, 10))
            .subscribe(data => {
                this.printingEditionModel = data;
            });

        console.log(this.printingEditionModel)
    }

}
