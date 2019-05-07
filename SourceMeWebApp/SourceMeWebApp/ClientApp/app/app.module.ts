import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { InfiniteScrollModule } from 'ngx-infinite-scroll';

import { AppComponent } from './app.component';
import { NewsFeed } from './feed/feed.component';
import { DataService } from './services/dataService';
import { CategoryList } from './pages//category/category.component';
import { Login } from './pages/account/login.component';
import { SignUp } from './pages/account/signup.component';
import { AuthTokenBearer } from './services/tokenService';



let routes = [
    { path: '', redirectTo: 'news-feed', pathMatch: 'full' },
    { path: "news-feed", component: NewsFeed },
    { path: "categories", component: CategoryList },
    { path: "login", component: Login },
    { path: "SignUp", component: SignUp }
   

];


@NgModule({
  declarations: [
      AppComponent,
      NewsFeed,
      CategoryList,
        Login,
        SignUp
  ],
  imports: [
      BrowserModule,
      InfiniteScrollModule,
      HttpModule,
      FormsModule,
      ReactiveFormsModule,
      RouterModule.forRoot(routes, {
          useHash: true,
          enableTracing: false // for debugging of the Routes
      })
  ],
    providers: [
        DataService,
        AuthTokenBearer
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
