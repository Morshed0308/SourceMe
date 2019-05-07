import { OnInit, Component } from "@angular/core";
import { Category } from "../../models/category";
import { DataService } from "../../services/dataService";
import { forEach } from "@angular/router/src/utils/collection";
import { Router } from "@angular/router";

@Component({
    selector: "category-list",
    templateUrl: "category.component.html",
    styleUrls: ["category.component.css"]
})

export class CategoryList implements OnInit {
    
    constructor(private data: DataService, private router: Router) {
        
    }

    public showSaveMessage: boolean = false;
    public categories: Category[] = [];
    public subCategories: Category[] = [];

    ngOnInit(): void {
        if (!this.data.isLoggedIn) {
            this.router.navigate(['/login']);
        }

        this.loadCategories();
    }

    async loadCategories() {
        var categories = await this.data.loadCategories();
        this.categories = categories;
        this.subCategories = this.data.subCategories;
    }
    
    onChecked(id: number){
        let found = this.subCategories.find(o => o.id === id);
        found.isChecked = !found.isChecked;
    }

    saveSelection() {
        this.categories.forEach(catg => {
            catg.subCategories.forEach(subCatg => {
                let found = this.subCategories.find(o => o.id === subCatg.id);
                subCatg.isChecked = found.isChecked;
            })
        });

        localStorage.removeItem('categories');
        localStorage.setItem('categories', JSON.stringify(this.categories));
        localStorage.removeItem('subCategories');

        this.showSaveMessage = true;
        setTimeout(() => {
            this.showSaveMessage = false;
        }, 5000)
    }
}