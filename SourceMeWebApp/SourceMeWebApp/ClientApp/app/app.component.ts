import { Component } from '@angular/core';
import { DataService } from './services/dataService';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';

@Component({
    selector: 'source-me',
  templateUrl: "./app.component.html",
  styleUrls: []
})
export class AppComponent {
    public activeComponent: string = "";
    constructor(public data: DataService, private router: Router, private route: ActivatedRoute) {
        router.events.subscribe((val) => {
            // see also 
            let navEnd = val instanceof NavigationEnd;
            if (navEnd) {
                if (this.router.url === '/news-feed') {
                    this.activeComponent = "news-feed";
                }
                else if (this.router.url === '/categories'){
                    this.activeComponent = "categories";
                }
            }
        });
    }
}
