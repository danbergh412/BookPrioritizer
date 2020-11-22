import { Component, OnInit } from '@angular/core';
import { BooksService } from '../services/books.service';
import { Router } from '@angular/router';
import { UpdateBookVM } from '../models/updateBookVM.model';
import { ParseGoodReadsShelf } from '../helpers/parseGoodReadsShelf';

@Component({
  selector: 'app-add-books',
  templateUrl: './add-books.component.html',
  styleUrls: ['./add-books.component.css']
})
export class AddBooksComponent implements OnInit {

  parseText: string;
  showLoadingIndicator: boolean = false;

  constructor(private _router: Router, private _service: BooksService) { 

  }
  saveBooks(){
    var parser = new ParseGoodReadsShelf(this.parseText);
    var books = parser.parse();
    
    this.showLoadingIndicator = true;
    this._service.saveBooks(books)
    .subscribe(() => {
        this.showLoadingIndicator = false;
        this.parseText = "";
    })
  }

  ngOnInit() {
  }

}
