import { OnInit, Component } from "@angular/core";
import { FeedItem } from "../models/feedItem";
import { DataService } from "../services/dataService";
import { Router } from "@angular/router";
import { Category } from "../models/category";

@Component({
    selector: "news-feed",
    templateUrl: "feed.component.html",
    styleUrls: ["feed.component.css"]
})

export class NewsFeed implements OnInit {

    constructor(private data: DataService, private router: Router) {
        //this.feedItems = data.feedItems;
    }

    public feedItems: FeedItem[] = [];
    public subCategories: Category[] = [];
    public loadedCategoryIndex: number = 0;
    public loadedChannelIndex: number = 0;
    public isLoading: boolean = false;
    selector: string = '.feed-container';

    ngOnInit(): void {
        if (!this.data.isLoggedIn) {
            this.router.navigate(['/login']);
        }
        
        //let categories = this.data.loadCategories();
        
        this.loadData();
    }

    async loadData() {
        let categories = await this.data.loadCategories();
        this.data.subCategories.forEach(a => {
            if (a.isChecked) {
                this.subCategories.push(a);
            }
        });
        this.feedItems = [];
        this.loadNewsFeed();
    }

    loadNewsFeed() {

        let currentCategory = this.subCategories[this.loadedCategoryIndex];
        //&& this.loadedChannelIndex <= currentCategory.noOfChannels
        if (this.loadedCategoryIndex >= this.subCategories.length) {
            return;
        }
        this.isLoading = true;
        
        this.data.loadNewsFeed(currentCategory.id, this.loadedChannelIndex)
            .subscribe((response: FeedItem[]) => {
                //increment channel or category index
                this.loadedChannelIndex++;
                if (this.loadedChannelIndex < currentCategory.noOfChannels) {
                    
                }
                else {
                    this.loadedCategoryIndex++;
                    this.loadedChannelIndex = 0;
                }
                
                if (response.length === 0 && this.loadedCategoryIndex < this.subCategories.length) {
                    this.loadNewsFeed();
                }
                response.forEach(item => {
                    this.feedItems.push(item);
                });
                
                this.isLoading = false;
            });
    }

    //onNav() {
    //    this.router.navigate(["categories"]);
    //}

    onImageFallbackClick(url: string) {
        window.open(url, "_blank");
    }
    
    onScroll() {
        this.loadNewsFeed();
    }
}