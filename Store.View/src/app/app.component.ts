import { Component } from '@angular/core';
import { slideInAnimation } from './shared/helpers/page-animation';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  animations: [ slideInAnimation ]
})
export class AppComponent {
  title = 'education-app';
}
