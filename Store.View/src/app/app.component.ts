import { Component } from '@angular/core';
import { PageAnimation } from './shared/helpers/page-animation';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  animations: [ PageAnimation ]
})
export class AppComponent {
  title = 'education-app';
}
